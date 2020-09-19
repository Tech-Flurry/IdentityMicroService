using DataAcess.Abstractions;
using DataAcess.Infrastructure;
using Domain.Models.Applications;
using System.Collections.Generic;

namespace DataAcess.Repositories
{
    internal class ApplicationsRepository : IApplicationsRepository
    {
        private readonly IDbConnection _db;

        public ApplicationsRepository(IDbConnection db)
        {
            _db = db;
        }

        public int CreateApplication(CreateApplicationModel appInfo)
        {
            var id = 0;
            var query = "CREATE_NEW_APPLICATION";
            var @params = new
            {
                APP_NAME = appInfo.ApplicationName,
                CREATED_BY = appInfo.CreatedBy,
                CREATED_DATE = appInfo.CreatedDate,
                IS_PHONE_AUTH = appInfo.Configuration.IsPhoneAuthentication,
                IS_ROLES_DEFINED = appInfo.Configuration.IsRolesDefined,
                IS_EMAIL_AUTH = appInfo.Configuration.IsEmailAuthentication,
                MAX_ATTEMPTS = appInfo.Configuration.MaxAttemptsAllowed
            };
            var result = _db.GetScalerResult<int>(query, System.Data.CommandType.StoredProcedure, out bool isSuccessfull, @params);
            if (isSuccessfull)
            {
                id = result;
            }
            return id;
        }

        public List<ApplicationsListModel> GetApplicationsList()
        {
            var query = "SELECT T.AppId AS ApplicationId, T.AppName AS ApplicationName, T.CreatedDate FROM APPLICATIONS T";
            List<ApplicationsListModel> result = _db.GetListResult<ApplicationsListModel>(query, System.Data.CommandType.Text, out bool isDataFound);
            return result;
        }

        public bool IsApplicationExists(int appId)
        {
            var query = @"SELECT COUNT(*) AS ApplicationName 
                            FROM APPLICATIONS T
                            WHERE T.AppId=@id";
            var result = _db.GetScalerResult<int>(query, System.Data.CommandType.Text, out bool isDataFound, new { id = appId });
            return result > 0; //return true if record found
        }

        public ApplicationGenerateModel ReadApplicationToGenerateKey(int appId)
        {
            var query = @"SELECT T.AppId, T.AppName AS ApplicationName 
                                FROM APPLICATIONS T
                                WHERE T.AppId=@id";
            var result = _db.GetSingleResult<ApplicationGenerateModel>(query, System.Data.CommandType.Text, out bool isDataFound, new { id = appId });
            return result;
        }

        public bool UpdateApplicationInfo(UpdateApplicationModel appInfo)
        {
            var query = "UPDATE_APPLICATION";
            var @params = new
            {
                APP_ID = appInfo.AppId,
                APP_NAME = appInfo.AppName,
                MODIFIED_BY = appInfo.ModifiedBy,
                MODIFIED_DATE = appInfo.ModifiedDate,
                IS_PHONE_AUTH = appInfo.Configuration.IsPhoneAuthentication,
                IS_ROLES_DEFINED = appInfo.Configuration.IsRolesDefined,
                IS_EMAIL_AUTH = appInfo.Configuration.IsEmailAuthentication,
                MAX_ATTEMPTS = appInfo.Configuration.MaxAttemptsAllowed
            };
            _db.ExecuteQuery(query, System.Data.CommandType.StoredProcedure, out bool isSuccessfull, @params);
            return isSuccessfull;
        }
    }
}
