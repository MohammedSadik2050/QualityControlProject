import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { RolesComponent } from 'app/roles/roles.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { AgenyComponent } from './ageny/ageny.component';
import { ProjectComponent } from './project/project.component';
import { EditProjectComponent } from './project/edit-project/edit-project.component';
import { AppUsersComponent } from './app-users/app-users.component';
import { InspectionTestComponent } from './inspection-test/inspection-test.component';
import { RequestsComponent } from './requests/requests.component';
import { InspectionTestCreateComponent } from './inspection-test/inspection-test-create/inspection-test-create.component';
import { RequestCreateComponent } from './requests/request-create/request-create.component';
import { RequestEditComponent } from './requests/request-edit/request-edit.component';
import { RequestViewDto } from '../shared/service-proxies/service-proxies';
import { RequestViewComponent } from './requests/request-view/request-view.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'home', component: HomeComponent,  canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    { path: 'agencies', component: AgenyComponent, data: { permission: 'Pages.Manage.Agences' }, canActivate: [AppRouteGuard] },
                    { path: 'qc-users', component: AppUsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    { path: 'projects', component: ProjectComponent, data: { permission: '' }, canActivate: [AppRouteGuard] },
                    { path: 'projects/edit/:id', component: EditProjectComponent, data: { permission: '' }, canActivate: [AppRouteGuard] },
                    { path: 'inspectionTest', component: InspectionTestComponent, data: { permission: 'Pages.Manage.InspectionTest' }, canActivate: [AppRouteGuard] },
                    { path: 'examinationRequest', component: RequestsComponent, data: { permission: '' }, canActivate: [AppRouteGuard] },
                    { path: 'examinationRequest/create', component: RequestCreateComponent, data: { permission: 'Pages.Manage.Contractor' }, canActivate: [AppRouteGuard] },
                    { path: 'examinationRequest/edit/:id', component: RequestEditComponent, data: { permission: '' }, canActivate: [AppRouteGuard] },
                    { path: 'examinationRequest/view/:id', component: RequestViewComponent, data: { permission: '' }, canActivate: [AppRouteGuard] },
                   /* { path: 'about', component: AboutComponent, canActivate: [AppRouteGuard] },*/
                    { path: 'update-password', component: ChangePasswordComponent, canActivate: [AppRouteGuard] }
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
