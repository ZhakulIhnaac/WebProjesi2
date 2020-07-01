import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ProjectBudgetingServiceProxy, ProjectDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '../../shared/app-component-base';
import { ESearchParamOperator } from '../../shared/AppEnums';
import { ProjectOverallViewDialogComponent } from './overall-view-project/overall-view-project.component';
import { CreateEditProjectDialogComponent } from './create-edit-project/create-edit-project-dialog.component';
import { ProjectExecutionDialogComponent } from './project-execution/create-edit-project-execution-dialog.component';

@Component({
    templateUrl: './projects.component.html',
    animations: [appModuleAnimation()],
    styles: [
        `
          mat-form-field {
            padding: 10px;
          }
        `
    ]
})
export class ProjectsComponent extends AppComponentBase {
    projectList: ProjectDto[] = [];
    searchKeyProjectName: string;

    constructor(
        injector: Injector,
        private _projectBudgetingService: ProjectBudgetingServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
        this.getProjectsForTable()
    }

    searchProjects() {
        this.page = 1;
        this.maxResult = 10;
        this.getProjectsForTable();
    }

    resetSearch() {
        this.page = 1;
        this.maxResult = 10;
        this.searchKeyProjectName = undefined;
        this.getProjectsForTable();
    }

    getProjectsForTable() {
        this.addSearchParamAnd("name", ESearchParamOperator.Equal, this.searchKeyProjectName);
        this._projectBudgetingService.getProjectsForTable(this.maxResult, this.page, this.searchParamList).subscribe(result => {
            this.projectList = result.itemList;
            this.totalItems = result.totalItemCount;
            this.searchParamList.searchParamAndList.length = 0;
            this.searchParamList.searchParamOrList.length = 0;
            this.pageList = [...Array(Math.ceil(this.totalItems / this.maxResult)).keys()].map(i => i + 1);
        })
    }

    createEditDialog(projectId?: string): void {
        let createEditDialog;

        createEditDialog = this._dialog.open(CreateEditProjectDialogComponent, {
            data: { projectId },
            width: "150vh",
            maxHeight: "90vh"
        });

        createEditDialog.afterClosed().subscribe(result => {
            if (result) {
                this.resetSearch();
            }
        });
    }

    createOverviewDialog(projectId: string, projectName: string): void {
        let detailDialog;

        detailDialog = this._dialog.open(ProjectOverallViewDialogComponent, {
            data: { projectId, projectName },
            width: "150vh",
            maxHeight: "90vh"
        });

        detailDialog.afterClosed().subscribe();
    }

    projectExecutionDialog(projectId?: string): void {
        let detailDialog;

        detailDialog = this._dialog.open(ProjectExecutionDialogComponent, {
            data: { projectId },
            width: "150vh",
            maxHeight: "90vh"
        });

        detailDialog.afterClosed().subscribe();
    }

    delete(projectId: string): void {
        abp.message.confirm(
            this.l('ProjectDeleteWarningMessage'), undefined,
            (result: boolean) => {
                if (result) {
                    this._projectBudgetingService.deleteProject(projectId).subscribe(() => {
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                        this.resetSearch();
                    });
                }
            }
        );
    }

}
