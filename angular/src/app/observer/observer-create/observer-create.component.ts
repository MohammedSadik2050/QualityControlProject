import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { AbpValidationError } from '../../../shared/components/validation/abp-validation.api';
import { CreateUpdateObserverDto, ObserverDto, ObserverServiceProxy, TowinShipServiceProxy, TownShipDto, UserServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-observer-create',
  templateUrl: './observer-create.component.html',
  styleUrls: ['./observer-create.component.css']
})
export class ObserverCreateComponent extends AppComponentBase implements OnInit {
    saving = false;
    qcUser = new CreateUpdateObserverDto();
    townShips: TownShipDto[] = [];
    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        public _observerServiceProxy: ObserverServiceProxy,
        public _towinShipServiceProxy: TowinShipServiceProxy,
        public bsModalRef: BsModalRef,
        public authService: AppAuthService,
        private _sessionService: AbpSessionService,
        public _userService: UserServiceProxy,

    ) {
        super(injector);
    }
    passwordValidationErrors: Partial<AbpValidationError>[] = [
        {
            name: 'pattern',
            localizationKey:
                'PasswordsMustBeAtLeast8CharactersContainLowercaseUppercaseNumber',
        },
    ];
    confirmPasswordValidationErrors: Partial<AbpValidationError>[] = [
        {
            name: 'validateEqual',
            localizationKey: 'PasswordsDoNotMatch',
        },
    ];
    ngOnInit(): void {
        
        this.loadTownShips();
    }
    loadTownShips() {
        this._towinShipServiceProxy.getAllAgenciesList().subscribe(res => {
            this.townShips = res;
        });
    }
   


    save(): void {
        this.saving = true;

        this._observerServiceProxy.createOrUpdate(this.qcUser).subscribe(
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
