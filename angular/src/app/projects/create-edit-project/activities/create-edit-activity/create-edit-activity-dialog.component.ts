import { Component, Inject, Injector, Optional } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MAT_MOMENT_DATE_FORMATS, MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { AppComponentBase } from '@shared/app-component-base';
import { ActivityDto, ExpenseDto, ProjectBudgetingServiceProxy } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { CreateEditExpenseDialogComponent } from './expenses/create-edit-expense/create-edit-expense-dialog.component';

@Component({
    templateUrl: './create-edit-activity-dialog.component.html',
    styles: [
        `
      mat-form-field {
        width: 100%;
      }
      mat-checkbox {
        padding-bottom: 5px;
      }
    `
    ],
    providers: [
        { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
        { provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS },
    ],
})
export class CreateEditActivityDialogComponent extends AppComponentBase {
    saving = false;
    activity: ActivityDto = new ActivityDto();
    expenseList: ExpenseDto[] = [];
    expenseListFiltered: ExpenseDto[] = [];
    createEditActivityFormGroup: FormGroup;
    expenseSelectBoxFilter: string;

    constructor(
        private fb: FormBuilder,
        injector: Injector,
        public _projectBudgetingService: ProjectBudgetingServiceProxy,
        private _dialogRef: MatDialogRef<CreateEditActivityDialogComponent>,
        private _dialog: MatDialog,
        @Optional() @Inject(MAT_DIALOG_DATA) private _data: { projectDuration, projectId, activityId }
    ) {
        super(injector);
        this.createEditActivityFormGroup = this.fb.group({
            name: new FormControl('', [Validators.required, Validators.maxLength(128)]),
            startingMonth: new FormControl('', [Validators.required]),
            endingMonth: new FormControl('', [Validators.required, Validators.max(this._data.projectDuration)]),
            description: new FormControl('', [Validators.required, Validators.maxLength(2048)]),
        })
        if (this._data.activityId != null) {
            this.findActivity();
            this.getExpenses();
        }
        this.activity.relatedProjectId = this._data.projectId;
    }

    findActivity() {
        this._projectBudgetingService.findActivity(this._data.activityId).subscribe(result => {
            this.activity = result;
        });
    }

    filterExpenseName() {
        this.expenseListFiltered = this.expenseList.filter(x => x.name.toLowerCase().includes(this.expenseSelectBoxFilter.toLowerCase()));
    }

    getExpenses() {
        this._projectBudgetingService.getExpensesOfActivity(this._data.activityId).subscribe(result => {
            this.expenseList = result;
            this.expenseListFiltered = result;
        });
    }

    save(): void {
        this.saving = true;
        if (this._data.activityId != null) {
            this._projectBudgetingService
                .updateActivity(this.activity)
                .pipe(
                    finalize(() => {
                        this.saving = false;
                    })
                )
                .subscribe(() => {
                    this.notify.info(this.l('SavedSuccessfully'));
                    this.close(true);
                });
        } else {
            this._projectBudgetingService
                .addActivity(this.activity)
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

    }

    createEditDialog(activityEndingMonth: number, activityId: string, expenseId?: string): void {
        let createEditDialog;

        createEditDialog = this._dialog.open(CreateEditExpenseDialogComponent, {
            data: { activityEndingMonth, activityId, expenseId },
            width: "150vh",
            maxHeight: "90vh"
        });

        createEditDialog.afterClosed().subscribe(result => {
            if (result) {
                this.getExpenses();
            }
        });
    }

    delete(expenseId: string): void {
        abp.message.confirm(
            this.l('ExpenseDeleteWarningMessage'), undefined,
            (result: boolean) => {
                if (result) {
                    this._projectBudgetingService.deleteExpense(expenseId).subscribe(() => {
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }

    close(result: any): void {
        this._dialogRef.close(result);
    }
}
