import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { AbpValidationError } from '../../../shared/components/validation/abp-validation.api';
import { AgencyDto, AgencyServiceProxy, AgencyTypeDto, CreateUpdateAgencyDto } from '../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-agency-create',
  templateUrl: './agency-create.component.html',
  styleUrls: ['./agency-create.component.css']
})
export class AgencyCreateComponent extends AppComponentBase implements OnInit {
    saving = false;
    agency = new CreateUpdateAgencyDto();
    agencyTypes:AgencyTypeDto[] =[];
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

        this.agency.creatorUserId = this._sessionService.userId;
        this._agencyServiceProxy.createOrUpdate(this.agency).subscribe(
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
