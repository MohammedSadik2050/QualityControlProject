import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { AgencyDto, AgencyServiceProxy, AgencyTypeDto, CreateUpdateDepartmentDto, DepartmentServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
    selector: 'app-department-edit',
    templateUrl: './department-edit.component.html',
    styleUrls: ['./department-edit.component.css']
})
export class DepartmentEditComponent extends AppComponentBase implements OnInit {
    saving = false;
    id: number;
    departmentAgencyType: number = 0;
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
    loadDepartment(id) {
        this._departmentServiceProxy.get(id).subscribe(res => {
            this.department = res;
            var currentAgency = this.allAgencies.find(s => s.id == this.department.agencyId);
            this.departmentAgencyType = currentAgency.agencyTypeId;
            this.onTypeChange(this.departmentAgencyType);
            this.department.agencyId = currentAgency.id;
        });
    }
    loadAgencies() {
        this._agencyServiceProxy.getAllAgenciesList().subscribe(res => {
            this.allAgencies = res;
            this.loadDepartment(this.id)
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
