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
    map: google.maps.Map;
    infoWindow: google.maps.InfoWindow;
    marker: google.maps.Marker;
    markerStartPosition = {
        lat: 24.748750734067904,
        lng: 46.72269763970338
    };
    markerDefaultPosition = {
        lat: 24.748750734067904,
        lng: 46.72269763970338
    };
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
    setMapLanAndLog
        (event: ClipboardEvent) {

        let clipboardData = event.clipboardData;
        let pastedText = clipboardData!.getData('text');
        var mapUrl = pastedText;

        if (mapUrl.indexOf('www.google.com') > -1) {
            var url = mapUrl.split('@');
            var at = url[1].split('z');
            var zero = at[0].split(',');
            var lat = zero[0];
            var lon = zero[1];
            this.markerStartPosition = {
                lat: Number(lat),
                lng: Number(lon)
            };
        } else {
            let lat = mapUrl.split(',')[0].trim();
            let lng = mapUrl.split(',')[1].trim();

            this.markerStartPosition = {
                lat: Number(lat),
                lng: Number(lng)
            };
        }

        this.initMap(this.markerStartPosition)
    }
    ngAfterViewInit(): void {
        this.initMap();
    }

    initMap(_pos?: any): void {
        var res = new google.maps.Map(document.getElementById("map"));
        this.map = new google.maps.Map(document.getElementById("map") as HTMLElement, {
            center: this.markerStartPosition,
            zoom: 6
        });
        //      this.infoWindow = new google.maps.InfoWindow();


        if (this.project.longitude && this.project.latitude) {
            let pos = {
                lat: this.project.latitude,
                lng: this.project.longitude
            };
            if (_pos) {
                pos = {
                    lat: _pos.lat,
                    lng: _pos.lng
                };
            }
            this.marker = new google.maps.Marker({
                position: pos,
                map: this.map,
                draggable: true,
              //  title: "My Store Location"
            });
           // this.infoWindow.setPosition(pos);
           // this.infoWindow.open(this.map);
            this.map.setCenter(pos);
            this.map.setZoom(15);
            this.marker.setPosition(pos);
        }
        else {
            this.marker = new google.maps.Marker({
                position: this.markerStartPosition,
                map: this.map,
                draggable: true,
                //  title: "My Store Location"
            });
            // this.infoWindow.setPosition(this.markerStartPosition);
            //  this.infoWindow.open(this.map);
            this.map.setCenter(this.markerStartPosition);
            this.map.setZoom(15);
            this.marker.setPosition(this.markerStartPosition);
        }
    }

    handleLocationError(
        browserHasGeolocation: boolean,
        infoWindow: google.maps.InfoWindow,
        pos: google.maps.LatLng
    ) {
        infoWindow.setPosition(pos);
        infoWindow.setContent(
            browserHasGeolocation
                ? "Error: The Geolocation service failed."
                : "Error: Your browser doesn't support geolocation."
        );
        infoWindow.open(this.map);
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
            this.initMap();
            this.changeDetectorRef.detectChanges();
        });

    }
    loadAllDepartments() {
        this._departmentServiceProxy.getAllDepartmentDropDown().subscribe(res => {
            this.allDepartments = res;
            this.onAgencyChange(this.project.agencyId, true);
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
    onTypeChange(event, isFirstLoad = false) {
        if (isFirstLoad === true) {
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
    getLocation() {
        return this.marker.getPosition();
    }
    save(status: number): void {
        let projectLocation = this.getLocation();
        if (projectLocation!.lat() === this.markerDefaultPosition.lat && projectLocation!.lng() === this.markerDefaultPosition.lng) {
            this.notify.error(this.l('PleaseSelectProjectLocation'));
            return;
        }
        this.saving = true;
        console.log("date", this.project.startDate);
        this.project.status = status;
        this.project.startDate = moment(this.startDatemodel, "YYYY-MM-DD");
        this.project.completedDate = moment(this.completeDatemodel, "YYYY-MM-DD");
        this.project.siteDelivedDate = moment(this.siteDelivedDatemodel, "YYYY-MM-DD");


        this.project.latitude = projectLocation!.lat();
        this.project.longitude = projectLocation!.lng();

        if (!this.project.startDate.isValid()) {
            this.notify.error(this.l('StartDateRequired'));
            return;
        }

        if (!this.project.completedDate.isValid()) {
            this.notify.error(this.l('CompletedDateRequired'));
            return;
        }

        if (!this.project.siteDelivedDate.isValid()) {
            this.notify.error(this.l('SiteDelivedDateRequired'));
            return;
        }


        this._projectServiceProxy.createOrUpdate(this.project).subscribe(
            () => {
                this.notify.info(this.l('SavedSuccessfully'));
                if (status > -1) {
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
        if (this.project.status == 0) {
            workFlow.actionName = "المقاول قام بإستكمال الطلب";
            workFlow.actionNotes = "المقاول قام بإستكمال الطلب";
        }

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
        this._attachmentServiceProxy.createOrUpdate(this.projectId, '', '', '', this.file, '', this.attachment.description, 2).subscribe(
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
}
