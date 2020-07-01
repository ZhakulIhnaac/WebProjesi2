using System.Collections.Generic;

namespace AYCProjectBudgeting.CustomDto
{
    public class ProjectOverviewDto
    {
        public List<MonthExpenseCoupleDto> MonthlyTotalCosts { get; set; }

        public long ProjectDuration { get; set; }

        public List<ActivityExpenseListDto> ActivitiesWithExpenses { get; set; }

        public List<TypeParentTypeDto> ExpenseTypeMonthlyCostList { get; set; }
    }
}
