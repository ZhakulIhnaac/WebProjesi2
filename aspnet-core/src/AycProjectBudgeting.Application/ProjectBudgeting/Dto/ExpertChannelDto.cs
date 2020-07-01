using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    [AutoMap(typeof(ExpertChannel))]
    public class ExpertChannelDto : FullAuditedEntityDto<Guid>
    {
        [Required]
        public Guid ExpertId { get; set; }

        [Required]
        public long ChannelTypeId { get; set; }

        [Required]
        public string ChannelContext { get; set; }

        public ExpertDto Expert { get; set; }

        public ChannelTypeDto ChannelType { get; set; }
    }
}
