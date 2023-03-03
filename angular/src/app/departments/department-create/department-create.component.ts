import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { AgencyDto, AgencyServiceProxy, AgencyTypeDto, CreateUpdateDepartmentDto, DepartmentServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
    selector: 'app-department-create',
    templateUrl: './department-create.component.html',
    styleUrls: ['./department-create.component.css']
})
export class DepartmentCreateComponent extends AppComponentBase implements OnInit {
    saving = false;
    departmentAgencyType :number=0;
    department = new CreateUpdateDepartmentDto();
    agencyTypes: AgencyTypeDto[] = [];
    agencies: AgencyDto[] = [];
    allAgencies: AgencyDto[] = [];
    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        public _agencyServiceProxy: AgencyServiceProxy,
        public _departmentServiceProxy: DepartmentServiceProxy,
        public bsModalRef: BsModalRef,
        public authService: AppAuthService,
        private _sessionService: AbpSessionService,

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.loadAgencyTypes();
        this.loadAgencies();
    }
    onTypeChange(event) {
        this.agencies = this.allAgencies.filter(s => s.agencyTypeId == event);
    }

    loadAgencies() {
        this._agencyServiceProxy.getAllAgenciesList().subscribe(res => {
            this.allAgencies = res;
        });
    }
    loadAgencyTypes() {

        this._agencyServiceProxy.getAllAgencyTypeList().subscribe(res => {
            this.agencyTypes = res;
        });
    }

    loadAgenciesTypes() {
        this._agencyServiceProxy.getAllAgencyTypeList().subscribe(res => {
            this.agencyTypes = res;
        });
    }
    save(): void {
        this.saving = true;
        this._departmentServiceProxy.createOrUpdate(this.department).subscribe(
            () => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.bsModalRef.hide();
                this.onSave.emit();
            },
            () => {
                this.saving = false;
            }
        );
    }
}
