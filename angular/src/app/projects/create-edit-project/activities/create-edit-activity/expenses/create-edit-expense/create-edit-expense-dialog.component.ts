import { Component, Inject, Injector, Optional } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MAT_MOMENT_DATE_FORMATS, MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { AppComponentBase } from '@shared/app-component-base';
import { ExpenseDto, ProjectBudgetingServiceProxy, CurrencyDto, DefinitionServiceProxy, ExpenseTypeDto, ExpenseTypeDtoOptionGroupDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';

@Component({
    templateUrl: './create-edit-expense-dialog.component.html',
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
export class CreateEditExpenseDialogComponent extends AppComponentBase {
    saving = false;
    expense: ExpenseDto = new ExpenseDto();
    expenseTypeGroupList: ExpenseTypeDtoOptionGroupDto[] = [];
    currencyList: CurrencyDto[] = [];
    createEditExpenseFormGroup: FormGroup;

    constructor(
        private fb: FormBuilder,
        injector: Injector,
        public _projectBudgetingService: ProjectBudgetingServiceProxy,
        public _definitionService: DefinitionServiceProxy,
        private _dialogRef: MatDialogRef<CreateEditExpenseDialogComponent>,
        @Optional() @Inject(MAT_DIALOG_DATA) private _data: { activityEndingMonth, activityId, expenseId }
    ) {
        super(injector);
        this.createEditExpenseFormGroup = this.fb.group({
            name: new FormControl('', [Validators.required, Validators.maxLength(128)]),
            typeId: new FormControl('', [Validators.required]),
            budget: new FormControl('', [Validators.required]),
            number: new FormControl('', [Validators.required]),
            currency: new FormControl('', [Validators.required]),
        })
        if (this._data.expenseId != null) {
            this.findExpense();
        }
        this.getCurrency();
        this.getExpenseType();
        this.expense.relatedActivityId = this._data.activityId;
        this.expense.occuranceMonth = this._data.activityEndingMonth;
    }

    findExpense() {
        this._projectBudgetingService.findExpense(this._data.expenseId).subscribe(result => {
            this.expense = result;
        });
    }

    getCurrency() {
        this._definitionService.getCurrencyForSelectBox().subscribe(result => {
            this.currencyList = result;
            this.expense.currencyTypeId = result.find(x => x.id == 1).id
        });
    }

    getExpenseType() {
        this._definitionService.getExpenseTypeForSelectBox().subscribe(result => {
            this.expenseTypeGroupList = result;
            console.log(this.expenseTypeGroupList);
        });
    }

    save(): void {
        this.saving = true;
        if (this._data.expenseId != null) {
            this._projectBudgetingService
                .updateExpense(this.expense)
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
                .addExpense(this.expense)
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

    close(result: any): void {
        this._dialogRef.close(result);
    }
}
