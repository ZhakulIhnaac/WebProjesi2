using Domain.Dto;
using System.Collections.Generic;

namespace AYCProjectBudgeting.CustomDto
{
    public class ActivityExpenseListDto
    {
        public ActivityDto Activity { get; set; }
        public long ActivityPlannedTotalCost { get; set; }
        public long ActivityActualTotalCost { get; set; }
        public List<ExpenseDto> ExpenseList { get; set; }
    }

}
