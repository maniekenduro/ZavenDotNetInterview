﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZavenDotNetInterview.App.Models;

namespace ZavenDotNetInterview.App.Repositories
{
    public interface ILogsRepository
    {
        List<Log> GetJobsLogs(Guid jobId);
        Log InsertLog(Log log);
    }
}
