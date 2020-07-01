using System.Collections.Generic;

namespace AYCProjectBudgeting.CustomDto
{
    public class ExpenseTypeMonthlyCostListDto
    {
        public string ExpenseType { get; set; }
        public List<ActualPlannedMonthlyCostPairsDto> MonthlyCostList { get; set; }
        public long ExpenseTypePlannedTotalCost { get; set; }
        public long ExpenseTypeActualTotalCost { get; set; }
    }

}
