using Abp.Application.Services;
using Abp.Domain.Repositories;
using AYCProjectBudgeting.CustomClasses;
using AYCProjectBudgeting.CustomConstructs.ListResult;
using AYCProjectBudgeting.CustomDto;
using Domain.Dto;
using Domain.Entity;
using IProjectBudgetingAppServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ProjectBudgetingAppServices
{
    public class ProjectBudgetingAppService : ApplicationService, IProjectBudgetingAppService
    {
        private readonly IRepository<Activity, Guid> _activityRepository;
        private readonly IRepository<Expense, Guid> _expenseRepository;
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IRepository<ExpertChannel, Guid> _expertChannelRepository;
        private readonly IRepository<ExpertProjectAssignment, Guid> _expertProjectAssignmentRepository;
        private readonly IRepository<Expert, Guid> _expertRepository;

        public ProjectBudgetingAppService(
            IRepository<Activity, Guid> activityRepository,
            IRepository<Expense, Guid> expenseRepository,
            IRepository<Project, Guid> projectRepository,
            IRepository<ExpertChannel, Guid> expertChannelRepository,
            IRepository<ExpertProjectAssignment, Guid> expertProjectAssignmentRepository,
            IRepository<Expert, Guid> expertRepository
            )
        {
            _activityRepository = activityRepository;
            _expenseRepository = expenseRepository;
            _projectRepository = projectRepository;
            _expertChannelRepository = expertChannelRepository;
            _expertProjectAssignmentRepository = expertProjectAssignmentRepository;
            _expertRepository = expertRepository;
        }

        #region Activity

        [HttpPost]
        public async Task<ListResultDto<ActivityDto>> GetActivitiesForTable(int maxResult, int page, SearchParamList searchParamList)
        {
            var res = _activityRepository.GetAllIncluding(x => x.RelatedProject).Where(LinqBuilder.WhereStatementBuilder(searchParamList)).OrderBy(x => x.Name);
            var activitiesList = res.Skip((page - 1) * maxResult).Take(maxResult);
            var totalItems = res.Count();

            ListResultDto<ActivityDto> result = new ListResultDto<ActivityDto>
            {
                totalItemCount = totalItems,
                itemList = ObjectMapper.Map<List<ActivityDto>>(activitiesList)
            };

            return result;
        }

        [HttpPost]
        public async Task<List<ActivityDto>> GetActivitiesForSelectBox()
        {
            var activitiesList = _activityRepository.GetAllIncluding(x => x.RelatedProject).OrderBy(x => x.Name);
            return ObjectMapper.Map<List<ActivityDto>>(activitiesList);
        }

        [HttpPost]
        public async Task<List<ActivityDto>> GetActivitiesOfProject(Guid projectId)
        {
            var activitiesList = _activityRepository.GetAllIncluding(x => x.RelatedProject).Where(x => x.RelatedProjectId == projectId).OrderByDescending(x => x.EndingMonth);
            return ObjectMapper.Map<List<ActivityDto>>(activitiesList);
        }

        [HttpGet]
        public async Task<ActivityDto> FindActivity(Guid activityId)
        {
            var activity = _activityRepository.GetAllIncluding(x => x.RelatedProject).Where(x => x.Id == activityId).FirstOrDefault();
            return ObjectMapper.Map<ActivityDto>(activity);
        }

        [HttpPost]
        public async void AddActivity(ActivityDto activity)
        {
            await _activityRepository.InsertAndGetIdAsync(ObjectMapper.Map<Activity>(activity));
        }

        [HttpPost]
        public async void UpdateActivity(ActivityDto activity)
        {
            await _activityRepository.UpdateAsync(ObjectMapper.Map<Activity>(activity));
        }

        [HttpGet]
        public async void DeleteActivity(Guid activityId)
        {
            await _activityRepository.DeleteAsync(activityId);
        }

        public async Task<List<ActivityExpenseListDto>> GetActivitiesWithExpenses(Guid projectId)
        {
            var expenseList = _expenseRepository.GetAllIncluding(x => x.RelatedActivity).Where(x => x.RelatedActivity.RelatedProjectId == projectId).ToList();
            List<ActivityExpenseListDto> activityList = new List<ActivityExpenseListDto>();
            foreach (var activity in expenseList.Select(x => x.RelatedActivity).Distinct().ToList())
            {
                ActivityExpenseListDto activityExpenseList = new ActivityExpenseListDto
                {
                    Activity = new ActivityDto(),
                    ExpenseList = new List<ExpenseDto>()
                };

                activityExpenseList.Activity = ObjectMapper.Map<ActivityDto>(activity);

                foreach (var expense in expenseList.Where(x => x.RelatedActivityId == activity.Id))
                {
                    activityExpenseList.ExpenseList.Add(ObjectMapper.Map<ExpenseDto>(expense));
                }

                activityList.Add(activityExpenseList);
            }

            return activityList;
        }

        [HttpPost]
        public async void UpdateExpensesInExecution(ExpenseDto[] expenseDtos)
        {
            foreach (var expense in expenseDtos)
            {
                expense.RelatedActivity = null;
                _expenseRepository.UpdateAsync(ObjectMapper.Map<Expense>(expense)).Wait();
            }
        }

        #endregion

        #region Expense

        [HttpPost]
        public async Task<ListResultDto<ExpenseDto>> GetExpensesForTable(int maxResult, int page, SearchParamList searchParamList)
        {
            var res = _expenseRepository.GetAllIncluding(x => x.CurrencyType, x => x.RelatedActivity, x => x.Type).Where(LinqBuilder.WhereStatementBuilder(searchParamList)).OrderBy(x => x.Name);
            var expensesList = res.Skip((page - 1) * maxResult).Take(maxResult);
            var totalItems = res.Count();

            ListResultDto<ExpenseDto> result = new ListResultDto<ExpenseDto>
            {
                totalItemCount = totalItems,
                itemList = ObjectMapper.Map<List<ExpenseDto>>(expensesList)
            };

            return result;
        }

        [HttpPost]
        public async Task<List<ExpenseDto>> GetExpensesOfActivity(Guid activityId)
        {
            var expenseList = _expenseRepository.GetAllIncluding(x => x.Type).Where(x => x.RelatedActivityId == activityId).ToList();
            return ObjectMapper.Map<List<ExpenseDto>>(expenseList);
        }

        [HttpPost]
        public async Task<List<ExpenseDto>> GetExpensesForSelectBox()
        {
            var expensesList = _expenseRepository.GetAllIncluding(x => x.CurrencyType, x => x.RelatedActivity, x => x.Type).OrderBy(x => x.Name);
            return ObjectMapper.Map<List<ExpenseDto>>(expensesList);
        }

        [HttpGet]
        public async Task<ExpenseDto> FindExpense(Guid expenseId)
        {
            var expense = _expenseRepository.GetAllIncluding(x => x.CurrencyType, x => x.RelatedActivity, x => x.Type).Where(x => x.Id == expenseId).FirstOrDefault();
            return ObjectMapper.Map<ExpenseDto>(expense);
        }

        [HttpPost]
        public async void AddExpense(ExpenseDto expense)
        {
            await _expenseRepository.InsertAndGetIdAsync(ObjectMapper.Map<Expense>(expense));
        }

        [HttpPost]
        public async void UpdateExpense(ExpenseDto expense)
        {
            await _expenseRepository.UpdateAsync(ObjectMapper.Map<Expense>(expense));
        }

        [HttpGet]
        public async void DeleteExpense(Guid expenseId)
        {
            await _expenseRepository.DeleteAsync(expenseId);
        }

        #endregion

        #region Project

        [HttpPost]
        public async Task<ListResultDto<ProjectDto>> GetProjectsForTable(int maxResult, int page, SearchParamList searchParamList)
        {
            var res = _projectRepository.GetAllIncluding().Where(LinqBuilder.WhereStatementBuilder(searchParamList)).OrderBy(x => x.Name);
            var projectsList = res.Skip((page - 1) * maxResult).Take(maxResult);
            var totalItems = res.Count();

            ListResultDto<ProjectDto> result = new ListResultDto<ProjectDto>
            {
                totalItemCount = totalItems,
                itemList = ObjectMapper.Map<List<ProjectDto>>(projectsList)
            };

            return result;
        }

        [HttpPost]
        public async Task<List<ProjectDto>> GetProjectsForSelectBox()
        {
            var projectsList = _projectRepository.GetAllIncluding().OrderBy(x => x.Name);
            return ObjectMapper.Map<List<ProjectDto>>(projectsList);
        }

        [HttpGet]
        public async Task<ProjectDto> FindProject(Guid projectId)
        {
            var project = _projectRepository.GetAllIncluding().Where(x => x.Id == projectId).FirstOrDefault();
            return ObjectMapper.Map<ProjectDto>(project);
        }

        [HttpPost]
        public async void AddProject(ProjectDto project)
        {
            await _projectRepository.InsertAndGetIdAsync(ObjectMapper.Map<Project>(project));
        }

        [HttpPost]
        public async void UpdateProject(ProjectDto project)
        {
            await _projectRepository.UpdateAsync(ObjectMapper.Map<Project>(project));
        }

        [HttpGet]
        public async void DeleteProject(Guid projectId)
        {
            await _projectRepository.DeleteAsync(projectId);
        }

        public async Task<ProjectOverviewDto> ProjectOverview(Guid projectId)
        {
            var expenseList = _expenseRepository.GetAllIncluding(x => x.Type, x => x.Type.ParentExpenseType, x => x.RelatedActivity, x => x.RelatedActivity.RelatedProject, x => x.CurrencyType).Where(x => x.RelatedActivity.RelatedProjectId == projectId).ToList();
            var projectDuration = _projectRepository.FirstOrDefault(x => x.Id == projectId).Duration;
            long monthlySumActual = 0;
            long monthlySumPlanned = 0;

            ProjectOverviewDto projectOverview = new ProjectOverviewDto
            {
                MonthlyTotalCosts = new List<MonthExpenseCoupleDto>(),
                ActivitiesWithExpenses = new List<ActivityExpenseListDto>(),
                ExpenseTypeMonthlyCostList = new List<TypeParentTypeDto>()
            };

            for (int i = 1; i <= projectDuration; i++)
            {
                monthlySumActual = 0;
                monthlySumPlanned = 0;
                foreach (var expense in expenseList.Where(x => x.OccuranceMonth == i))
                {
                    monthlySumActual += expense.ActualCost * expense.ActualNumber;
                    monthlySumPlanned += expense.Budget * expense.Number;
                }
                projectOverview.MonthlyTotalCosts.Add(
                    new MonthExpenseCoupleDto {
                        Month = i,
                        PlannedTotalExpense = monthlySumPlanned,
                        ActualTotalExpense = monthlySumActual,
                    }) ;
            }

            projectOverview.ProjectDuration = projectDuration;

            foreach (var activity in expenseList.Select(x => x.RelatedActivity).Distinct().ToList())
            {
                ActivityExpenseListDto activityExpenseListDto = new ActivityExpenseListDto
                {
                    Activity = new ActivityDto(),
                    ExpenseList = new List<ExpenseDto>(),
                };

                activityExpenseListDto.Activity = ObjectMapper.Map<ActivityDto>(activity);
                activityExpenseListDto.ActivityPlannedTotalCost = 0;
                activityExpenseListDto.ActivityActualTotalCost = 0;

                foreach (var expense in expenseList.Where(x => x.RelatedActivityId == activity.Id))
                {
                    activityExpenseListDto.ExpenseList.Add(ObjectMapper.Map<ExpenseDto>(expense));
                    activityExpenseListDto.ActivityPlannedTotalCost += expense.Number * expense.Budget;
                    activityExpenseListDto.ActivityActualTotalCost += expense.ActualNumber * expense.ActualCost;
                }

                projectOverview.ActivitiesWithExpenses.Add(activityExpenseListDto);
            }

            foreach (var expenseParentType in expenseList.Select(x => x.Type.ParentExpenseType).Distinct().ToList())
            {

                TypeParentTypeDto typeParentType = new TypeParentTypeDto
                {
                    ParentType = expenseParentType.Name,
                    ChildExpenseTypeList = new List<ExpenseTypeMonthlyCostListDto>()
                };

                foreach (var expenseType in expenseList.Where(x => x.Type.ParentExpenseTypeId == expenseParentType.Id).Select(x => x.Type).Distinct().ToList())
                {
                    long expenseTypePlannedMonthlySum = 0;
                    long expenseTypeActualMonthlySum = 0;

                    ExpenseTypeMonthlyCostListDto expenseTypeMonthlyCost = new ExpenseTypeMonthlyCostListDto
                    {
                        MonthlyCostList = new List<ActualPlannedMonthlyCostPairsDto>()
                    };

                    expenseTypeMonthlyCost.ExpenseType = expenseType.Name;

                    for (int i = 1; i <= projectDuration; i++)
                    {
                        ActualPlannedMonthlyCostPairsDto actualPlannedMonthlyCostPairs = new ActualPlannedMonthlyCostPairsDto
                        {
                            Planned = expenseList.Where(x => x.OccuranceMonth == i && x.Type.Name == expenseType.Name).Select(x => x.Number * x.Budget).Sum(),
                            Actual = expenseList.Where(x => x.OccuranceMonth == i && x.Type.Name == expenseType.Name).Select(x => x.ActualNumber * x.ActualCost).Sum(),
                        };

                        expenseTypeMonthlyCost.MonthlyCostList.Add(actualPlannedMonthlyCostPairs);
                        expenseTypePlannedMonthlySum += actualPlannedMonthlyCostPairs.Planned;
                        expenseTypeActualMonthlySum += actualPlannedMonthlyCostPairs.Actual;

                    }

                    expenseTypeMonthlyCost.ExpenseTypePlannedTotalCost = expenseTypePlannedMonthlySum;
                    expenseTypeMonthlyCost.ExpenseTypeActualTotalCost = expenseTypeActualMonthlySum;
                    
                    typeParentType.ChildExpenseTypeList.Add(expenseTypeMonthlyCost);

                }

                projectOverview.ExpenseTypeMonthlyCostList.Add(typeParentType);

            }

            return projectOverview;

        }

        #endregion

        #region ExpertChannel

        [HttpPost]
        public async Task<List<ExpertChannelDto>> GetExpertChannelByExpertIdForTable(Guid expertId)
        {
            var expertChannelList = _expertChannelRepository.GetAllIncluding(x => x.Expert, x => x.ChannelType).Where(x => x.ExpertId == expertId);
            return ObjectMapper.Map<List<ExpertChannelDto>>(expertChannelList);
        }

        [HttpGet]
        public async Task<ExpertChannelDto> FindExpertChannel(Guid expertChannelId)
        {
            var expertChannel = await _expertChannelRepository.FirstOrDefaultAsync(x => x.Id == expertChannelId);
            return ObjectMapper.Map<ExpertChannelDto>(expertChannel);
        }

        [HttpPost]
        public async void AddExpertChannel(ExpertChannelDto expertChannel)
        {
            await _expertChannelRepository.InsertAndGetIdAsync(ObjectMapper.Map<ExpertChannel>(expertChannel));
        }

        [HttpPost]
        public async void UpdateExpertChannel(ExpertChannelDto expertChannel)
        {
            await _expertChannelRepository.UpdateAsync(ObjectMapper.Map<ExpertChannel>(expertChannel));
        }

        [HttpGet]
        public async void DeleteExpertChannel(Guid expertChannelId)
        {
            await _expertChannelRepository.DeleteAsync(expertChannelId);
        }

        #endregion

        #region ExpertProjectAssignment

        [HttpPost]
        public async Task<ListResultDto<ExpertProjectAssignmentDto>> GetExpertProjectAssignmentsForTable(int maxResult, int page, SearchParamList searchParamList)
        {
            var res = _expertProjectAssignmentRepository.GetAllIncluding(x => x.Expert, x => x.Project, x => x.CurrencyType).Where(LinqBuilder.WhereStatementBuilder(searchParamList)).OrderBy(x => x.Expert.Name);
            var expertProjectAssignmentsList = res.Skip((page - 1) * maxResult).Take(maxResult);
            var totalItems = res.Count();

            ListResultDto<ExpertProjectAssignmentDto> result = new ListResultDto<ExpertProjectAssignmentDto>
            {
                totalItemCount = totalItems,
                itemList = ObjectMapper.Map<List<ExpertProjectAssignmentDto>>(expertProjectAssignmentsList)
            };

            return result;
        }

        [HttpPost]
        public async Task<List<ExpertProjectAssignmentDto>> GetExpertProjectAssignmentsForSelectBox()
        {
            var expertProjectAssignmentsList = _expertProjectAssignmentRepository.GetAllIncluding(x => x.Expert, x => x.Project, x => x.CurrencyType).OrderBy(x => x.Expert.Name);
            return ObjectMapper.Map<List<ExpertProjectAssignmentDto>>(expertProjectAssignmentsList);
        }

        [HttpGet]
        public async Task<ExpertProjectAssignmentDto> FindExpertProjectAssignment(Guid expertProjectAssignmentId)
        {
            var expertProjectAssignment = _expertProjectAssignmentRepository.GetAllIncluding().Where(x => x.Id == expertProjectAssignmentId).FirstOrDefault();
            return ObjectMapper.Map<ExpertProjectAssignmentDto>(expertProjectAssignment);
        }

        [HttpPost]
        public async void AddExpertProjectAssignment(ExpertProjectAssignmentDto expertProjectAssignment)
        {
            await _expertProjectAssignmentRepository.InsertAndGetIdAsync(ObjectMapper.Map<ExpertProjectAssignment>(expertProjectAssignment));
        }

        [HttpPost]
        public async void UpdateExpertProjectAssignment(ExpertProjectAssignmentDto expertProjectAssignment)
        {
            await _expertProjectAssignmentRepository.UpdateAsync(ObjectMapper.Map<ExpertProjectAssignment>(expertProjectAssignment));
        }

        [HttpGet]
        public async void DeleteExpertProjectAssignment(Guid expertProjectAssignmentId)
        {
            await _expertProjectAssignmentRepository.DeleteAsync(expertProjectAssignmentId);
        }

        #endregion

        #region Experts

        [HttpPost]
        public async Task<ListResultDto<ExpertDto>> GetExpertForTable(int maxResult, int page, SearchParamList searchParamList)
        {
            var res = _expertRepository.GetAllIncluding(x => x.Nationality).Where(LinqBuilder.WhereStatementBuilder(searchParamList));
            var expertList = res.Skip((page - 1) * maxResult).Take(maxResult).OrderBy(x => x.Name);
            var totalItems = res.Count();

            ListResultDto<ExpertDto> result = new ListResultDto<ExpertDto>();
            result.totalItemCount = totalItems;
            result.itemList = ObjectMapper.Map<List<ExpertDto>>(expertList);
            return result;
        }

        [HttpPost]
        public async Task<List<ExpertDto>> GetExpertForSelectBox()
        {
            var expertList = await _expertRepository.GetAllListAsync();
            expertList.OrderBy(x => x.Name);
            return ObjectMapper.Map<List<ExpertDto>>(expertList);
        }

        [HttpGet]
        public async Task<ExpertDto> FindExpert(Guid expertId)
        {
            var expert = await _expertRepository.FirstOrDefaultAsync(x => x.Id == expertId);
            return ObjectMapper.Map<ExpertDto>(expert);
        }

        [HttpPost]
        public async void AddExpert(ExpertDto expert)
        {
            await _expertRepository.InsertAndGetIdAsync(ObjectMapper.Map<Expert>(expert));
        }

        [HttpPost]
        public async void UpdateExpert(ExpertDto expert)
        {
            await _expertRepository.UpdateAsync(ObjectMapper.Map<Expert>(expert));
        }

        [HttpGet]
        public async void DeleteExpert(Guid expertId)
        {
            await _expertRepository.DeleteAsync(expertId);
        }

        #endregion

    }
}