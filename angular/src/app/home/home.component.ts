import { Component, Injector, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { DashboardServiceProxy, StatisticsDto } from '../../shared/service-proxies/service-proxies';
import { Router } from '@angular/router';

@Component({
    templateUrl: './home.component.html',
    animations: [appModuleAnimation()],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent extends AppComponentBase implements OnInit {
    constructor(injector: Injector, private _dashboardService: DashboardServiceProxy, private router: Router) {
        super(injector);
    }
    dashBoard = new StatisticsDto();
    ngOnInit(): void {
        if (this.isGranted('Pages.Manage.LabProjectManager' || 'Pages.Manage.SupervisingQuality')) {

        }
        else if (this.isGranted('Pages.Manage.Contractor') || this.isGranted('Pages.Manage.Observer')) {
            this.router.navigateByUrl('/app/examinationRequest');
            return;
        }
        this._dashboardService.getSystemStatisticsDashboard().subscribe(res => {
            this.dashBoard = res;
            this.dashBoard.stackholderDto.totalContractorUsers;
        });
    }


}
