using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    [AutoMap(typeof(Expense))]
    public class ExpenseDto : FullAuditedEntityDto<Guid>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public long TypeId { get; set; }

        [Required]
        public long Number { get; set; }

        [Required]
        public long Budget { get; set; }

        [Required]
        public long OccuranceMonth { get; set; }

        [Required]
        public long CurrencyTypeId { get; set; }

        [Required]
        public Guid RelatedActivityId { get; set; }

        [Required]
        public long ActualCost { get; set; }

        [Required]
        public long ActualNumber { get; set; }

        public Currency CurrencyType { get; set; }

        public Activity RelatedActivity { get; set; }

        public ExpenseType Type { get; set; }
    }
}
