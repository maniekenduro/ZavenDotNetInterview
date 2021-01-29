using System;
using System.Collections.Generic;
using ZavenDotNetInterview.App.Models;

namespace ZavenDotNetInterview.App.Repositories
{
    public interface IJobsRepository
    {
        List<Job> GetAllJobs();
        Job CreateJob(string name, DateTime? doAfter);
        Job GetJobDetails(Guid guid);
    }
}