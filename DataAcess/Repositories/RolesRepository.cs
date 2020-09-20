using DataAcess.Abstractions;
using DataAcess.Infrastructure;
using Domain.Infrastucture;
using Domain.Models.Roles;
using System;
using System.Collections.Generic;

namespace DataAcess.Repositories
{
    internal class RolesRepository : IRolesRepository
    {
        private readonly IDbConnection _db;

        public RolesRepository(IDbConnection db)
        {
            _db = db;
        }
        public bool AddNewApplicationRole(AddRoleModel model)
        {
            var query = "CREATE_NEW_ROLE";
            var @params = new
            {
                APP_ID = model.AppId,
                ROLE_NAME = model.RoleName.Trim().ToInitialCapital(),
                CREATED_BY = model.CreatedBy,
                CREATED_DATE = model.CreatedDate
            };
            bool result;
            try
            {
                var rows = _db.ExecuteQuery(query, System.Data.CommandType.StoredProcedure, out bool isSuccessfull, @params);
                result = rows > 0;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool DeleteRole(string role, int appId)
        {
            var query = @"DELETE APPLICATION_ROLES
                            where lower(RoleName)=@role
                            and AppId=@appId";
            var @params = new
            {
                role = role.Trim().ToLower(),
                appId,
            };
            _db.ExecuteQuery(query, System.Data.CommandType.Text, out bool isSuccessfull, @params);
            return isSuccessfull;
        }

        public bool DisableRole(string role, int appId)
        {
            return ChangeDisableFlag(role, appId, true);
        }

        public bool EnableRole(string role, int appId)
        {
            return ChangeDisableFlag(role, appId, false);
        }
        private bool ChangeDisableFlag(string role, int appId, bool isDisabled)
        {
            var query = @"update APPLICATION_ROLES
                            set IsDisabled=@isDisabled
                            where lower(RoleName)=@role
                            and AppId=@appId";
            var @params = new
            {
                role = role.Trim().ToLower(),
                appId,
                isDisabled
            };
            _db.ExecuteQuery(query, System.Data.CommandType.Text, out bool isSuccessful, @params);
            return isSuccessful;
        }
        public List<RolesListModel> GetApplicationRoles(int appId)
        {
            var query = @"SELECT *
                            FROM APPLICATION_ROLES T
                            WHERE T.APPID=@appId";
            var @params = new
            {
                appId
            };
            var result = _db.GetListResult<RolesListModel>(query, System.Data.CommandType.Text, out bool isDataFound, @params);
            if (isDataFound)
            {
                return result;
            }
            else
            {
                return new List<RolesListModel>();
            }
        }

        public bool IsExists(string roleName, int appId)
        {
            var query = @"SELECT COUNT(*)
                            FROM APPLICATION_ROLES T
                            WHERE LOWER(T.ROLENAME)=@roleName AND T.APPID=@appId";
            var @params = new
            {
                roleName = roleName.Trim().ToLower(),
                appId
            };

            var count = _db.GetScalerResult<int>(query, System.Data.CommandType.Text, out _, @params);
            return (count > 0); //returns true if count is greater then 0 else returns false
        }

        public bool IsRoleAssigned(string role, int appId)
        {
            var query = @"SELECT COUNT(*)
                            FROM USER_ROLES T
                            INNER JOIN APPLICATION_ROLES AR ON AR.ROLEID=T.ROLEID
                            WHERE AR.APPID=@appId AND LOWER(AR.ROLENAME)=@role";
            var @params = new
            {
                appId,
                role = role.Trim().ToLower()
            };
            var count = _db.GetScalerResult<int>(query, System.Data.CommandType.Text, out _, @params);
            return count > 0; //returns true if count is greater then 0 else returns false
        }
    }
}
