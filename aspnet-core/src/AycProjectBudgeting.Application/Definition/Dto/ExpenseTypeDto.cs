using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    [AutoMap(typeof(ExpenseType))]
    public class ExpenseTypeDto : FullAuditedEntityDto<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool EndType { get; set; }

        public long? ParentExpenseTypeId { get; set; }

        public ExpenseTypeDto ParentExpenseType { get; set; }
    }
}
