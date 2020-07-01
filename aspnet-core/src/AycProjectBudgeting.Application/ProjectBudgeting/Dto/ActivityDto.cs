using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    [AutoMap(typeof(Activity))]
    public class ActivityDto : FullAuditedEntityDto<Guid>
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public long StartingMonth { get; set; }

        [Required]
        public long EndingMonth { get; set; }

        [Required]
        public Guid RelatedProjectId { get; set; }

        public Project RelatedProject { get; set; }
    }
}
