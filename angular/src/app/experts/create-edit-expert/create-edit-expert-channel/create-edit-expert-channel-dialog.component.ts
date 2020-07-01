import { Component, Inject, Injector, OnInit, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AppComponentBase } from '@shared/app-component-base';
import { DefinitionServiceProxy, ExpertChannelDto, ChannelTypeDto, ProjectBudgetingServiceProxy } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    templateUrl: './create-edit-expert-channel-dialog.component.html',
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
export class CreateEditExpertChannelDialogComponent extends AppComponentBase {
    saving = false;
    expertChannel: ExpertChannelDto = new ExpertChannelDto();
    channelTypeList: ChannelTypeDto[] = [];
    createEditExpertChannelFormGroup: FormGroup;

    constructor(
        private fb: FormBuilder,
        injector: Injector,
        public _projectBudgetingService: ProjectBudgetingServiceProxy,
        public _definitionService: DefinitionServiceProxy,
        private _dialogRef: MatDialogRef<CreateEditExpertChannelDialogComponent>,
        @Optional() @Inject(MAT_DIALOG_DATA) private _data: { expertId, expertChannelId }
    ) {
        super(injector);
        this.createEditExpertChannelFormGroup = this.fb.group({
            channelType: new FormControl('', [Validators.required]),
            channelContext: new FormControl('', [Validators.required]),
        })
        if (this._data.expertChannelId != null) {
            this._projectBudgetingService.findExpertChannel(this._data.expertChannelId).subscribe(result => {
                this.expertChannel = result;
            });
        }
        this.expertChannel.expertId = _data.expertId;
        this.getChannelTypes();
    }

    getChannelTypes() {
        this._definitionService.getChannelTypeForSelectBox().subscribe(result => {
            this.channelTypeList = result;
        });
    }

    save(): void {
        this.saving = true;
        if (this._data.expertChannelId != null) {
            this._projectBudgetingService
                .updateExpertChannel(this.expertChannel)
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
                .addExpertChannel(this.expertChannel)
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
