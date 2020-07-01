using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AYCProjectBudgeting.CustomEntity;
using System;

namespace AYCProjectBudgeting.CustomDto
{
    [AutoMap(typeof(ScoreStatisticsOfFirmTotal))]
    public class ScoreStatisticsOfFirmTotalDto : EntityDto<Guid>
    {
        public float? AvgTechnicalScoreWithNulls { get; set; }

        public float? AvgTechnicalScoreWithoutNulls { get; set; }

        public float? AvgFinancialScoreWithNulls { get; set; }

        public float? AvgFinancialScoreWithoutNulls { get; set; }

        public float? StDevTechnicalScoreWithNulls { get; set; }

        public float? StDevTechnicalScoreWithoutNulls { get; set; }

        public float? StDevFinancialScoreWithNulls { get; set; }

        public float? StDevFinancialScoreWithoutNulls { get; set; }

        public float? AvgDiscount { get; set; }

        public float? StDevDiscount { get; set; }

        public float? MaximumDiscount { get; set; }

        public float? MinimumDiscount { get; set; }

    }
}
