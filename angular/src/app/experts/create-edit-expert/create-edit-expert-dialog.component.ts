import { Component, Inject, Injector, OnInit, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { AppComponentBase } from '@shared/app-component-base';
import { DefinitionServiceProxy, ExpertChannelDto, ExpertDto, ProjectBudgetingServiceProxy, CountryDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { CreateEditExpertChannelDialogComponent } from './create-edit-expert-channel/create-edit-expert-channel-dialog.component';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    templateUrl: './create-edit-expert-dialog.component.html',
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
export class CreateEditExpertDialogComponent extends AppComponentBase implements OnInit {
    saving = false;
    expert: ExpertDto = new ExpertDto();
    countryList: CountryDto[] = [];
    countryListFiltered: CountryDto[] = [];
    expertChannelList: ExpertChannelDto[] = [];
    countrySelectBoxFilter: string;
    createEditExpertFormGroup: FormGroup;


    constructor(
        private fb: FormBuilder,
        injector: Injector,
        public _corporateService: ProjectBudgetingServiceProxy,
        public _definitionService: DefinitionServiceProxy,
        private _dialogRef: MatDialogRef<CreateEditExpertDialogComponent>,
        private _dialog: MatDialog,
        @Optional() @Inject(MAT_DIALOG_DATA) private _data: { expertId }
    ) {
        super(injector);
        this.createEditExpertFormGroup = this.fb.group({
            name: new FormControl('', [Validators.required, Validators.maxLength(64)]),
            surname: new FormControl('', [Validators.required, Validators.maxLength(64)]),
            title: new FormControl('', [Validators.required, Validators.maxLength(32)]),
            country: new FormControl('', [Validators.required]),
            sex: new FormControl('', [Validators.required]),
        })
        if (this._data.expertId != null) {
            this._corporateService.findExpert(this._data.expertId).subscribe(result => {
                this.expert = result;
            });
        }
    }

    ngOnInit(): void {
        this.getCountries();
        this.getExpertChannels();
    }

    filterCountry() {
        this.countryListFiltered = this.countryList.filter(x => x.countryName.toLowerCase().includes(this.countrySelectBoxFilter.toLowerCase()));
    }

    getCountries() {
        this._definitionService.getCountryForSelectBox().subscribe(result => {
            this.countryList = result;
            this.countryListFiltered = result;
        });
    }

    getExpertChannels() {
        this._corporateService.getExpertChannelByExpertIdForTable(this._data.expertId).subscribe(result => {
            this.expertChannelList = result;
        });
    }

    createEditDialog(expertId: string, expertChannelId?: string): void {
        let createEditDialog;

        createEditDialog = this._dialog.open(CreateEditExpertChannelDialogComponent, {
            data: { expertId },
            width: "80vh",
            maxHeight: "90vh"
        });

        createEditDialog.afterClosed().subscribe(result => {
            if (result) {
                this.getExpertChannels();
            }
        });
    }

    save(): void {
        this.saving = true;
        if (this._data.expertId != null) {
            this._corporateService
                .updateExpert(this.expert)
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
            this._corporateService
                .addExpert(this.expert)
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

    delete(expertChannelId: string): void {
        abp.message.confirm(
            this.l('ExpertChannelDeleteWarningMessage'), undefined,
            (result: boolean) => {
                if (result) {
                    this._corporateService.deleteExpertChannel(expertChannelId).subscribe(() => {
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
