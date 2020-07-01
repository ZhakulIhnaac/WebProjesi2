using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("tt_expense_type")]
    public class ExpenseType : FullAuditedEntity<long>
    {
        [Column("Name")]
        [Required]
        public string Name { get; set; }

        [Column("Description")]
        [Required]
        public string Description { get; set; }

        [Column("EndType")]
        [Required]
        public bool EndType { get; set; }

        [Column("ParentExpenseTypeId")]
        public long? ParentExpenseTypeId { get; set; }

        [ForeignKey("ParentExpenseTypeId")]
        public ExpenseType ParentExpenseType { get; set; }

    }
}
