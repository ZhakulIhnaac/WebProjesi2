import { Component, Inject, Injector, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { AppComponentBase } from '@shared/app-component-base';
import { ActivityExpenseListDto, ProjectBudgetingServiceProxy, ExpenseDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { CreateEditExpenseDialogComponent } from '../create-edit-project/activities/create-edit-activity/expenses/create-edit-expense/create-edit-expense-dialog.component';

@Component({
    templateUrl: './create-edit-project-execution-dialog.component.html',
    styles: [
        `
      mat-form-field {
        width: 100%;
      }
      mat-checkbox {
        padding-bottom: 5px;
      }
    `
    ]
})
export class ProjectExecutionDialogComponent extends AppComponentBase {
    saving = false;
    activityList: ActivityExpenseListDto[] = [];
    expenseListToUpdate: ExpenseDto[] = [];
    activitySelectBoxFilter: string;

    constructor(
        injector: Injector,
        public _projectBudgetingService: ProjectBudgetingServiceProxy,
        private _dialogRef: MatDialogRef<ProjectExecutionDialogComponent>,
        private _dialog: MatDialog,
        @Optional() @Inject(MAT_DIALOG_DATA) private _data: { projectId }
    ) {
        super(injector);
        this.getExpenses();
    }

    getExpenses() {
        this._projectBudgetingService.getActivitiesWithExpenses(this._data.projectId).subscribe(result => {
            this.activityList = result;
            console.log(this.activityList);
        });
    }

    addExpenseDialog(activityEndingMonth: number, activityId: string): void {
        let createEditDialog;

        createEditDialog = this._dialog.open(CreateEditExpenseDialogComponent, {
            data: { activityEndingMonth, activityId },
            width: "150vh",
            maxHeight: "90vh"
        });

        createEditDialog.afterClosed().subscribe(result => {
            if (result) {
                this.getExpenses();
            }
        });
    }

    save(): void {
        this.activityList.forEach(x => { x.expenseList.forEach(y => this.expenseListToUpdate.push(y)) })
        this.saving = true;
        this._projectBudgetingService
            .updateExpensesInExecution(this.expenseListToUpdate)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close(true);
            });
    }

    close(result: any): void {
        this._dialogRef.close(result);
    }
}
