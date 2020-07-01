using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity

{
    [Table("t_projects")]

    public class Project : FullAuditedEntity<Guid>
    {
        [Column("Name")]
        [Required]
        public string Name { get; set; }

        [Column("Description")]
        [Required]
        public string Description { get; set; }

        [Column("Duration")]
        [Required]
        public long Duration { get; set; }

    }
}