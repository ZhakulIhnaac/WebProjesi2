using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AYCProjectBudgeting.CustomEntity;
using System;

namespace AYCProjectBudgeting.CustomDto
{
    public class MutualTendersComparisonDto : EntityDto<Guid>
    {
        public double Year { get; set; }

        public string TenderNumber { get; set; }

        public string TenderName { get; set; }

        public long FirstFirmRole { get; set; }

        public long SecondFirmRole { get; set; }

    }
}
