using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity

{
    [Table("t_expert_channel")]

    public class ExpertChannel : FullAuditedEntity<Guid>
    {
        [Column("ExpertId")]
        [Required]
        public Guid ExpertId { get; set; }

        [Column("ChannelTypeId")]
        [Required]
        public long ChannelTypeId { get; set; }

        [Column("ChannelContext")]
        [Required]
        public string ChannelContext { get; set; }

        [ForeignKey("ExpertId")]
        public Expert Expert { get; set; }

        [ForeignKey("ChannelTypeId")]
        public ChannelType ChannelType { get; set; }
    }
}