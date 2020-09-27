using DataAcess.Abstractions;
using Dapper;
using DataAcess.Infrastructure;
using Domain.Models.OTP;
using System;
using Domain.Infrastucture;

namespace DataAcess.Repositories
{
    internal class OTPRepository : IOTPRepository
    {
        private readonly IDbConnection _db;

        public OTPRepository(IDbConnection db)
        {
            _db = db;
        }
        public bool AddOTP(string otp, MethodInvokeModel confirmationMethod, int expirySpan)
        {
            var expiryDate = DateTime.Now.AddSeconds(expirySpan);
            confirmationMethod.Params.ForEach(x =>
            {
                x.Value = x.Value.ToString();
            });
            var query = "CREATE_OTP";

            var @params = new
            {
                otp,
                methodName = confirmationMethod.MethodName,
                parameters = confirmationMethod
                                .Params
                                .ToDataTable()
                                .AsTableValuedParameter("CALLBACK_PARAM_TYPE"),
                expiryDate
            };
            var result = _db.ExecuteQuery(query, System.Data.CommandType.StoredProcedure, out bool isSuccessful, @params);
            return isSuccessful;
        }

        public void DeleteAllExpiredOTPs()
        {
            var query = "DELETE FROM OTPS WHERE EXPIRYTIME>=@currentDate";
            var p = new
            {
                currentDate = DateTime.Now
            };
            _db.ExecuteQuery(query, System.Data.CommandType.Text, out bool isSuccessfull, p);
        }

        public void DeleteOTP(string otp)
        {
            var query = "DELETE FROM OTPS WHERE HashedOTP=@otp";
            var p = new
            {
                otp
            };
            _db.ExecuteQuery(query, System.Data.CommandType.Text, out bool isSuccessfull, p);
        }

        public MethodInvokeModel GetConfirmationInvokeMethod(string otp)
        {
            var queryForMethodName = @"select c.CallbackName as MethodName
                                        from OTPs otp
                                        inner join OTP_CALLBACKS c on c.OtpId = otp.OTPId
                                        where otp.HashedOTP = @otp";
            var queryForParams = @"select p.IsInJson, p.ParamName as Name, p.Type, p.Value
                                from OTPs otp
                                inner join OTP_CALLBACKS c on c.OtpId=otp.OTPId
                                inner join CALLBACK_PARAMS p on p.CallbackId=c.CallbackId
                                where otp.HashedOTP=@otp";

            var p = new { otp };
            MethodInvokeModel result = new MethodInvokeModel
            {
                MethodName = _db.GetScalerResult<string>(queryForMethodName, System.Data.CommandType.Text, out bool isDataFound, p),
                Params = _db.GetListResult<MethodParams>(queryForParams, System.Data.CommandType.Text, out bool isDataFound2, p)
            };
            return result;
        }

        public bool IsExists(string otp)
        {
            var query = "select count(*) from OTPs where HashedOTP=@otp";
            var p = new
            {
                otp
            };
            var count = _db.GetScalerResult<int>(query, System.Data.CommandType.Text, out _, p);
            return count > 0;
        }
    }
}
