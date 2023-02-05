import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { AgencyServiceProxy, AgencyTypeDto, CreateUpdateAgencyDto } from '../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-agency-edit',
  templateUrl: './agency-edit.component.html',
  styleUrls: ['./agency-edit.component.css']
})
export class AgencyEditComponent extends AppComponentBase implements OnInit {
    saving = false;
    agency = new CreateUpdateAgencyDto();
    agencyTypes: AgencyTypeDto[] = [];
    id: number;
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
            this.loadAgency(this.id)
        });
    }

    loadAgency(id:number) {

        this._agencyServiceProxy.get(id).subscribe(res => {
            this.agency = res;
        });
    }

    save(): void {
        this.saving = true;
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
