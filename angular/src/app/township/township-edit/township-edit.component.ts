import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { CreateUpdateTownShipDto, TowinShipServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-township-edit',
  templateUrl: './township-edit.component.html',
  styleUrls: ['./township-edit.component.css']
})
export class TownshipEditComponent extends AppComponentBase implements OnInit {
    saving = false;
    id: number;
  
    township = new CreateUpdateTownShipDto();
    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        public _towinShipServiceProxy: TowinShipServiceProxy,
        public bsModalRef: BsModalRef,
        public authService: AppAuthService,
        private _sessionService: AbpSessionService,

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this._towinShipServiceProxy.get(this.id).subscribe(res => {
            this.township = res;
        });
    }
 
    save(): void {
        this.saving = true;
        this._towinShipServiceProxy.createOrUpdate(this.township).subscribe(
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
