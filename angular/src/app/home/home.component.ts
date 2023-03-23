import { Component, Injector, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { DashboardServiceProxy, StatisticsDto } from '../../shared/service-proxies/service-proxies';

@Component({
    templateUrl: './home.component.html',
    animations: [appModuleAnimation()],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent extends AppComponentBase implements OnInit {
    constructor(injector: Injector, private _dashboardService: DashboardServiceProxy) {
        super(injector);
    }
    dashBoard = new StatisticsDto();
    ngOnInit(): void {
        this._dashboardService.getSystemStatisticsDashboard().subscribe(res => {
            this.dashBoard = res;
            this.dashBoard.stackholderDto.totalContractorUsers;
        });
    }


}
