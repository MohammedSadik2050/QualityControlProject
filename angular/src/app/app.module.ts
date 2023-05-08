import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxPaginationModule } from 'ngx-pagination';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';
import { HomeComponent } from '@app/home/home.component';
import { AboutComponent } from '@app/about/about.component';
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
// layout
import { HeaderComponent } from './layout/header.component';
import { HeaderLeftNavbarComponent } from './layout/header-left-navbar.component';
import { HeaderLanguageMenuComponent } from './layout/header-language-menu.component';
import { HeaderUserMenuComponent } from './layout/header-user-menu.component';
import { FooterComponent } from './layout/footer.component';
import { SidebarComponent } from './layout/sidebar.component';
import { SidebarLogoComponent } from './layout/sidebar-logo.component';
import { SidebarUserPanelComponent } from './layout/sidebar-user-panel.component';
import { SidebarMenuComponent } from './layout/sidebar-menu.component';
import { AgenyComponent } from './ageny/ageny.component';
import { AgencyCreateComponent } from './ageny/agency-create/agency-create.component';
import { AgencyEditComponent } from './ageny/agency-edit/agency-edit.component';
import { ProjectComponent } from './project/project.component';
import { CreateProjectComponent } from './project/create-project/create-project.component';
import { EditProjectComponent } from './project/edit-project/edit-project.component';
import { AppUsersComponent } from './app-users/app-users.component';
import { AppUserCreateComponent } from './app-users/app-user-create/app-user-create.component';
import { AppUserEditComponent } from './app-users/app-user-edit/app-user-edit.component';
import { InspectionTestComponent } from './inspection-test/inspection-test.component';
import { InspectionTestCreateComponent } from './inspection-test/inspection-test-create/inspection-test-create.component';
import { InspectionTestEditComponent } from './inspection-test/inspection-test-edit/inspection-test-edit.component';
import { RequestsComponent } from './requests/requests.component';
import { RequestCreateComponent } from './requests/request-create/request-create.component';
import { RequestEditComponent } from './requests/request-edit/request-edit.component';
import { RequestViewComponent } from './requests/request-view/request-view.component';
import { RejectModalComponent } from './requests/reject-modal/reject-modal.component';
import { DepartmentsComponent } from './departments/departments.component';
import { DepartmentCreateComponent } from './departments/department-create/department-create.component';
import { DepartmentEditComponent } from './departments/department-edit/department-edit.component';
import { ProjectRejectModalComponent } from './project/project-reject-modal/project-reject-modal.component';
import { TownshipComponent } from './township/township.component';
import { TownshipCreateComponent } from './township/township-create/township-create.component';
import { TownshipEditComponent } from './township/township-edit/township-edit.component';
import { ObserverComponent } from './observer/observer.component';
import { ObserverCreateComponent } from './observer/observer-create/observer-create.component';
import { ObserverEditComponent } from './observer/observer-edit/observer-edit.component';
import { RequestAssignComponent } from './request-assign/request-assign.component';
import { AssignModalComponent } from './request-assign/assign-modal/assign-modal.component';
import { Rc2testComponent } from './requests/rc2test/rc2test.component';
import { AsphaltFieldComponent } from './requests/asphalt-field/asphalt-field.component';
import { ConcreteFieldComponent } from './requests/concrete-field/concrete-field.component';
import { InspectionTestAssignComponent } from './inspection-test-assign/inspection-test-assign.component';
import { AssignTestFormComponent } from './inspection-test-assign/assign-test-form/assign-test-form.component';
import { AgmCoreModule } from '@agm/core';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        AboutComponent,
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
        // layout
        HeaderComponent,
        HeaderLeftNavbarComponent,
        HeaderLanguageMenuComponent,
        HeaderUserMenuComponent,
        FooterComponent,
        SidebarComponent,
        SidebarLogoComponent,
        SidebarUserPanelComponent,
        SidebarMenuComponent,
        AgenyComponent,
        AgencyCreateComponent,
        AgencyEditComponent,
        ProjectComponent,
        CreateProjectComponent,
        EditProjectComponent,
        AppUsersComponent,
        AppUserCreateComponent,
        AppUserEditComponent,
        InspectionTestComponent,
        InspectionTestCreateComponent,
        InspectionTestEditComponent,
        RequestsComponent,
        RequestCreateComponent,
        RequestEditComponent,
        RequestViewComponent,
        RejectModalComponent,
        DepartmentsComponent,
        DepartmentCreateComponent,
        DepartmentEditComponent,
        ProjectRejectModalComponent,
        TownshipComponent,
        TownshipCreateComponent,
        TownshipEditComponent,
        ObserverComponent,
        ObserverCreateComponent,
        ObserverEditComponent,
        RequestAssignComponent,
        AssignModalComponent,
        Rc2testComponent,
        AsphaltFieldComponent,
        ConcreteFieldComponent,
        InspectionTestAssignComponent,
        AssignTestFormComponent,
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        HttpClientJsonpModule,
        ModalModule.forChild(),
        BsDropdownModule,
        CollapseModule,
        TabsModule,
        AppRoutingModule,
        ServiceProxyModule,
        SharedModule,
        NgxPaginationModule,
        AgmCoreModule.forRoot({
            apiKey: 'AIzaSyARYjxnezBeTTArNqLH1wuX_WLVWwOayt8',
            libraries: ['drawing', 'geometry'],
        }),
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
        AppUsersComponent,
        AppUserCreateComponent,
        AppUserEditComponent,
        AgencyCreateComponent,
        AgencyEditComponent
    ],
})
export class AppModule { }
