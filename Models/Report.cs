using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SnackisApp.Models
{
    public class Report
    {
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }
        public Post Post { get; set; }

        [Required]
        public string ReportedById { get; set; }
        public SnackisUser ReportedBy { get; set; }

        [Required]
        public DateTime ReportDate { get; set; }

        [StringLength(500)]
        public string Reason { get; set; }
        public ReportStatus Status { get; set; }
    }

    public enum ReportStatus
    {
        Pending,
        Approved,
        Rejected
    }
}