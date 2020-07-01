import { Component, Inject, Injector, Optional } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AppComponentBase } from '@shared/app-component-base';
import { ActivityDto, ProjectBudgetingServiceProxy, ProjectDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { CreateEditActivityDialogComponent } from './activities/create-edit-activity/create-edit-activity-dialog.component';

@Component({
    templateUrl: './create-edit-project-dialog.component.html',
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
export class CreateEditProjectDialogComponent extends AppComponentBase {
    saving = false;
    project: ProjectDto = new ProjectDto();
    activityList: ActivityDto[] = [];
    activityListFiltered: ActivityDto[] = [];
    createEditProjectFormGroup: FormGroup;
    activitySelectBoxFilter: string;

    constructor(
        private fb: FormBuilder,
        injector: Injector,
        public _projectBudgetingService: ProjectBudgetingServiceProxy,
        private _dialogRef: MatDialogRef<CreateEditProjectDialogComponent>,
        private _dialog: MatDialog,
        @Optional() @Inject(MAT_DIALOG_DATA) private _data: { projectDuration, projectId }
    ) {
        super(injector);
        this.createEditProjectFormGroup = this.fb.group({
            name: new FormControl('', [Validators.required, Validators.maxLength(128)]),
            description: new FormControl('', [Validators.required, Validators.maxLength(4096)]),
            duration: new FormControl('', [Validators.required]),
        })
        if (this._data.projectId != null) {
            this.findProject();
            this.getActivities();
        }
    }

    refresh() {
        this.findProject();
        this.getActivities();
    };

    findProject() {
        this._projectBudgetingService.findProject(this._data.projectId).subscribe(result => {
            this.project = result;
        });
    }

    filterActivityName() {
        this.activityListFiltered = this.activityList.filter(x => x.name.toLowerCase().includes(this.activitySelectBoxFilter.toLowerCase()));
    }

    getActivities() {
        this._projectBudgetingService.getActivitiesOfProject(this._data.projectId).subscribe(result => {
            this.activityList = result;
            this.activityListFiltered = result;
        });
    }

    save(): void {
        this.saving = true;
        if (this._data.projectId != null) {
            this._projectBudgetingService
                .updateProject(this.project)
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
                .addProject(this.project)
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

    createEditDialog(projectDuration: number, projectId: string, activityId?: string): void {
        let createEditDialog;

        createEditDialog = this._dialog.open(CreateEditActivityDialogComponent, {
            data: { projectDuration, projectId, activityId },
            width: "150vh",
            maxHeight: "90vh"
        });

        createEditDialog.afterClosed().subscribe(result => {
            if (result) {
                this.getActivities();
            }
        });
    }

    delete(projectId: string): void {
        abp.message.confirm(
            this.l('ProjectDeleteWarningMessage'), undefined,
            (result: boolean) => {
                if (result) {
                    this._projectBudgetingService.deleteProject(projectId).subscribe(() => {
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
