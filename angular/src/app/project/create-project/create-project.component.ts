import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AbpSessionService } from 'abp-ng2-module';
import * as moment from 'moment';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { AgencyDto, AgencyServiceProxy, AgencyTypeDto, DepartmentDto, DepartmentServiceProxy, DropdownListDto, LookupServiceProxy, ProjectDto, ProjectServiceProxy } from '../../../shared/service-proxies/service-proxies';

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
    departments: DepartmentDto[] = [];
    allDepartments: DepartmentDto[] = [];
    supervisingEngineers: DropdownListDto[] = [];
    consultants: DropdownListDto[] = [];
    contractors: DropdownListDto[] = [];
    projectManagers: DropdownListDto[] = [];
    supervisingQualities: DropdownListDto[] = [];
    @Output() onSave = new EventEmitter<any>();
    minDate = moment(this.project.startDate).format("YYYY-MM-DD");
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
    constructor(
        injector: Injector,
        private router: Router,
        private _projectServiceProxy: ProjectServiceProxy,
        private _agencyServiceProxy: AgencyServiceProxy,
        private _lookupServiceProxy: LookupServiceProxy,
        private _departmentServiceProxy: DepartmentServiceProxy,
        public bsModalRef: BsModalRef,
        private authService: AppAuthService,
        private _sessionService: AbpSessionService,

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
      //  this.infoWindow = new google.maps.InfoWindow();

        //if (this.storeId && this.oldStoreLocation) {
        //    if (this.oldStoreLocation.longitude && this.oldStoreLocation.latitude) {
        //        let pos = {
        //            lat: this.oldStoreLocation.latitude,
        //            lng: this.oldStoreLocation.longitude
        //        };
        //        if (_pos) {
        //            pos = {
        //                lat: _pos.lat,
        //                lng: _pos.lng
        //            };
        //        }
        //        this.marker = new google.maps.Marker({
        //            position: pos,
        //            map: this.map,
        //            draggable: true,
        //            title: "My Store Location"
        //        });
        //        this.infoWindow.setPosition(pos);
        //        this.infoWindow.open(this.map);
        //        this.map.setCenter(pos);
        //        this.map.setZoom(15);
        //        this.marker.setPosition(pos);
        //    }
        //} else {
            this.marker = new google.maps.Marker({
                position: this.markerStartPosition,
                map: this.map,
                draggable: true,
              //  title: "My Store Location"
            });
           // this.infoWindow.setPosition(this.markerStartPosition);
           // this.infoWindow.open(this.map);
            this.map.setCenter(this.markerStartPosition);
            this.map.setZoom(15);
            this.marker.setPosition(this.markerStartPosition);
       // }
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
    ngOnInit(): void {
        this.loadAgencyTypes();
        this.loadAgencies();
        this.loadSupervisingEngineers();
        this.loadConsultants();
        this.loadContractors();
        this.loadProjectManagers();
        this.loadSupervisingQualities();
        this.loadAllDepartments();

       
    }
    loadAllDepartments() {
        this._departmentServiceProxy.getAllDepartmentDropDown().subscribe(res => {
            this.allDepartments = res;
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
        });
    }
    loadAgencyTypes() {

        this._agencyServiceProxy.getAllAgencyTypeList().subscribe(res => {
            this.agencyTypes = res;
        });
    }
    onTypeChange(event) {
        this.project.agencyId = 0;
        this.agencies = this.allAgencies.filter(s => s.agencyTypeId == event);
    }

    onAgencyChange(event) {
        console.log(event);
        this.project.departmentId = 0;
        this.departments = this.allDepartments.filter(s => s.agencyId == event);
    }
    getLocation() {
        return this.marker.getPosition();
    }
    save(): void {
        let projectLocation = this.getLocation();
        if (projectLocation!.lat() === this.markerDefaultPosition.lat && projectLocation!.lng() === this.markerDefaultPosition.lng) {
            this.notify.error(this.l('PleaseSelectProjectLocation'));
            return;
        }
        this.project.latitude = projectLocation!.lat();
        this.project.longitude = projectLocation!.lng();
        this.saving = true;
        this.project.startDate = moment(this.project.startDate, "YYYY-MM-DD");
        this.project.completedDate = moment(this.project.completedDate, "YYYY-MM-DD");
        this.project.siteDelivedDate = moment(this.project.siteDelivedDate, "YYYY-MM-DD");

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
        this.project.status = -1;
        this._projectServiceProxy.createOrUpdate(this.project).subscribe(
            res => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.bsModalRef.hide();
                //this.onSave.emit();
                this.router.navigateByUrl('/app/projects/edit/' + res.id);
            },
            () => {
                this.saving = false;
            }
        );
    }
}
