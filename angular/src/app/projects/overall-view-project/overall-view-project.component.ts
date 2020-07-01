import { Component, Inject, Injector, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AppComponentBase } from '@shared/app-component-base';
import { ActivityExpenseListDto, ExpenseTypeMonthlyCostListDto, ProjectBudgetingServiceProxy, MonthExpenseCoupleDto, ProjectOverviewDto, TypeParentTypeDto } from '@shared/service-proxies/service-proxies';
import { ChartDataSets } from 'chart.js';
import { Color, Label } from 'ng2-charts';
import { EChartColors } from '../../../shared/AppEnums';

@Component({
    templateUrl: './overall-view-project.component.html'
})

export class ProjectOverallViewDialogComponent extends AppComponentBase {

    monthlyTotalCosts: MonthExpenseCoupleDto[] = [];
    monthsList: number[] = [];
    projectDuration: number;
    plannedTotalCost: number;
    actualTotalCost: number;
    activityList: ActivityExpenseListDto[] = [];
    expenseTypeMonthlyCostList: TypeParentTypeDto[] = [];
    actualExpenseParentTypeList: any[] = [];
    plannedExpenseParentTypeList: any[] = [];

    //Planned Monthly Expenses
    plannedMonthlyExpenseBarChartReady: boolean = false;
    plannedMonthlyExpenseBarChartDataset: ChartDataSets[];
    plannedMonthlyExpenseBarChartLabel: Label[];

    //Planned Monthly Cumulative Expenses
    plannedMonthlyCumulativeExpenseBarChartReady: boolean = false;
    plannedMonthlyCumulativeExpenseBarChartDataset: ChartDataSets[];
    plannedMonthlyCumulativeExpenseBarChartLabel: Label[];

    //Planned Expense By Activity
    plannedExpensesByActivityChartReady: boolean = false;
    plannedExpensesByActivityChartDataset: ChartDataSets[];
    plannedExpensesByActivityChartLabel: Label[];

    //Planned Expenses By Type
    plannedExpensesByTypeChartReady: boolean = false;
    plannedExpensesByTypeChartDataset: ChartDataSets[];
    plannedExpensesByTypeChartLabel: Label[];

    //Actual Monthly Expenses
    actualMonthlyExpenseBarChartReady: boolean = false;
    actualMonthlyExpenseBarChartDataset: ChartDataSets[];
    actualMonthlyExpenseBarChartLabel: Label[];

    //Actual Monthly Cumulative Expenses
    actualMonthlyCumulativeExpenseBarChartReady: boolean = false;
    actualMonthlyCumulativeExpenseBarChartDataset: ChartDataSets[];
    actualMonthlyCumulativeExpenseBarChartLabel: Label[];

    //Actual Expense By Activity
    actualExpensesByActivityChartReady: boolean = false;
    actualExpensesByActivityChartDataset: ChartDataSets[];
    actualExpensesByActivityChartLabel: Label[];

    //Actual Expenses By Type
    actualExpensesByTypeChartReady: boolean = false;
    actualExpensesByTypeChartDataset: ChartDataSets[];
    actualExpensesByTypeChartLabel: Label[];

    //Default LineChart Attributes
    defaultLineChartType = 'line';
    defaultLineChartOptions = { responsive: true, aspectRatio: 3.0 };
    defaultLineChartColors: Color[] = [
        { borderColor: ['rgba(55, 192, 210, 1)'], backgroundColor: ['rgba(55, 192, 210, 0.5)'] },
        { borderColor: ['rgba(210, 55, 55, 1)'], backgroundColor: ['rgba(210, 55, 55, 0.5)'] }
    ];

    //Default Doughnut Chart Attributes
    defaultDoughnutChartType = 'doughnut';
    defaultDoughnutChartOptions = { responsive: true, legend: { display: false } };
    defaultDoughnutChartColors: Color[] = [{ borderColor: EChartColors.lines, backgroundColor: EChartColors.areas }];

    //Default Bar Chart Attributes
    defaultBarChartType = 'bar';
    defaultBarChartOptions = { responsive: true, legend: { display: false } };
    defaultBarChartColors: Color[] = [{ borderColor: 'rgba(55, 192, 210, 1)', backgroundColor: 'rgba(55, 192, 210, 0.5)' }];

    //Default Horizontal Bar Chart Attributes
    defaultHorizontalBarChartType = 'horizontalBar';
    defaultHorizontalBarChartOptions = { responsive: true, legend: { display: false } };
    defaultHorizontalBarChartColors: Color[] = [{ borderColor: 'rgba(55, 192, 210, 1)', backgroundColor: 'rgba(55, 192, 210, 0.5)' }];

    //Default Chart Attributes
    defaultChartLegend = true;
    defaultChartPlugins = [];
    defaultChartAspectRatio = 0.5;

    constructor(
        injector: Injector,
        public _projectBudgetingService: ProjectBudgetingServiceProxy,
        private _dialogRef: MatDialogRef<ProjectOverallViewDialogComponent>,
        @Optional() @Inject(MAT_DIALOG_DATA) private _data: { projectId, projectName }
    ) {
        super(injector);
        this.projectOverview();
    }

    projectOverview() {
        this._projectBudgetingService.projectOverview(this._data.projectId).subscribe(result => {
            this.monthlyTotalCosts = result.monthlyTotalCosts;
            this.projectDuration = result.projectDuration;
            this.activityList = result.activitiesWithExpenses;
            this.expenseTypeMonthlyCostList = result.expenseTypeMonthlyCostList;

            this.plannedTotalCost = result.monthlyTotalCosts.map(x => x.plannedTotalExpense).reduce((a, b) => a + b, 0);
            this.actualTotalCost = result.monthlyTotalCosts.map(x => x.actualTotalExpense).reduce((a, b) => a + b, 0);
            this.plannedDataImplement(result);
            this.actualDataImplement(result);
            this.monthsList = [...Array(this.projectDuration).keys()].map(i => i + 1);
            console.log(this.monthsList);

            console.log(result);
        });
    }

    plannedDataImplement(result: ProjectOverviewDto) {

        // Monthly Expense Bar Chart Dataset
        this.plannedMonthlyExpenseBarChartDataset = [{
            data: result.monthlyTotalCosts.map(x => x.plannedTotalExpense), label: this.l('PlannedCost')
        }];

        this.plannedMonthlyExpenseBarChartLabel = result.monthlyTotalCosts.map(x => x.month.toString());
        this.plannedMonthlyExpenseBarChartReady = true;

        // Cumulative Monthly Expense Bar Chart Dataset
        this.plannedMonthlyCumulativeExpenseBarChartDataset = [{
            data: result.monthlyTotalCosts.map(x => x.plannedTotalExpense).map((i, index) => {
                return result.monthlyTotalCosts.map(x => x.plannedTotalExpense).slice(0, index + 1).reduce((a, b) => a + b, 0)
            }), label: this.l('PlannedCumulativeCost'), lineTension: 0
        }];

        this.plannedMonthlyCumulativeExpenseBarChartLabel = [...Array(this.projectDuration).keys()].map(i => (i + 1).toString());
        this.plannedMonthlyCumulativeExpenseBarChartReady = true;

        // Expenses By Activity Bar Chart Dataset
        this.plannedExpensesByActivityChartDataset = [{
            data: this.activityList.map(x => { return x.activityPlannedTotalCost }), label: this.l('ActivityCosts')
        }];

        this.plannedExpensesByActivityChartLabel = this.activityList.map(x => { return x.activity.name });
        this.plannedExpensesByActivityChartReady = true;

        this.expenseTypeMonthlyCostList.forEach(x => {
            // Expenses By Type Bar Chart Dataset
            var dataset = [{
                data: x.childExpenseTypeList.map(x => { return x.expenseTypePlannedTotalCost })
            }];

            var label = x.childExpenseTypeList.map(y => { return y.expenseType });
            var ready = true;
            this.plannedExpenseParentTypeList.push({ dataset: dataset, label: label, ready: ready, parentType: x.parentType });
        })

    }

    actualDataImplement(result: ProjectOverviewDto) {

        // Monthly Expense Bar Chart Dataset
        this.actualMonthlyExpenseBarChartDataset = [{
            data: result.monthlyTotalCosts.map(x => x.actualTotalExpense), label: this.l('ActualCost')
        }];

        this.actualMonthlyExpenseBarChartLabel = result.monthlyTotalCosts.map(x => x.month.toString());
        this.actualMonthlyExpenseBarChartReady = true;

        // Cumulative Monthly Expense Bar Chart Dataset
        this.actualMonthlyCumulativeExpenseBarChartDataset = [{
            data: result.monthlyTotalCosts.map(x => x.actualTotalExpense).map((i, index) => {
                return result.monthlyTotalCosts.map(x => x.actualTotalExpense).slice(0, index + 1).reduce((a, b) => a + b, 0)
            }), label: this.l('ActualCumulativeCost'), lineTension: 0
        }];

        this.actualMonthlyCumulativeExpenseBarChartLabel = [...Array(this.projectDuration).keys()].map(i => (i + 1).toString());
        this.actualMonthlyCumulativeExpenseBarChartReady = true;

        // Expenses By Activity Bar Chart Dataset
        this.actualExpensesByActivityChartDataset = [{
            data: this.activityList.map(x => { return x.activityActualTotalCost }), label: this.l('ActivityCosts')
        }];

        this.actualExpensesByActivityChartLabel = this.activityList.map(x => { return x.activity.name });
        this.actualExpensesByActivityChartReady = true;

        this.expenseTypeMonthlyCostList.forEach(x => {
            // Expenses By Type Bar Chart Dataset
            var dataset = [{
                data: x.childExpenseTypeList.map(x => { return x.expenseTypeActualTotalCost })
            }];

            var label = x.childExpenseTypeList.map(y => { return y.expenseType });
            var ready = true;
            this.actualExpenseParentTypeList.push({ dataset: dataset, label: label, ready: ready, parentType: x.parentType });
        })

    }

    close(result: any): void {
        this._dialogRef.close(result);
    }
}
