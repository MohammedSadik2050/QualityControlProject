import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AbpSessionService } from 'abp-ng2-module';
import { stat } from 'fs';
import * as moment from 'moment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppConsts } from '../../../shared/AppConsts';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import {
    AgencyDto,
    AgencyServiceProxy,
    AttachmentDto,
    AttachmentServiceProxy,
    CreateUpdateRequestTestDto, DepartmentDto, DepartmentServiceProxy, DropdownListDto, InspectionTestDto,
    InspectionTestServiceProxy, LookupServiceProxy, ProjectDto, ProjectItemDto, ProjectServiceProxy,
    RequestDto, RequestInspectionTestViewDto, RequestnspectionTestServiceProxy, RequestProjectItemDto, RequestProjectItemServiceProxy, RequestProjectItemViewDto, RequestServiceProxy,
    RequestStatus, RequestWFDto, RequestWFHistoryDto, RequestWFServiceProxy, TowinShipServiceProxy, TownShipDto
} from '../../../shared/service-proxies/service-proxies';
import { RejectModalComponent } from '../reject-modal/reject-modal.component';

@Component({
    selector: 'app-request-edit',
    templateUrl: './request-edit.component.html',
    styleUrls: ['./request-edit.component.css']
})
export class RequestEditComponent extends AppComponentBase implements OnInit {
    saving = false;
    startDatemodel: string = new Date().toLocaleDateString();
    completeDatemodel: string = new Date().toLocaleDateString();
    projectName: string = '';
    projectContractNumber: string = '';
    hide = false;
    hasSample = false;
    projectDepartmentId: number = 0;
    consultantId: number = 20;
    supervisingQualityId: number = 0;
    projectAgency: number = 0;
    selectedprojectItem: number = 0;
    hourse: number = 0;
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
    allTownShips: TownShipDto[] = [];
    minDate = moment(new Date()).format("YYYY-MM-DD");
    showTabs:boolean = false;
    showTestTab: boolean = false;
    attachment: any = {};
    attachments: AttachmentDto[] = [];
    file: any = {};
    baseURL = AppConsts.remoteServiceBaseUrl;
    constructor(
        injector: Injector,
        public _attachmentServiceProxy: AttachmentServiceProxy,
        private _departmentServiceProxy: DepartmentServiceProxy,
        public _towinShipServiceProxy: TowinShipServiceProxy,
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

    downloadMyFile(url, fileName) {
        console.log("url", url);
        var xhr = new XMLHttpRequest();
        xhr.open("GET", url, true);
        xhr.responseType = "blob";
        xhr.onload = function () {
            var urlCreator = window.URL || window.webkitURL;
            var imageUrl = urlCreator.createObjectURL(this.response);
            var tag = document.createElement('a');
            tag.href = imageUrl;
            tag.download = fileName;
            document.body.appendChild(tag);
            tag.click();
            document.body.removeChild(tag);
        }
        xhr.send();
    }
    handleFileInput(files: FileList) {
        var currentFile = files.item(0);
        this.file.data = currentFile;
        this.file.fileName = currentFile.name;
    }

    saveAttachment(): void {
        console.log('attachments', this.attachment);
        this.attachment.entity = 1;
        this.attachment.entityId = this.request.id;
        this._attachmentServiceProxy.createOrUpdate(this.request.id, '', '', '',this.file, '',this.attachment.description, 1).subscribe(
            () => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.loadAttachments();
                //  this.onSave.emit();
            },
            () => {
                //this.saving = false;
            }
        );
    }

    deleteAttachment(attachment: AttachmentDto) {

        this._attachmentServiceProxy.delete(attachment.id).subscribe(
            () => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.loadAttachments();
                this.attachment = {};
            },
            () => {
                this.saving = false;
            }
        );
    }

    ngOnInit(): void {
        this.loadAllProject();
        this.loadMainRequestTypes();
        this.loadTestTypes();
        this.request.id = this.routeActive.snapshot.params['id'];
        this._requestServiceProxy.get(this.request.id).subscribe(res => {
            this.request = res;
            if (this.request.hasSample ==1) {
                this.showTestTab = true;
            }
            this.LoadProject();
            this.hasSample = res.hasSample == 1 ? true : false;
            this.InspectionDatemodel = moment(this.request.inspectionDate).format("YYYY-MM-DD");
            this.loadRequestTests();
            this.loadTestsByTypes();
            this.LoadRequestHistory();
            this.loadRequestProjectItems();
            this.loadAllDepartments();
            this.loadAgencies();
            this.showTabs = true;
            this.loadAllTownShips();
            this.loadAttachments();
        });
    }
    loadAllDepartments() {
        this._departmentServiceProxy.getAllDepartmentDropDown().subscribe(res => {
            this.allDepartments = res;
        });
    }
    loadAttachments() {
        this._attachmentServiceProxy.getAllAttachment(1, this.request.id).subscribe(res => {
            this.attachments = res;
        });
    }
    loadAllTownShips() {
        this._towinShipServiceProxy.getAllownShipList().subscribe(res => {
            this.allTownShips = res;
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
            console.log('Project', this.appSession.userId);
            this.projectAgency = this.project.agencyId;
            this.supervisingQualityId = this.project.supervisingQualityId;
            this.consultantId = this.project.consultantId;
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

        this._requestWFServiceProxy.getAllHistory(this.request.id,1).subscribe(res => {
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
        if (!this.request.inspectionDate.isValid()) {
            this.notify.error(this.l('InspectionDateRequired'));
            return;
        }

        this._requestServiceProxy.createOrUpdate(this.request).subscribe(
            res => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.request = res;
                this.InspectionDatemodel = moment(this.request.inspectionDate).format("YYYY-MM-DD");
                this.loadTestsByTypes();
                this.saving = false;
               /* if (this.request.status == RequestStatus._2) {*/
                    this.saveWorkFlow();
              //  }
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
                    consultantId: this.appSession.userId,
                    request: this.request,
                },
            }
        );

        rejectModal.content.onSave.subscribe(() => {
            this.ngOnInit();
        });
    }

    saveWorkFlow() {
       
        var workFlow = new RequestWFDto();
        workFlow.requestId = this.request.id;
        workFlow.entity = 1;
        workFlow.currentUserId = this.appSession.userId;
        if (this.request.status == 2) {
            workFlow.actionName = "تم المراجعه";
            workFlow.actionNotes = "الإستشاري راجع الطلب";
        }

        if (this.request.status == 3) {
            workFlow.actionName = "تمت الموافقه على الطلب";
            workFlow.actionNotes = "الإستشاري وافق على الطلب";
        }

        if (this.request.status == 4) {
            workFlow.actionName = "تم إعتماد الطلب";
            workFlow.actionNotes = "تم إعتماد الطلب";
        }

        if (this.request.status == 7) {
            workFlow.actionName = " إلغاء الطلب";
            workFlow.actionNotes = "تم إلغاء الطلب";
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
            this.requestInspection = new CreateUpdateRequestTestDto();
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
