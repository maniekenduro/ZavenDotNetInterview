using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ZavenDotNetInterview.App.Extensions;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.Context;
using ZavenDotNetInterview.App.Repositories;

namespace ZavenDotNetInterview.App.Services
{
    public class JobProcessorService : IJobProcessorService
    {
        private ZavenDotNetInterviewContext _ctx;
        private readonly ILogsRepository _logsRepository;
        private bool disposed = false;

        public JobProcessorService(ZavenDotNetInterviewContext ctx, ILogsRepository logsRepository)
        {
            _ctx = ctx;
            _logsRepository = logsRepository;
        }

        public async Task ProcessJobs()
        {
            IJobsRepository jobsRepository = new JobsRepository(_ctx, _logsRepository);
            var allJobs = jobsRepository.GetAllJobs();
            var jobsToProcess = allJobs.Where(x => x.Status == JobStatus.New).ToList();

            jobsToProcess.ForEach(job => job.ChangeStatus(JobStatus.InProgress));
                        
            await _ctx.SaveChangesAsync();

            Parallel.ForEach(jobsToProcess, (currentjob) =>
            {
                new Task(async () =>
                {
                    Log log = new Log() { Job = currentjob, JobId = currentjob.Id, CreatedAt = DateTime.Now };
                    bool result = await this.ProcessJob(currentjob).ConfigureAwait(false);
                    if (currentjob.FailureCounter < 5)
                    {
                        if (result)
                        {
                            log.Description = "Job status has been changed";
                            currentjob.ChangeStatus(JobStatus.Done);
                        }
                        else
                        {
                            log.Description = "Change Job status has been failed";
                            currentjob.ChangeStatus(JobStatus.Failed);
                            currentjob.RaiseFailureCounter();
                        }
                        currentjob.LastUpdatedAt = DateTime.Now;
                        _logsRepository.InsertLog(log);
                    }
                    else
                    {
                        currentjob.ChangeStatus(JobStatus.Closed);
                    }
                }).Start();
                
            });
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private async Task<bool> ProcessJob(Job job)
        {
            Random rand = new Random();
            if (rand.Next(10) < 5)
            {
                await Task.Delay(2000);
                return false;
            }
            else
            {
                await Task.Delay(1000);
                return true;
            }
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