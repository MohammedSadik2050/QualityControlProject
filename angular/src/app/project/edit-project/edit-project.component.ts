import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AbpSessionService } from 'abp-ng2-module';
import * as moment from 'moment';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { AgencyDto, AgencyServiceProxy, AgencyTypeDto, DropdownListDto, LookupServiceProxy, ProjectDto, ProjectItemDto, ProjectServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-edit-project',
  templateUrl: './edit-project.component.html',
  styleUrls: ['./edit-project.component.css']
})
export class EditProjectComponent extends AppComponentBase implements OnInit {
    saving = false;
    startDatemodel :string = new Date().toLocaleDateString();
    completeDatemodel :string = new Date().toLocaleDateString();
    siteDelivedDatemodel: string = new Date().toLocaleDateString();
    project = new ProjectDto();
    projectItems :ProjectItemDto[]=[];
    projectItem: ProjectItemDto = new ProjectItemDto();
    agencyTypes: AgencyTypeDto[] = [];
    agencies: AgencyDto[] = [];
    allAgencies: AgencyDto[] = [];
    supervisingEngineers: DropdownListDto[] = [];
    consultants: DropdownListDto[] = [];
    contractors: DropdownListDto[] = [];
    projectManagers: DropdownListDto[] = [];
    supervisingQualities: DropdownListDto[] = []; 
    @Output() onSave = new EventEmitter<any>();
    projectId: number = 0;
    constructor(
        injector: Injector,
        public _projectServiceProxy: ProjectServiceProxy,
        public _agencyServiceProxy: AgencyServiceProxy,
        public _lookupServiceProxy: LookupServiceProxy,
        public authService: AppAuthService,
        private _sessionService: AbpSessionService,
        private route: ActivatedRoute

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.projectId = this.route.snapshot.params['id'];
        this._projectServiceProxy.get(this.projectId).subscribe(res => {
            this.project = res; 
            this.startDatemodel = moment(this.project.startDate).format("YYYY-MM-DD");
            this.completeDatemodel = moment(this.project.completedDate).format("YYYY-MM-DD");
            this.siteDelivedDatemodel = moment(this.project.siteDelivedDate).format("YYYY-MM-DD");
            this.loadAgencyTypes();
            this.loadAgencies();
            this.loadSupervisingEngineers();
            this.loadConsultants();
            this.loadContractors();
            this.loadProjectManagers();
            this.loadSupervisingQualities();
            this.loadProjectItems();
        });
       
    }

    loadProjectItems() {
        this._projectServiceProxy.getProjectItemsByProjectId(this.projectId).subscribe(res => {
            this.projectItems = res;
        });
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
            this.onTypeChange();
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
        console.log("date", this.project.startDate);
        this.project.startDate = moment(this.startDatemodel, "YYYY-MM-DD");
        this.project.completedDate = moment(this.completeDatemodel, "YYYY-MM-DD");
        this.project.siteDelivedDate = moment(this.siteDelivedDatemodel, "YYYY-MM-DD");
        this._projectServiceProxy.createOrUpdate(this.project).subscribe(
            () => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.onSave.emit();
            },
            () => {
                this.saving = false;
            }
        );
    }



    saveProductItems() {
        this.projectItem.projectId = this.projectId;
        this._projectServiceProxy.createOrUpdateProjectItem(this.projectItem).subscribe(
            () => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.loadProjectItems();
                this.projectItem = new ProjectItemDto();
            },
            () => {
                this.saving = false;
            }
        );
    }

    editProjectItems(id: number) {

        this._projectServiceProxy.getProjectItem(id).subscribe(res => {
            this.projectItem = res;
        });
    }
}