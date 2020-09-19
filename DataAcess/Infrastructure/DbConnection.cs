using Dapper;
using Domain.Infrastucture.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAcess.Infrastructure
{
    /// <summary>
    /// Handles the Db connections and commands
    /// </summary>
    public interface IDbConnection
    {
        /// <summary>
        /// Returns the number of rows affected by the command
        /// </summary>
        /// <param name="query">the command to be executed</param>
        /// <param name="commandType">type of the command (query/procedure)</param>
        /// <param name="isSuccessfull">out flag which indicates the success of the operation</param>
        /// <param name="params">parameters for the command</param>
        /// <param name="transaction"></param>
        int ExecuteQuery(string query, CommandType commandType, out bool isSuccessfull, object @params = null, IDbTransaction transaction = null);
        /// <summary>
        /// Runs a set on commands in a transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        bool ExecuteTransaction(Func<IDbTransaction, bool> transaction);
        /// <summary>
        /// Returns the data extracted from the db in the form of list
        /// </summary>
        /// <typeparam name="T">Any Model Matching the query result</typeparam>
        /// <param name="query">The command which fetches the data</param>
        /// <param name="commandType">Type of the command</param>
        /// <param name="isDataFound">Out flag which indicates the presence of data</param>
        /// <param name="params">parameters of a query</param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        List<T> GetListResult<T>(string query, CommandType commandType, out bool isDataFound, object @params = null, IDbTransaction transaction = null);
        /// <summary>
        /// Returns the first column of the row of the data extracted from the db
        /// </summary>
        /// <typeparam name="T">Any Model Matching the query result</typeparam>
        /// <param name="query">The command which fetches the data</param>
        /// <param name="commandType">Type of the command</param>
        /// <param name="isDataFound">Out flag which indicates the presence of data</param>
        /// <param name="params">parameters of a query</param>
        /// <param name="transaction"></param>
        T GetScalerResult<T>(string query, CommandType commandType, out bool isDataFound, object @params = null, IDbTransaction transaction = null);
        /// <summary>
        /// Returns the first row of the data extracted from the db
        /// </summary>
        /// <typeparam name="T">Any Model Matching the query result</typeparam>
        /// <param name="query">The command which fetches the data</param>
        /// <param name="commandType">Type of the command</param>
        /// <param name="isDataFound">Out flag which indicates the presence of data</param>
        /// <param name="params">parameters of a query</param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        T GetSingleResult<T>(string query, CommandType commandType, out bool isDataFound, object @params = null, IDbTransaction transaction = null);
    }

    /// <summary>
    /// Handles the Db connections and commands
    /// </summary>
    internal class DbConnection : IDbConnection
    {
        private readonly IDbConfiguration _configuration;

        public DbConnection(IDbConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Returns the data extracted from the db in the form of list
        /// </summary>
        /// <typeparam name="T">Any Model Matching the query result</typeparam>
        /// <param name="query">The command which fetches the data</param>
        /// <param name="commandType">Type of the command</param>
        /// <param name="isDataFound">Out flag which indicates the presence of data</param>
        /// <param name="params">parameters of a query</param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<T> GetListResult<T>(string query, CommandType commandType, out bool isDataFound, object @params = null, IDbTransaction transaction = null)
        {
            List<T> result;
            using (System.Data.IDbConnection con = new SqlConnection(_configuration.ConnectionString))
            {
                result = con.Query<T>(sql: query, commandType: commandType, commandTimeout: _configuration.ConnectionTimeout, param: @params, transaction: transaction).ToList();
            }
            isDataFound = (result != null || result.Count > 0);
            return result;
        }
        /// <summary>
        /// Returns the first row of the data extracted from the db
        /// </summary>
        /// <typeparam name="T">Any Model Matching the query result</typeparam>
        /// <param name="query">The command which fetches the data</param>
        /// <param name="commandType">Type of the command</param>
        /// <param name="isDataFound">Out flag which indicates the presence of data</param>
        /// <param name="params">parameters of a query</param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public T GetSingleResult<T>(string query, CommandType commandType, out bool isDataFound, object @params = null, IDbTransaction transaction = null)
        {
            T result;
            using (System.Data.IDbConnection con = new SqlConnection(_configuration.ConnectionString))
            {
                result = con.Query<T>(sql: query, commandType: commandType, commandTimeout: _configuration.ConnectionTimeout, param: @params, transaction: transaction).First();
            }
            isDataFound = (result != null);
            return result;
        }
        /// <summary>
        /// Returns the first column of the row of the data extracted from the db
        /// </summary>
        /// <typeparam name="T">Any Model Matching the query result</typeparam>
        /// <param name="query">The command which fetches the data</param>
        /// <param name="commandType">Type of the command</param>
        /// <param name="isDataFound">Out flag which indicates the presence of data</param>
        /// <param name="params">parameters of a query</param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public T GetScalerResult<T>(string query, CommandType commandType, out bool isDataFound, object @params = null, IDbTransaction transaction = null)
        {
            T result;
            using (System.Data.IDbConnection con = new SqlConnection(_configuration.ConnectionString))
            {
                result = (T)con.ExecuteScalar(sql: query, commandType: commandType, commandTimeout: _configuration.ConnectionTimeout, param: @params, transaction: transaction);
            }
            isDataFound = (result != null);
            return result;
        }
        /// <summary>
        /// Returns the number of rows affected by the command
        /// </summary>
        /// <param name="query">the command to be executed</param>
        /// <param name="commandType">type of the command (query/procedure)</param>
        /// <param name="isSuccessfull">out flag which indicates the success of the operation</param>
        /// <param name="params">parameters for the command</param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int ExecuteQuery(string query, CommandType commandType, out bool isSuccessfull, object @params = null, IDbTransaction transaction = null)
        {
            int result = 0;
            using (System.Data.IDbConnection con = new SqlConnection(_configuration.ConnectionString))
            {
                result = con.Execute(query, param: @params, commandTimeout: _configuration.ConnectionTimeout, commandType: commandType, transaction: transaction);
            }
            isSuccessfull = (result > 0);
            return result;
        }
        /// <summary>
        /// Runs a set on commands in a transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool ExecuteTransaction(Func<IDbTransaction, bool> transaction)
        {
            if (transaction is null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }
            using System.Data.IDbConnection con = new SqlConnection(_configuration.ConnectionString);
            var dbTransaction = con.BeginTransaction();
            try
            {
                transaction.Invoke(dbTransaction);
                dbTransaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
        }
    }
}
