import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { AgencyServiceProxy, AgencyTypeDto, ProjectDto } from '../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.css']
})
export class CreateProjectComponent extends AppComponentBase implements OnInit {
    saving = false;
    project = new ProjectDto();
    agencyTypes: AgencyTypeDto[] = [];
    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        public _agencyServiceProxy: AgencyServiceProxy,
        public bsModalRef: BsModalRef,
        public authService: AppAuthService,
        private _sessionService: AbpSessionService,

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this._agencyServiceProxy.getAllAgencyTypeList().subscribe(res => {
            this.agencyTypes = res;
        });
    }



    save(): void {
        this.saving = true;
        //this._agencyServiceProxy.createOrUpdate(this.agency).subscribe(
        //    () => {
        //        this.notify.info(this.l('SavedSuccessfully'));
        //        this.bsModalRef.hide();
        //        this.onSave.emit();
        //    },
        //    () => {
        //        this.saving = false;
        //    }
        //);
    }
}