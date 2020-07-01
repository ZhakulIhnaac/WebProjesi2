using Abp.AutoMapper;
using AycProjectBudgeting.Authentication.External;

namespace AycProjectBudgeting.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
