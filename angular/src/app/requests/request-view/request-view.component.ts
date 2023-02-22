import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AbpSessionService } from 'abp-ng2-module';
import * as moment from 'moment';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import {
    CreateUpdateRequestTestDto, DropdownListDto, InspectionTestDto,
    InspectionTestServiceProxy, LookupServiceProxy, ProjectDto, ProjectServiceProxy,
    RequestDto, RequestInspectionTestViewDto, RequestnspectionTestServiceProxy, RequestServiceProxy,
    RequestStatus, RequestWFDto, RequestWFServiceProxy
} from '../../../shared/service-proxies/service-proxies';


@Component({
  selector: 'app-request-view',
  templateUrl: './request-view.component.html',
  styleUrls: ['./request-view.component.css']
})
export class RequestViewComponent extends AppComponentBase implements OnInit {
    saving = false;
    hide = false;
    hasSample = false;
    startDatemodel: string = new Date().toLocaleDateString();
    completeDatemodel: string = new Date().toLocaleDateString();
    projectName: string ='';
    projectContractNumber: string ='';
    saveText = 'SendToConsultant';
    request = new RequestDto();
    projects: ProjectDto[] = [];
    project = new ProjectDto();
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
        private routeActive: ActivatedRoute,

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.loadAllProject();
        this.loadMainRequestTypes();
        this.loadTestTypes();
        this.request.id = this.routeActive.snapshot.params['id'];
        this._requestServiceProxy.get(this.request.id).subscribe(res => {
            this.hasSample = res.hasSample == 1 ? true : false;
            this.request = res;
            this.loadRequestTests();
            this.loadTestsByTypes();
            this.LoadProject();
        });
    }

    LoadProject() {
        console.log('asdasdas');
        this._projectServiceProxy.get(this.request.projectId).subscribe(res => {
            this.project = res;
            this.startDatemodel = moment(this.project.startDate).format("YYYY-MM-DD");
            this.completeDatemodel = moment(this.project.completedDate).format("YYYY-MM-DD");
            this.projectName = res.name;
            this.projectContractNumber = res.contractNumber;
        });
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
        if (event == true) {
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

    save(): void {
        this.saving = true;
        if (this.hasSample == true) {
            this.request.hasSample = 1;
        } else {
            this.request.hasSample = 2;
        }
        if (this.saveText == 'SendToConsultant' || this.request.id > 0) {
            this.request.status = RequestStatus._2;
        } else {
            this.request.status = RequestStatus._1;
        }

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
}