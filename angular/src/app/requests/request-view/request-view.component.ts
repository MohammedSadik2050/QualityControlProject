import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AbpSessionService } from 'abp-ng2-module';
import { stat } from 'fs';
import * as moment from 'moment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import {
    AgencyDto,
    AgencyServiceProxy,
    CreateUpdateRequestTestDto, DepartmentDto, DepartmentServiceProxy, DropdownListDto, InspectionTestDto,
    InspectionTestServiceProxy, LookupServiceProxy, ProjectDto, ProjectItemDto, ProjectServiceProxy,
    RequestDto, RequestInspectionTestViewDto, RequestnspectionTestServiceProxy, RequestProjectItemDto, RequestProjectItemServiceProxy, RequestProjectItemViewDto, RequestServiceProxy,
    RequestStatus, RequestWFDto, RequestWFHistoryDto, RequestWFServiceProxy
} from '../../../shared/service-proxies/service-proxies';
import { RejectModalComponent } from '../reject-modal/reject-modal.component';


@Component({
  selector: 'app-request-view',
  templateUrl: './request-view.component.html',
  styleUrls: ['./request-view.component.css']
})
export class RequestViewComponent extends AppComponentBase implements OnInit {
    saving = false;
    startDatemodel: string = new Date().toLocaleDateString();
    completeDatemodel: string = new Date().toLocaleDateString();
    projectName: string = '';
    projectContractNumber: string = '';
    hide = false;
    hasSample = false;
    projectDepartmentId: number = 0;
    projectAgency: number = 0;
    selectedprojectItem: number = 0;
    saveText = 'SendToConsultant';
    request = new RequestDto();
    projects: ProjectDto[] = [];
    project = new ProjectDto();
    requestInspection = new CreateUpdateRequestTestDto();
    inspectionTestTypes: DropdownListDto[] = [];
    inspectionTests: InspectionTestDto[] = [];
    requestTests: RequestInspectionTestViewDto[] = [];
    mainRequestTypes: DropdownListDto[] = [];
    requestHistories: RequestWFHistoryDto[] = [];
    projectItems: ProjectItemDto[] = [];
    requestProjectItems: RequestProjectItemViewDto[] = [];
    InspectionDatemodel: string = new Date().toLocaleDateString();
    allAgencies: AgencyDto[] = [];
    allDepartments: DepartmentDto[] = [];
    constructor(
        injector: Injector,
        private _departmentServiceProxy: DepartmentServiceProxy,
        private _agencyServiceProxy: AgencyServiceProxy,
        public _requestProjectItemServiceProxy: RequestProjectItemServiceProxy,
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
        private _modalService: BsModalService

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.loadAllProject();
        this.loadMainRequestTypes();
        this.loadTestTypes();
        this.request.id = this.routeActive.snapshot.params['id'];
        this._requestServiceProxy.get(this.request.id).subscribe(res => {
            this.request = res;
            this.hasSample = res.hasSample == 1 ? true : false;
            this.InspectionDatemodel = moment(this.request.inspectionDate).format("YYYY-MM-DD");
            this.loadRequestTests();
            this.loadTestsByTypes();
            this.LoadProject();
            this.LoadRequestHistory();
            this.loadRequestProjectItems();
            this.loadAllDepartments();
            this.loadAgencies();
        });
    }
    loadAllDepartments() {
        this._departmentServiceProxy.getAllDepartmentDropDown().subscribe(res => {
            this.allDepartments = res;
        });
    }
    loadAgencies() {
        this._agencyServiceProxy.getAllAgenciesList().subscribe(res => {
            this.allAgencies = res;


        });
    }
    LoadProject() {

        this._projectServiceProxy.get(this.request.projectId).subscribe(res => {
            this.project = res;
            this.projectAgency = this.project.agencyId;
            this.projectDepartmentId = this.project.departmentId;
            this.startDatemodel = moment(this.project.startDate).format("YYYY-MM-DD");
            this.completeDatemodel = moment(this.project.completedDate).format("YYYY-MM-DD");
            this.projectName = res.name;
            this.projectContractNumber = res.contractNumber;
            this.loadProjectItems();
        });
    }

    loadProjectItems() {
        this._projectServiceProxy.getProjectItemsByProjectId(this.request.projectId).subscribe(res => {
            this.projectItems = res;

        });
    }
    LoadRequestHistory() {

        this._requestWFServiceProxy.getAllHistory(this.request.id).subscribe(res => {
            this.requestHistories = res;
            console.log('History', this.requestHistories);
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

    loadRequestProjectItems() {

        this._requestProjectItemServiceProxy.getAll(this.request.id).subscribe(res => {
            this.requestProjectItems = res;
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

    save(status: number): void {

        this.saving = true;
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

    rejectRequest() {
        let rejectModal: BsModalRef;
        rejectModal = this._modalService.show(
            RejectModalComponent,
            {
                class: 'modal-lg',
                initialState: {
                    id: this.request.id,
                    consultantId: this.project.consultantId,
                    request: this.request,
                },
            }
        );

        rejectModal.content.onSave.subscribe(() => {
            this.ngOnInit();
        });
    }

    saveWorkFlow() {
        var currentProject = this.projects.find(x => x.id === parseInt(this.request.projectId.toString()));
        var workFlow = new RequestWFDto();
        workFlow.requestId = this.request.id;
        workFlow.currentUserId = currentProject.consultantId;
        if (this.request.status == 2) {
            workFlow.actionName = "تم التسجيل";
            workFlow.actionNotes = "تم الإرسال الى الاستشاري";
        }

        if (this.request.status == 3) {
            workFlow.actionName = "تمت الموافقه على الطلب";
            workFlow.actionNotes = "الإستشاري وافق على الطلب";
        }

        if (this.request.status == 4) {
            workFlow.actionName = "مراقب الجوده وافق على الطلب";
            workFlow.actionNotes = "مراقب الجوده وافق على الطلب";
        }


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

    saveProjectItems() {
        var check = this.requestProjectItems.filter(x => x.projectIdItemId === parseInt(this.selectedprojectItem.toString()));
        if (check != undefined && check.length > 0) {
            this.notify.error(this.l('DuplicateProjectItem'));
            return null;
        }
        var requestProjectItem = new RequestProjectItemDto();
        requestProjectItem.requestId = this.request.id;
        requestProjectItem.projectItemId = this.selectedprojectItem;
        this._requestProjectItemServiceProxy.createOrUpdate(requestProjectItem).subscribe(res => {
            this.loadRequestProjectItems();
        });
    }

    delete(row: RequestInspectionTestViewDto) {
        this._requestnspectionTestServiceProxy.delete(row.id).subscribe(res => {
            this.loadRequestTests();
        });
    }

    deleteRequestItem(row: RequestProjectItemViewDto) {
        this._requestProjectItemServiceProxy.delete(row.id).subscribe(res => {
            this.loadRequestProjectItems();
        });
    }
}
