using Abp.Application.Services;
using AycProjectBudgeting.MultiTenancy.Dto;

namespace AycProjectBudgeting.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

