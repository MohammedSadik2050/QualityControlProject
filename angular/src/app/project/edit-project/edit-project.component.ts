import { ChangeDetectorRef, Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AbpSessionService } from 'abp-ng2-module';
import * as moment from 'moment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppConsts } from '../../../shared/AppConsts';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { AgencyDto, AgencyServiceProxy, AgencyTypeDto, AttachmentDto, AttachmentServiceProxy, DepartmentDto, DepartmentServiceProxy, DropdownListDto, FileParameter, LookupServiceProxy, ProjectDto, ProjectItemDto, ProjectServiceProxy, RequestWFDto, RequestWFHistoryDto, RequestWFServiceProxy } from '../../../shared/service-proxies/service-proxies';
import { ProjectRejectModalComponent } from '../project-reject-modal/project-reject-modal.component';

@Component({
    selector: 'app-edit-project',
    templateUrl: './edit-project.component.html',
    styleUrls: ['./edit-project.component.css']
})
export class EditProjectComponent extends AppComponentBase implements OnInit {
    saving = false;
    startDatemodel: string = new Date().toLocaleDateString();
    completeDatemodel: string = new Date().toLocaleDateString();
    siteDelivedDatemodel: string = new Date().toLocaleDateString();
    project = new ProjectDto();
    projectItems: ProjectItemDto[] = [];
    projectItem: ProjectItemDto = new ProjectItemDto();
    agencyTypes: AgencyTypeDto[] = [];
    agencies: AgencyDto[] = [];
    allAgencies: AgencyDto[] = [];
    supervisingEngineers: DropdownListDto[] = [];
    consultants: DropdownListDto[] = [];
    contractors: DropdownListDto[] = [];
    projectManagers: DropdownListDto[] = [];
    supervisingQualities: DropdownListDto[] = [];
    departments: DepartmentDto[] = [];
    allDepartments: DepartmentDto[] = [];
    @Output() onSave = new EventEmitter<any>();
    projectId: number = 0;
    minDate = moment(this.project.startDate).format("YYYY-MM-DD");
    consultantId: number = 0;
    requestHistories: RequestWFHistoryDto[] = [];
    file: any = {};
    baseURL = AppConsts.remoteServiceBaseUrl;

    attachment: any = {};
    attachments: AttachmentDto[] = [];
    entityId: number;
    constructor(
        injector: Injector,
        private router: Router,
        private _departmentServiceProxy: DepartmentServiceProxy,
        public _projectServiceProxy: ProjectServiceProxy,
        public _agencyServiceProxy: AgencyServiceProxy,
        public _lookupServiceProxy: LookupServiceProxy,
        public authService: AppAuthService,
        private _sessionService: AbpSessionService,
        private route: ActivatedRoute,
        private changeDetectorRef: ChangeDetectorRef,
        private _modalService: BsModalService,
        public _requestWFServiceProxy: RequestWFServiceProxy,
        public _attachmentServiceProxy: AttachmentServiceProxy,

    ) {
        super(injector);
    }

    downloadMyFile(url) {
        const link = document.createElement('a');
        link.setAttribute('target', '_blank');
        link.setAttribute('href', url);
        link.setAttribute('download', `products.csv`);
        document.body.appendChild(link);
        link.click();
        link.remove();
    }
    handleFileInput(files: FileList) {
        var currentFile = files.item(0);
        this.file.data = currentFile;
        this.file.fileName = currentFile.name;
    }
    ngOnInit(): void {
        this.projectId = this.route.snapshot.params['id'];
        this._projectServiceProxy.get(this.projectId).subscribe(res => {
            this.project = res;
            this.loadAttachments();
            this.startDatemodel = moment(this.project.startDate).format("YYYY-MM-DD");
            this.completeDatemodel = moment(this.project.completedDate).format("YYYY-MM-DD");
            this.siteDelivedDatemodel = moment(this.project.siteDelivedDate).format("YYYY-MM-DD");
            this.loadAllDepartments();
            this.loadAgencyTypes();
            this.loadAgencies();
            this.loadSupervisingEngineers();
            this.loadConsultants();
            this.loadContractors();
            this.loadProjectManagers();
            this.loadSupervisingQualities();
            this.loadProjectItems();
            this.LoadRequestHistory();
            console.log('user', this.appSession.userId);
            this.consultantId = this.project.contractorId;
            console.log('conss', this.consultantId)
            this.changeDetectorRef.detectChanges();
        });

    }
    loadAllDepartments() {
        this._departmentServiceProxy.getAllDepartmentDropDown().subscribe(res => {
            this.allDepartments = res;
            this.onAgencyChange(this.project.agencyId,true);
        });
    }

    loadAttachments() {
        this._attachmentServiceProxy.getAllAttachment(2, this.projectId).subscribe(res => {
            this.attachments = res;
        });
    }
    onAgencyChange(event, isFirstLoad = false) {
        console.log(event);
        if (isFirstLoad == true) {
            this.departments = this.allDepartments.filter(s => s.agencyId == event);
        } else {
            this.project.departmentId = 0;
            this.departments = this.allDepartments.filter(s => s.agencyId == event);
        }
   
    }
    LoadRequestHistory() {

        this._requestWFServiceProxy.getAllHistory(this.project.id, 2).subscribe(res => {
            this.requestHistories = res;
            console.log('History', this.requestHistories);
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
            this.onTypeChange(this.project.agencyTypeId, true);
         
           
        });
    }
    loadAgencyTypes() {

        this._agencyServiceProxy.getAllAgencyTypeList().subscribe(res => {
            this.agencyTypes = res;
        
            this.project.agencyTypeId = this.project.agencyTypeId;
        });
    }
    onTypeChange(event, isFirstLoad=false) {
        if (isFirstLoad ===true) {
          //  this.project.agencyId = 0;
            this.agencies = this.allAgencies.filter(s => s.agencyTypeId == event);
            this.project.agencyId = this.project.agencyId;
        } else {
            this.project.agencyId = 0;
            this.project.departmentId = 0;
            this.agencies = this.allAgencies.filter(s => s.agencyTypeId == event);
            this.onAgencyChange(0);
        }
      
     
        //this.changeDetectorRef.detectChanges();
        //console.log("data", this.agencies);
        //console.log("value", this.project.agencyId);
    }

    save(status:number): void {
        this.saving = true;
        console.log("date", this.project.startDate);
        this.project.status = status;
        this.project.startDate = moment(this.startDatemodel, "YYYY-MM-DD");
        this.project.completedDate = moment(this.completeDatemodel, "YYYY-MM-DD");
        this.project.siteDelivedDate = moment(this.siteDelivedDatemodel, "YYYY-MM-DD");
        this._projectServiceProxy.createOrUpdate(this.project).subscribe(
            () => {
                this.notify.info(this.l('SavedSuccessfully'));
                if (status >0) {
                    this.saveWorkFlow();
                }
               
                this.onSave.emit();
            },
            () => {
                this.saving = false;
            }
        );
    }
    saveWorkFlow() {

        var workFlow = new RequestWFDto();
        workFlow.requestId = this.project.id;
        workFlow.entity = 2;
        workFlow.currentUserId = this.appSession.userId;
        if (this.project.status == 1) {
            workFlow.actionName = "تم مراجعه الطلب";
            workFlow.actionNotes = "تم الإرسال الى الجهة المشرفة ";
        }

        if (this.project.status == 2) {
            workFlow.actionName = "تمت إعتماد الطلب";
            workFlow.actionNotes = "الجهه المشرفت وافقت على الطلب";
        }

        if (this.project.status == 3) {
            workFlow.actionName = "تم تفعيل الطلب";
            workFlow.actionNotes = "تم تسجيل المشروع بالنظام";
        }


        this._requestWFServiceProxy.createOrUpdate(workFlow).subscribe(res => {
            this.router.navigateByUrl('/app/projects');
        });
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
    deleteItem(projectItem: ProjectItemDto) {

        this._projectServiceProxy.deleteProjectItem(projectItem.id).subscribe(
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

    rejectRequest() {
        let rejectModal: BsModalRef;
        rejectModal = this._modalService.show(
            ProjectRejectModalComponent,
            {
                class: 'modal-lg',
                initialState: {
                    id: this.project.id,
                    consultantId: this.appSession.userId,
                    request: this.project,
                },
            }
        );

        rejectModal.content.onSave.subscribe(() => {
            this.ngOnInit();
        });
    }

    saveAttachment(): void {
        console.log('attachments', this.attachment);
        this.attachment.entity = 2;
        this.attachment.entityId = this.projectId;
        //const formData = new FormData();
        //formData.append('file', this.postForm.get('title').value);
        //formData.append('description', this.attachment.description);
        //formData.append('entity', '2');
        //formData.append('entityId', this.attachment.entityId.toString());
        this._attachmentServiceProxy.createOrUpdate(this.projectId, '', '', this.file, this.attachment.description,2).subscribe(
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
                this.loadProjectItems();
                this.attachment = {};
            },
            () => {
                this.saving = false;
            }
        );
    }
}
