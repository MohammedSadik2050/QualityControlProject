import { ChangeDetectorRef, Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AbpSessionService } from 'abp-ng2-module';
import * as moment from 'moment';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { CreateUpdateInspectionTestDto, CreateUpdateRequestTestDto, DropdownListDto, InspectionTestDto, InspectionTestServiceProxy, LookupServiceProxy, ProjectDto, ProjectDtoPagedResultDto, ProjectServiceProxy, RequestDto, RequestInspectionTestDto, RequestInspectionTestViewDto, RequestnspectionTestServiceProxy, RequestServiceProxy, RequestStatus, RequestWFDto, RequestWFServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
    selector: 'app-request-create',
    templateUrl: './request-create.component.html',
    styleUrls: ['./request-create.component.css']
})
export class RequestCreateComponent extends AppComponentBase implements OnInit {
    startDatemodel: string = new Date().toLocaleDateString();
    completeDatemodel: string = new Date().toLocaleDateString();
    projectName: string = '';
    projectContractNumber: string = '';
    project = new ProjectDto();
    saving = false;
    hide = false;
    hasSample = false;
    saveText = 'SendToConsultant';
    request = new RequestDto();
    projects: ProjectDto[] = [];
    requestInspection = new CreateUpdateRequestTestDto();
    inspectionTestTypes: DropdownListDto[] = [];
    inspectionTests: InspectionTestDto[] = [];
    requestTests: RequestInspectionTestViewDto[] = [];
    mainRequestTypes: DropdownListDto[] = [];
    @Output() onSave = new EventEmitter<any>();
    InspectionDatemodel: string = new Date().toLocaleDateString();
    constructor(
        injector: Injector,
        public _requestServiceProxy: RequestServiceProxy,
        public _projectServiceProxy: ProjectServiceProxy,
        public _lookupServiceProxy: LookupServiceProxy,
        public _inspectionTestServiceProxy: InspectionTestServiceProxy,
        public _requestnspectionTestServiceProxy: RequestnspectionTestServiceProxy,
        public _requestWFServiceProxy: RequestWFServiceProxy,
        public authService: AppAuthService,
        private _sessionService: AbpSessionService,
        private router: Router,

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.loadAllProject();
        this.loadMainRequestTypes();
        this.loadTestTypes();
        this.request.id = 0;
    }
    loadTestTypes() {

        this._lookupServiceProxy.inspectionTestTypes().subscribe(res => {
            this.inspectionTestTypes = res;
        });
    }

    loadTestsByTypes() {

        this._inspectionTestServiceProxy.getAllInspectionTestListByType(this.request.testType).subscribe(res => {
            this.inspectionTests = res;
        });
    }

    loadRequestTests() {

        this._requestnspectionTestServiceProxy.getAll(this.request.id).subscribe(res => {
            this.requestTests = res;
        });
    }
    loadMainRequestTypes() {

        this._lookupServiceProxy.mainRequestTypes().subscribe(res => {
            this.mainRequestTypes = res;
        });
    }
    sampleChange(event) {
        if (event==true) {
            this.saveText = 'CONTINUE'
        } else {
            this.saveText = 'SendToConsultant'
        }

    }
    loadAllProject() {

        this._projectServiceProxy.getAllDropdown().subscribe(res => {
            this.projects = res;
        });
    }

    save(status:number): void {
        this.saving = true;
        this.request.mainRequestType = 1;
        if (this.hasSample == true) {
            this.request.hasSample = 1;
        } else {
            this.request.hasSample = 2;
        }
        this.request.status = status;
      
        this.request.inspectionDate = moment(this.InspectionDatemodel, "YYYY-MM-DD");
        this._requestServiceProxy.createOrUpdate(this.request).subscribe(
            res => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.request = res;
                this.InspectionDatemodel = moment(this.request.inspectionDate).format("YYYY-MM-DD");
                this.loadTestsByTypes();
                this.saving = false;
                if (this.request.status == RequestStatus._2) {
                    this.saveWorkFlow();
                }
                //this.bsModalRef.hide();
                //this.onSave.emit();
            },
            () => {
                this.saving = false;
            }
        );
    }
    saveWorkFlow() {
        var currentProject = this.projects.find(x => x.id === parseInt(this.request.projectId.toString()));
        var workFlow = new RequestWFDto();
        workFlow.requestId = this.request.id;
        workFlow.currentUserId = currentProject.consultantId;
        this._requestWFServiceProxy.createOrUpdate(workFlow).subscribe(res => {
            this.router.navigateByUrl('/app/examinationRequest');
        });
    }
    saveInspectionTest() {
        //this.requestInspection.
       
        this.requestInspection.inspectionTestType = this.request.testType;
        this.requestInspection.requestId = this.request.id;
        var check = this.requestTests.filter(x => x.inspectionTestId === parseInt(this.requestInspection.inspectionTestId.toString()));
        if (check != undefined && check.length > 0) {
            this.notify.error(this.l('DuplicateTest'));
            return null;
        }
        this._requestnspectionTestServiceProxy.createOrUpdate(this.requestInspection).subscribe(res => {
            this.loadRequestTests();
        });
    }

    delete(row: RequestInspectionTestViewDto) {
        this._requestnspectionTestServiceProxy.delete(row.id).subscribe(res => {
            this.loadRequestTests();
        });
    }

    LoadProject(event) {
        
        this._projectServiceProxy.get(event).subscribe(res => {
            this.project = res;
           
            this.projectName = this.project.name;
            this.projectContractNumber = this.project.contractNumber;
            this.startDatemodel = moment(this.project.startDate).format("YYYY-MM-DD");
            this.completeDatemodel = moment(this.project.completedDate).format("YYYY-MM-DD");
          
        });
    }
}
