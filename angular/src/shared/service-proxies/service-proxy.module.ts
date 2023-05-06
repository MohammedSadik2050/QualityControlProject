import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AbpHttpInterceptor } from 'abp-ng2-module';

import * as ApiServiceProxies from './service-proxies';

@NgModule({
    providers: [
        ApiServiceProxies.RoleServiceProxy,
        ApiServiceProxies.SessionServiceProxy,
        ApiServiceProxies.TenantServiceProxy,
        ApiServiceProxies.UserServiceProxy,
        ApiServiceProxies.LookupServiceProxy,
        ApiServiceProxies.TowinShipServiceProxy,
        ApiServiceProxies.ObserverServiceProxy,
        ApiServiceProxies.QCUserServiceProxy,
        ApiServiceProxies.ProjectServiceProxy,
        ApiServiceProxies.AgencyServiceProxy,
        ApiServiceProxies.InspectionTestServiceProxy,
        ApiServiceProxies.TokenAuthServiceProxy,
        ApiServiceProxies.AccountServiceProxy,
        ApiServiceProxies.ConfigurationServiceProxy,
        ApiServiceProxies.RequestServiceProxy,
        ApiServiceProxies.RequestnspectionTestServiceProxy,
        ApiServiceProxies.RequestWFServiceProxy,
        ApiServiceProxies.RequestProjectItemServiceProxy,
        ApiServiceProxies.DepartmentServiceProxy,
        ApiServiceProxies.DashboardServiceProxy,
        ApiServiceProxies.AttachmentServiceProxy,
        ApiServiceProxies.RC2ServiceProxy,
        ApiServiceProxies.AsphaltFieldServiceProxy,
        { provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true }
    ]
})
export class ServiceProxyModule { }
