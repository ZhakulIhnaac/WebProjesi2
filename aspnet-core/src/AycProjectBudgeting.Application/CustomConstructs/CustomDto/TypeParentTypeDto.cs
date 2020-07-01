using Domain.Dto;
using System.Collections.Generic;

namespace AYCProjectBudgeting.CustomDto
{
    public class TypeParentTypeDto
    {
        public string ParentType { get; set; }
        public List<ExpenseTypeMonthlyCostListDto> ChildExpenseTypeList { get; set; }
    }

}
