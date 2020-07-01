using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity

{
    [Table("t_activities")]

    public class Activity : FullAuditedEntity<Guid>
    {
        [Column("Name")]
        [Required]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("StartingMonth")]
        [Required]
        public long StartingMonth { get; set; }

        [Column("EndingMonth")]
        [Required]
        public long EndingMonth { get; set; }

        [Column("RelatedProjectId")]
        [Required]
        public Guid RelatedProjectId { get; set; }

        [ForeignKey("RelatedProjectId")]
        public Project RelatedProject { get; set; }
    }
}