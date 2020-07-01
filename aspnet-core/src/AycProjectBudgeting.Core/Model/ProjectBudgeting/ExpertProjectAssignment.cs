using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity

{
    [Table("t_expert_project_assignment")]

    public class ExpertProjectAssignment : FullAuditedEntity<Guid>
    {
        [Column("ProjectId")]
        [Required]
        public Guid ProjectId { get; set; }

        [Column("ExpertId")]
        [Required]
        public Guid ExpertId { get; set; }

        [Column("Duration")]
        [Required]
        public long Duration { get; set; }

        [Column("DailyPrice")]
        [Required]
        public long DailyPrice { get; set; }

        [Column("CurrencyTypeId")]
        [Required]
        public long CurrencyTypeId { get; set; }

        [Column("Title")]
        [Required]
        public string Title { get; set; }

        [Column("Description")]
        [Required]
        public string Description { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        [ForeignKey("ExpertId")]
        public Expert Expert { get; set; }

        [ForeignKey("CurrencyTypeId")]
        public Currency CurrencyType { get; set; }
    }
}