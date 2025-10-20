using Microsoft.Data.SqlClient;
using System.Data;

namespace Repository.Repositories;

/// <summary>
/// Provides a mechanism to execute a non-query stored procedure (INSERT, UPDATE, DELETE).
/// </summary>
public interface IGlobalNonQueryAction<T>
{
    Task ExecuteNonQueryCommand(string spName, object parameters);
}

/// <summary>
/// Provides a mechanism to execute a stored procedure and return a list of records.
/// </summary>
public interface IGlobalQueryListAction<T>
{
    Task<IList<T>> ExecuteQueryCommandList(string spName, object parameters);
}

/// <summary>
/// Provides a mechanism to execute a stored procedure and return a single record.
/// </summary>
public interface IGlobalQueryAction<T>
{
    Task<T> ExecuteQueryCommand(string spName, object parameters);
}

/// <summary>
/// Provides a mechanism to execute a stored procedure and return a single scalar value (such as COUNT, SUM, or an ID).
/// </summary>
public interface IGlobalScalarQueryAction<T>
{
    Task<T> ExecuteScalarQueryCommand(string spName, object? parameters = null, string? outputParamName = null);
}

/// <summary>
/// Provides a mechanism to execute a stored procedure with pagination support 
/// and return a list of records along with the total page count.
/// </summary>
public interface IGlobalQueryListPaginationAction<T>
{
    Task<(IList<T> Result, long TotalRecords)> ExecuteQueryCommandListPagination(
        string spName, object parameters, string pageCountParamName);
}

/// <summary>
/// Executes a stored procedure that returns:
/// 1. A result set mapped to type <typeparamref name="T"/>, and
/// 2. An output parameter (e.g., @IsSuccess) as a boolean flag.
/// </summary>
public interface IGlobalQueryWithSuccessOutputAction<T>
{
    Task<(bool IsSuccess, IList<T> Result)> ExecuteQueryCommandWithSuccessOutput(
        string spName,
        DynamicParameters parameters,
        string outputParamName = "@IsSuccess"
    );
}

/// <summary>
/// Provides a mechanism to execute a stored procedure that returns multiple result sets dynamically.
/// Each result set is returned as an IEnumerable<dynamic> in a list.
/// </summary>
public interface IGlobalQueryMultipleDynamicAction
{
    Task<List<IEnumerable<dynamic>>?> ExecuteQueryMultipleCommandDynamic(string spName, object parameters);
}

/// <summary>
/// Interface for executing stored procedures that return multiple result sets 
/// along with an output parameter for pagination or other metadata.
/// </summary>
public interface IGlobalQueryMultipleWithPaginationAction
{
    Task<(List<IEnumerable<dynamic>> Result, long TotalRecords)> ExecuteQueryMultipleWithPagination(
        string spName, object parameters, string pageCountParamName);
}



/// <summary>
/// Base repository to handle database connections.
/// </summary>
internal abstract class BaseRepository
{
    private readonly string _connectionString;

    protected BaseRepository()
    {
        _connectionString = Constants.ConnectionString ?? throw new InvalidOperationException("Connection string is not initialized.");
    }

    /// <summary>
    /// Creates a new database connection.
    /// </summary>
    protected IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}

/// <summary>
/// Handles executing non-query commands such as INSERT, UPDATE, DELETE.
/// </summary>
internal class GlobalNonQueryAction<T> : BaseRepository, IGlobalNonQueryAction<T>
{
    /// <summary>
    /// Executes a non-query command (such as an INSERT, UPDATE, or DELETE) against the database 
    /// using a stored procedure.
    /// </summary>
    public async Task ExecuteNonQueryCommand(string spName, object parameters)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
    }
}

/// <summary>
/// Handles executing stored procedures that return a list of records.
/// </summary>
internal class GlobalQueryListAction<T> : BaseRepository, IGlobalQueryListAction<T>
{
    /// <summary>
    /// Executes a query command that returns a list of results from the database.
    /// </summary>
    /// <returns>A list of results of type T.</returns>
    public async Task<IList<T>> ExecuteQueryCommandList(string spName, object parameters)
    {
        using var connection = CreateConnection();
        var data = await connection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
        return data.ToList();
    }
}

/// <summary>
/// Handles executing stored procedures that return a single record.
/// </summary>
internal class GlobalQueryAction<T> : BaseRepository, IGlobalQueryAction<T>
{
    /// <summary>
    /// Executes a query command that returns a single result from the database.
    /// </summary>
    /// <returns>The result of type T, or the default value of T if no result is found.</returns>
    public async Task<T> ExecuteQueryCommand(string spName, object parameters)
    {
        using var connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
        return result ?? default!;
    }
}

/// <summary>
/// Handles executing stored procedures that return a single scalar value (like SUM, COUNT, or an ID).
/// </summary>
internal class GlobalScalarQueryAction<T> : BaseRepository, IGlobalScalarQueryAction<T>
{
    public async Task<T> ExecuteScalarQueryCommand(string spName, object? inputParams = null, string? outputParamName = null)
    {
        using var connection = CreateConnection();
        var parameters = new DynamicParameters();

        if (inputParams is DynamicParameters dynParams)
        {
            parameters = dynParams;
        }
        else if (inputParams != null)
        {
            foreach (var prop in inputParams.GetType().GetProperties())
            {
                var value = prop.GetValue(inputParams);
                parameters.Add("@" + prop.Name, value);
            }
        }

        if (!string.IsNullOrWhiteSpace(outputParamName))
        {
            parameters.Add(
                "@" + outputParamName,
                dbType: Helper.GetDbType(typeof(T)),
                direction: ParameterDirection.Output
            );
        }

        if (!string.IsNullOrWhiteSpace(outputParamName))
        {
            await connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            return parameters.Get<T>("@" + outputParamName);
        }
        else
        {
            var scalarResult = await connection.ExecuteScalarAsync<T>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure,
                commandTimeout: 120
            );
            return scalarResult!;
        }
    }
}

/// <summary>
/// Implements pagination support for executing stored procedures and retrieving a list of records 
/// along with the total page count.
/// </summary>
internal class GlobalQueryListPaginationAction<T> : BaseRepository, IGlobalQueryListPaginationAction<T>
{
    public async Task<(IList<T> Result, long TotalRecords)> ExecuteQueryCommandListPagination(
        string spName, object parameters, string pageCountParamName)
    {
        using var connection = CreateConnection();
        var dynamicParams = new DynamicParameters(parameters);

        dynamicParams.Add(pageCountParamName, dbType: DbType.Int64, direction: ParameterDirection.Output);

        var data = await connection.QueryAsync<T>(spName, dynamicParams, commandType: CommandType.StoredProcedure);
        long pageCount = dynamicParams.Get<long>(pageCountParamName);

        return (data.ToList(), pageCount);
    }
}

/// <summary>
/// Executes a stored procedure using Dapper and returns a list of results along with an output success flag.
/// This action executes the stored procedure with optional output parameter (default: "@IsSuccess") and returns both:
/// - the success status from the output parameter
/// - the result list mapped to type <typeparamref name="T"/>
/// </summary>
internal class GlobalQueryWithSuccessOutputAction<T> : BaseRepository, IGlobalQueryWithSuccessOutputAction<T>
{
    public async Task<(bool IsSuccess, IList<T> Result)> ExecuteQueryCommandWithSuccessOutput(
        string spName, DynamicParameters parameters, string outputParamName = "@IsSuccess")
    {
        using var connection = CreateConnection();

        if (!parameters.ParameterNames.Contains(outputParamName))
        {
            parameters.Add(outputParamName, dbType: DbType.Boolean, direction: ParameterDirection.Output);
        }

        var result = (await connection.QueryAsync<T>(
            spName,
            parameters,
            commandType: CommandType.StoredProcedure
        )).ToList();

        var isSuccess = parameters.Get<bool>(outputParamName);

        return (isSuccess, result);
    }
}

/// <summary>
/// Implements dynamic multi-result-set support for executing stored procedures.
/// This action executes a stored procedure using Dapper's QueryMultipleAsync method and returns all result sets
/// </summary>
internal class GlobalQueryMultipleDynamicAction : BaseRepository, IGlobalQueryMultipleDynamicAction
{
    public async Task<List<IEnumerable<dynamic>>?> ExecuteQueryMultipleCommandDynamic(string spName, object parameters)
    {
        using var connection = CreateConnection();

        try
        {
            using var multi = await connection.QueryMultipleAsync(spName, parameters, commandType: CommandType.StoredProcedure);

            var results = new List<IEnumerable<dynamic>>();

            while (!multi.IsConsumed)
            {
                try
                {
                    var resultSet = (await multi.ReadAsync()).ToList();
                    results.Add(resultSet);
                }
                catch (InvalidOperationException)
                {
                    results.Add([]);
                }
            }

            if (results.All(r => !r.Any()))
                return null;

            return results;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

/// <summary>
/// Implements dynamic multi-result-set support for executing stored procedures.
/// This action executes a stored procedure using Dapper's QueryMultipleAsync method and returns all result sets
/// along with an output parameter (e.g., total records for pagination).
/// </summary>
internal class GlobalQueryMultipleWithPaginationAction : BaseRepository, IGlobalQueryMultipleWithPaginationAction
{
    public async Task<(List<IEnumerable<dynamic>> Result, long TotalRecords)> ExecuteQueryMultipleWithPagination(
        string spName, object parameters, string pageCountParamName)
    {
        using var connection = CreateConnection();
        var dynamicParams = new DynamicParameters(parameters);
        dynamicParams.Add(pageCountParamName, dbType: DbType.Int64, direction: ParameterDirection.Output);

        using var multi = await connection.QueryMultipleAsync(spName, dynamicParams, commandType: CommandType.StoredProcedure);

        var results = new List<IEnumerable<dynamic>>();
        while (!multi.IsConsumed)
        {
            var resultSet = (await multi.ReadAsync()).ToList();
            results.Add(resultSet);
        }

        long totalRecords;
        try
        {
            totalRecords = dynamicParams.Get<long>(pageCountParamName);
        }
        catch
        {
            totalRecords = 0;
        }

        return (results, totalRecords);
    }
}
