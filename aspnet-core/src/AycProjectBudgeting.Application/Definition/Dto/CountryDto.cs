using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    [AutoMap(typeof(Country))]
    public class CountryDto : FullAuditedEntityDto<long>
    {
        [Required]
        public string CountryName { get; set; }
    }
}
