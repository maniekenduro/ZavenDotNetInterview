using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZavenDotNetInterview.App.Models
{
    public class Job
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(400)]
        [Index("JobName", 1, IsUnique = true)]
        public string Name { get; set; }
        public JobStatus Status { get; set; }
        public DateTime? DoAfter { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FailureCounter { get; set; }
        public virtual List<Log> Logs { get; set; }
    }

    public enum JobStatus
    {
        Failed = -1,
        New = 0,
        InProgress = 1,
        Done = 2,
        Closed = 3
    }
}