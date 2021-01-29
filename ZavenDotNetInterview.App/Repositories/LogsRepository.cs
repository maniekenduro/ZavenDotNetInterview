using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.Context;

namespace ZavenDotNetInterview.App.Repositories
{
    public class LogsRepository: ILogsRepository
    {
        public LogsRepository()
        {
        }

        public List<Log> GetJobsLogs(Guid jobId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ToString()))
            {
                var logs = connection.Query<Log>($"SELECT * FROM Logs WHERE JobId = {jobId}").ToList();
                return logs;
            }
        }

        public Log InsertLog(Log log)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ToString()))
            {
                string sql = "INSERT INTO Logs (Id, Description, CreatedAt, JobId) Values (@Id, @Description, @CreatedAt, @JobId);";

                log.CreatedAt = DateTime.UtcNow;
                var newLog = connection.Execute(sql, new { Id = Guid.NewGuid(), Description = log.Description, CreatedAt = log.CreatedAt, JobId = log.JobId });

                return log;
            }
        }
    }
}