import { Component, Inject, Injector, OnInit, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AppComponentBase } from '@shared/app-component-base';
import { ExpertChannelDto, ExpertDto, ProjectBudgetingServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
    templateUrl: './detail-expert.component.html'
})

export class ExpertDetailDialogComponent extends AppComponentBase {

    expert: ExpertDto = new ExpertDto();
    expertChannelList: ExpertChannelDto[] = [];

    constructor(
        injector: Injector,
        public _projectBudgetingServiceProxy: ProjectBudgetingServiceProxy,
        private _dialogRef: MatDialogRef<ExpertDetailDialogComponent>,
        @Optional() @Inject(MAT_DIALOG_DATA) private _data: { expertId }
    ) {
        super(injector);
        this.findExpert();
        this.findExpertChannels();
    }

    findExpert() {
        this._projectBudgetingServiceProxy.findExpert(this._data.expertId).subscribe(result => {
            this.expert = result;
        });
    }

    findExpertChannels() {
        this._projectBudgetingServiceProxy.getExpertChannelByExpertIdForTable(this._data.expertId).subscribe(result => {
            this.expertChannelList = result;
        });
    }

    close(result: any): void {
        this._dialogRef.close(result);
    }
}
