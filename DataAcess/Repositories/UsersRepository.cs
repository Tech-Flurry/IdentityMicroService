using Dapper;
using DataAcess.Abstractions;
using DataAcess.Infrastructure;
using Domain.Infrastucture;
using Domain.Models.Users;
using Domain.ValueObjects;
using System.Collections.Generic;

namespace DataAcess.Repositories
{
    internal class UsersRepository : IUsersRepository
    {
        private readonly IDbConnection _db;

        public UsersRepository(IDbConnection db)
        {
            _db = db;
        }
        public bool ChangeDisableFlag(long userId, bool isDisabled)
        {
            var query = "update USERS set IsDisabled=@isDisabled where UserId=@userId";
            var @params = new { userId, isDisabled };
            _db.ExecuteQuery(query, System.Data.CommandType.Text, out bool isSuccessfull, @params);
            return isSuccessfull;
        }

        public bool CreateUser(CreateUserModel model, int appId)
        {
            var query = "ADD_NEW_USER";
            var @params = new
            {
                firstName = model.FullName.FirstName,
                lastName = model.FullName.LastName,
                username = model.Username,
                password = model.Password,
                phone = model.MobileNumber,
                email = model.Email,
                country = 1,
                appId,
                roles = model.Roles.ToDataTable().AsTableValuedParameter("UserRolesType")
            };
            _db.ExecuteQuery(query, System.Data.CommandType.StoredProcedure, out bool isSuccessfull, @params);
            return isSuccessfull;
        }

        public List<UserListingModel> GetRegisteredUsers(int appId)
        {
            var query = @"select UserId as Id,
                            PhoneNumber as MobileNumber,
                            'Pakistan' as Country,
                            Email,
                            FirstName,
                            LastName
                            from USERS where AppId=@appId";
            var p = new
            {
                appId
            };
            var result = _db.GetListResult<UserListingModel, Name>(query, System.Data.CommandType.Text, (user, name) =>
              {
                  user.UserFullName = name;
                  return user;
              }, out _, p);
            return result;
        }

        public long GetUserId(string username, int appId)
        {
            var query = "select UserId from USERS where Username=@username and AppId=@appId";
            var p = new
            {
                username,
                appId
            };
            var result = _db.GetScalerResult<long?>(query, System.Data.CommandType.Text, out bool isDataFound, p);
            return isDataFound ? result ?? 0 : 0;
        }

        public bool IsExists(string username, int appId)
        {
            var query = "select count(*) from USERS where Username=@username and AppId=@appId";
            var p = new
            {
                username,
                appId
            };
            var count = _db.GetScalerResult<int>(query, System.Data.CommandType.Text, out _, p);
            return count > 0;
        }

        public bool UpdatePhoneNumber(UserPhoneNumberParam model, int appId)
        {
            var query = "update USERS set PhoneNumber=@phoneNumber where UserId=@userId";
            var @params = new
            {
                phoneNumber = model.PhoneNumber,
                userId = model.UserId
            };
            _db.ExecuteQuery(query, System.Data.CommandType.Text, out bool isSuccessfull, @params);
            return isSuccessfull;
        }

        public bool UpdateUser(UpdateUserModel model, int appId)
        {
            var query = "update USERS set FirstName=@firstName, LastName=@lastName where UserId=@userId";
            var @params = new
            {
                firstName = model.FullName.FirstName,
                lastName = model.FullName.LastName,
                userId = model.UserId
            };
            _db.ExecuteQuery(query, System.Data.CommandType.Text, out bool isSuccessfull, @params);
            return isSuccessfull;
        }
    }
}
