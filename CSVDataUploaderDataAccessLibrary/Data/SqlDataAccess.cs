﻿using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CSVDataUploaderDataAccessLibrary.Data;
public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> QueryDataAsync<T, U>(string queryString,
        U parameters,
        string connectionStringName = "Default")
    {
        string connectionString = _config.GetConnectionString(connectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);

        var rows = await connection.QueryAsync<T>(queryString,
            parameters,
            commandType: CommandType.Text);

        return rows;
    }

    public async Task ExecuteDataAsync<T>(string queryString,
        T parameters,
        string connectionStringName = "Default")
    {
        string connectionString = _config.GetConnectionString(connectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);

        var rows = await connection.QueryAsync<T>(queryString,
            parameters,
            commandType: CommandType.Text);
    }
}
