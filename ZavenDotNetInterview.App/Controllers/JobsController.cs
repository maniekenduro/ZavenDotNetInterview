using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.Context;
using ZavenDotNetInterview.App.Repositories;
using ZavenDotNetInterview.App.Services;

namespace ZavenDotNetInterview.App.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobProcessorService _jobProcessorService;
        private readonly IJobsRepository _jobsRepository;
        public JobsController(IJobProcessorService jobProcessorService, IJobsRepository jobsRepository)
        {
            _jobProcessorService = jobProcessorService;
            _jobsRepository = jobsRepository;

        }

        // GET: Tasks
        
        public ActionResult Index()
        {
            List<Job> jobs = _jobsRepository.GetAllJobs();
            return View(jobs);
        }

        // POST: Tasks/Process
        [HttpGet]
        public ActionResult Process()
        {
            _jobProcessorService.ProcessJobs();

            return RedirectToAction("Index");
        }

        // GET: Tasks/Create
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        public ActionResult Create(string name, DateTime? doAfter)
        {
            try
            {
                _jobsRepository.CreateJob(name, doAfter);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(Guid jobId)
        {
            Job job = _jobsRepository.GetJobDetails(jobId);
            return View(job);
        }
    }
}
