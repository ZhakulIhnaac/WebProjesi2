using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    [AutoMap(typeof(Project))]
    public class ProjectDto : FullAuditedEntity<Guid>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public long Duration { get; set; }
    }
}