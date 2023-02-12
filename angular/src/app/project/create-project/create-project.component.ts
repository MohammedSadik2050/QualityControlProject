import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import * as moment from 'moment';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { AgencyDto, AgencyServiceProxy, AgencyTypeDto, DropdownListDto, LookupServiceProxy, ProjectDto, ProjectServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.css']
})
export class CreateProjectComponent extends AppComponentBase implements OnInit {
    saving = false;
    project = new ProjectDto();
    agencyTypes: AgencyTypeDto[] = [];
    agencies: AgencyDto[] = [];
    allAgencies: AgencyDto[] = [];
    supervisingEngineers: DropdownListDto[] = [];
    consultants: DropdownListDto[] = [];
    contractors: DropdownListDto[] = [];
    projectManagers: DropdownListDto[] = [];
    supervisingQualities: DropdownListDto[] = [];
    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        private _projectServiceProxy: ProjectServiceProxy,
        private _agencyServiceProxy: AgencyServiceProxy,
        private _lookupServiceProxy: LookupServiceProxy,
        public bsModalRef: BsModalRef,
        private authService: AppAuthService,
        private _sessionService: AbpSessionService,

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.loadAgencyTypes();
        this.loadAgencies();
        this.loadSupervisingEngineers();
        this.loadConsultants();
        this.loadContractors();
        this.loadProjectManagers();
        this.loadSupervisingQualities();
    }
    loadSupervisingQualities() {
        this._lookupServiceProxy.supervisingQualityList().subscribe(res => {
            this.supervisingQualities = res;
        });
    }

    loadProjectManagers() {
        this._lookupServiceProxy.labProjectManagerList().subscribe(res => {
            this.projectManagers = res;
        });
    }

    loadContractors() {
        this._lookupServiceProxy.contractorList().subscribe(res => {
            this.contractors = res;
        });
    }

    loadSupervisingEngineers() {
        this._lookupServiceProxy.supervisingEngineerList().subscribe(res => {
            this.supervisingEngineers = res;
        });
    }

    loadConsultants() {
        this._lookupServiceProxy.consultantList().subscribe(res => {
            this.consultants = res;
        });
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
    onTypeChange() {
        this.agencies = this.allAgencies.filter(s => s.agencyTypeId == this.project.agencyTypeId);
    }

    save(): void {
        this.saving = true;
        this.project.startDate = moment(this.project.startDate, "YYYY-MM-DD");
        this.project.completedDate = moment(this.project.completedDate, "YYYY-MM-DD");
        this.project.siteDelivedDate = moment(this.project.siteDelivedDate, "YYYY-MM-DD");
        this._projectServiceProxy.createOrUpdate(this.project).subscribe(
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
