using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity

{
    [Table("t_expenses")]

    public class Expense : FullAuditedEntity<Guid>
    {
        [Column("Name")]
        [Required]
        public string Name { get; set; }

        [Column("TypeId")]
        [Required]
        public long TypeId { get; set; }

        [Column("Number")]
        [Required]
        public long Number { get; set; }

        [Column("Budget")]
        [Required]
        public long Budget { get; set; }

        [Column("CurrencyTypeId")]
        [Required]
        public long CurrencyTypeId { get; set; }

        [Column("OccuranceMonth")]
        [Required]
        public long OccuranceMonth { get; set; }

        [Column("RelatedActivityId")]
        [Required]
        public Guid RelatedActivityId { get; set; }

        [Column("ActualCost")]
        [Required]
        public long ActualCost { get; set; }

        [Column("ActualNumber")]
        [Required]
        public long ActualNumber { get; set; }

        [ForeignKey("CurrencyTypeId")]
        public Currency CurrencyType { get; set; }

        [ForeignKey("RelatedActivityId")]
        public Activity RelatedActivity { get; set; }

        [ForeignKey("TypeId")]
        public ExpenseType Type { get; set; }
    }
}