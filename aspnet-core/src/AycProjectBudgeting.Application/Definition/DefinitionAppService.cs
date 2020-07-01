using Abp.Application.Services;
using Abp.Domain.Repositories;
using AYCProjectBudgeting.CustomClasses;
using AYCProjectBudgeting.CustomDto;
using Domain.Dto;
using Domain.Entity;
using IDefinitionAppServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace DefinitionAppServices
{
    public class DefinitionAppService : ApplicationService, IDefinitionAppService
    {
        private readonly IRepository<Currency, long> _currencyRepository;
        private readonly IRepository<ExpenseType, long> _expenseTypeRepository;
        private readonly IRepository<ChannelType, long> _channelTypeRepository;
        private readonly IRepository<Country, long> _countryRepository;

        public DefinitionAppService(
            IRepository<Currency, long> currencyRepository,
            IRepository<ExpenseType, long> expenseTypeRepository,
            IRepository<ChannelType, long> channelTypeRepository,
            IRepository<Country, long> countryRepository
            )
        {
            _currencyRepository = currencyRepository;
            _expenseTypeRepository = expenseTypeRepository;
            _channelTypeRepository = channelTypeRepository;
            _countryRepository = countryRepository;
        }

        #region Currency

        [HttpPost]
        public async Task<List<CurrencyDto>> GetCurrencyForSelectBox()
        {
            var currencyList = await _currencyRepository.GetAllListAsync();
            return ObjectMapper.Map<List<CurrencyDto>>(currencyList);
        }

        [HttpGet]
        public async Task<CurrencyDto> FindCurrency(long currencyId)
        {
            var currency = await _currencyRepository.FirstOrDefaultAsync(x => x.Id == currencyId);
            return ObjectMapper.Map<CurrencyDto>(currency);
        }

        #endregion

        #region ExpenseType

        [HttpPost]
        public async Task<List<OptionGroupDto<ExpenseTypeDto>>> GetExpenseTypeForSelectBox()
        {
            var expenseTypeChildList = _expenseTypeRepository.GetAllIncluding(x => x.ParentExpenseType).Where(x => x.EndType == true).ToList();
            var expenseTypeParentList = expenseTypeChildList.Select(x => x.ParentExpenseType).Distinct().ToList();
            var expenseTypeList = new List<OptionGroupDto<ExpenseTypeDto>>();

            foreach (var expenseTypeParent in expenseTypeParentList)
            {
                OptionGroupDto<ExpenseTypeDto> optionGroup = new OptionGroupDto<ExpenseTypeDto>
                {
                    Parent = ObjectMapper.Map<ExpenseTypeDto>(expenseTypeParent),
                    ChildList = ObjectMapper.Map<List<ExpenseTypeDto>>(expenseTypeChildList.Where(x => x.ParentExpenseTypeId == expenseTypeParent.Id))
                };
                expenseTypeList.Add(optionGroup);
            }

            return expenseTypeList;
        }

        [HttpGet]
        public async Task<ExpenseTypeDto> FindExpenseType(long expenseTypeId)
        {
            var expenseType = await _expenseTypeRepository.FirstOrDefaultAsync(x => x.Id == expenseTypeId);
            return ObjectMapper.Map<ExpenseTypeDto>(expenseType);
        }

        #endregion

        #region ChannelType

        [HttpPost]
        public async Task<List<ChannelTypeDto>> GetChannelTypeForSelectBox()
        {
            var channelTypeList = await _channelTypeRepository.GetAllListAsync();
            return ObjectMapper.Map<List<ChannelTypeDto>>(channelTypeList);
        }

        [HttpGet]
        public async Task<ChannelTypeDto> FindChannelType(long channelTypeId)
        {
            var channelType = await _channelTypeRepository.FirstOrDefaultAsync(x => x.Id == channelTypeId);
            return ObjectMapper.Map<ChannelTypeDto>(channelType);
        }

        #endregion

        #region Country

        [HttpPost]
        public async Task<List<CountryDto>> GetCountryForSelectBox()
        {
            var countryList = await _countryRepository.GetAllListAsync();
            return ObjectMapper.Map<List<CountryDto>>(countryList);
        }

        [HttpGet]
        public async Task<CountryDto> FindCountry(long countryId)
        {
            var country = await _countryRepository.FirstOrDefaultAsync(x => x.Id == countryId);
            return ObjectMapper.Map<CountryDto>(country);
        }

        #endregion

    }
}