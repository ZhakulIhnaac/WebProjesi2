import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ChartsModule } from 'ng2-charts';
import { ModalModule } from 'ngx-bootstrap';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { AbpModule } from '@abp/abp.module';

import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';

import { HomeComponent } from '@app/home/home.component';
import { AboutComponent } from '@app/about/about.component';
import { TopBarComponent } from '@app/layout/topbar.component';
import { TopBarLanguageSwitchComponent } from '@app/layout/topbar-languageswitch.component';
import { SideBarUserAreaComponent } from '@app/layout/sidebar-user-area.component';
import { SideBarNavComponent } from '@app/layout/sidebar-nav.component';
import { SideBarFooterComponent } from '@app/layout/sidebar-footer.component';
import { RightSideBarComponent } from '@app/layout/right-sidebar.component';
// tenants
import { TenantsComponent } from '@app/tenants/tenants.component';
import { CreateTenantDialogComponent } from './tenants/create-tenant/create-tenant-dialog.component';
import { EditTenantDialogComponent } from './tenants/edit-tenant/edit-tenant-dialog.component';
// roles
import { RolesComponent } from '@app/roles/roles.component';
import { CreateRoleDialogComponent } from './roles/create-role/create-role-dialog.component';
import { EditRoleDialogComponent } from './roles/edit-role/edit-role-dialog.component';
// users
import { UsersComponent } from '@app/users/users.component';
import { CreateUserDialogComponent } from '@app/users/create-user/create-user-dialog.component';
import { EditUserDialogComponent } from '@app/users/edit-user/edit-user-dialog.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { ResetPasswordDialogComponent } from './users/reset-password/reset-password.component';
import { ExpertsComponent } from './experts/experts.component';
import { CreateEditExpertDialogComponent } from './experts/create-edit-expert/create-edit-expert-dialog.component';
import { ExpertDetailDialogComponent } from './experts/detail-expert/detail-expert.component';
import { CreateEditExpertChannelDialogComponent } from './experts/create-edit-expert/create-edit-expert-channel/create-edit-expert-channel-dialog.component';
import { ProjectsComponent } from './projects/projects.component';
import { CreateEditProjectDialogComponent } from './projects/create-edit-project/create-edit-project-dialog.component';
import { CreateEditActivityDialogComponent } from './projects/create-edit-project/activities/create-edit-activity/create-edit-activity-dialog.component';
import { CreateEditExpenseDialogComponent } from './projects/create-edit-project/activities/create-edit-activity/expenses/create-edit-expense/create-edit-expense-dialog.component';
import { ProjectOverallViewDialogComponent } from './projects/overall-view-project/overall-view-project.component';
import { ProjectExecutionDialogComponent } from './projects/project-execution/create-edit-project-execution-dialog.component';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        AboutComponent,
        TopBarComponent,
        TopBarLanguageSwitchComponent,
        SideBarUserAreaComponent,
        SideBarNavComponent,
        SideBarFooterComponent,
        RightSideBarComponent,

        // tenants
        TenantsComponent,
        CreateTenantDialogComponent,
        EditTenantDialogComponent,

        // roles
        RolesComponent,
        CreateRoleDialogComponent,
        EditRoleDialogComponent,

        // users
        UsersComponent,
        CreateUserDialogComponent,
        EditUserDialogComponent,
        ChangePasswordComponent,
        ResetPasswordDialogComponent,

        // experts
        ExpertsComponent,
        CreateEditExpertDialogComponent,
        ExpertDetailDialogComponent,
        CreateEditExpertChannelDialogComponent,

        // projects
        ProjectsComponent,
        CreateEditProjectDialogComponent,
        ProjectOverallViewDialogComponent,
        CreateEditActivityDialogComponent,
        CreateEditExpenseDialogComponent,
        ProjectExecutionDialogComponent

    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        HttpClientJsonpModule,
        ModalModule.forRoot(),
        AbpModule,
        AppRoutingModule,
        ChartsModule,
        ServiceProxyModule,
        SharedModule,
        NgxPaginationModule
    ],
    providers: [],
    entryComponents: [
        // tenants
        CreateTenantDialogComponent,
        EditTenantDialogComponent,

        // roles
        CreateRoleDialogComponent,
        EditRoleDialogComponent,

        // users
        CreateUserDialogComponent,
        EditUserDialogComponent,
        ResetPasswordDialogComponent,

        // experts
        CreateEditExpertDialogComponent,
        ExpertDetailDialogComponent,
        CreateEditExpertChannelDialogComponent,

        // projects
        CreateEditProjectDialogComponent,
        ProjectOverallViewDialogComponent,
        CreateEditActivityDialogComponent,
        CreateEditExpenseDialogComponent,
        ProjectExecutionDialogComponent
    ]
})
export class AppModule { }
