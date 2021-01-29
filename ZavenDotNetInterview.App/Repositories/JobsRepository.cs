using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.Context;

namespace ZavenDotNetInterview.App.Repositories
{
    public class JobsRepository : IJobsRepository, IDisposable
    {
        private readonly ZavenDotNetInterviewContext _ctx;
        private readonly ILogsRepository _logsRepository;
        private bool disposed = false;

        public JobsRepository(ZavenDotNetInterviewContext ctx, ILogsRepository logsRepository)
        {
            _ctx = ctx;
            _logsRepository = logsRepository;
        }

        public List<Job> GetAllJobs()
        {
            return _ctx.Jobs.OrderBy(x => x.CreatedAt).ToList();
        }

        public Job GetJobDetails(Guid guid)
        {
            var css = _ctx.Jobs.Where(x => x.Id == guid).FirstOrDefault();
            return css;
        }

        public Job CreateJob(string name, DateTime? doAfter)
        {
            Job newJob = new Job() { Id = Guid.NewGuid(), DoAfter = doAfter, LastUpdatedAt = DateTime.Now, FailureCounter = 0, CreatedAt = DateTime.Now, Name = name, Status = JobStatus.New };
            Log log = new Log() { Job = newJob, JobId = newJob.Id, CreatedAt = DateTime.Now};
            try
            {
                newJob = _ctx.Jobs.Add(newJob);
                _ctx.SaveChanges();

                log.Description = "A new job has been added";
                _logsRepository.InsertLog(log);
                _ctx.SaveChanges();
            }
            catch
            {
                log.Description = "A new job was not added corretlly";
                _logsRepository.InsertLog(log);
                _ctx.SaveChanges();
            }
            return newJob;

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
            }
            this.disposed = true;
        }

        
    }
}