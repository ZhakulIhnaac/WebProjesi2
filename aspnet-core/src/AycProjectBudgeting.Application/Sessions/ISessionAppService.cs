using System.Threading.Tasks;
using Abp.Application.Services;
using AycProjectBudgeting.Sessions.Dto;

namespace AycProjectBudgeting.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
