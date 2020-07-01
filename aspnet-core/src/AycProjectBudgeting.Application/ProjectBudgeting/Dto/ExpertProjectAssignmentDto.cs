using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    [AutoMap(typeof(ExpertProjectAssignment))]
    public class ExpertProjectAssignmentDto : FullAuditedEntity<Guid>
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public Guid ExpertId { get; set; }

        [Required]
        public long Duration { get; set; }

        [Required]
        public long DailyPrice { get; set; }

        [Required]
        public long CurrencyTypeId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public Project Project { get; set; }

        public Expert Expert { get; set; }

        public Currency CurrencyType { get; set; }
    }
}