using Domain.Dto;
using System.Collections.Generic;

namespace AYCProjectBudgeting.CustomDto
{
    public class MonthExpenseCoupleDto
    {
        public long Month { get; set; }
        public long PlannedTotalExpense { get; set; }
        public long ActualTotalExpense { get; set; }
    }

}
