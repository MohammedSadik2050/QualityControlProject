import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AbpValidationError } from '../../../shared/components/validation/abp-validation.api';
import { CreateUpdateObserverDto, ObserverDto, ObserverServiceProxy, TowinShipServiceProxy, TownShipDto, UserServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
    selector: 'app-observer-edit',
    templateUrl: './observer-edit.component.html',
    styleUrls: ['./observer-edit.component.css']
})
export class ObserverEditComponent extends AppComponentBase
    implements OnInit {
    saving = false;
    townShips: TownShipDto[] = [];
    qcUser = new ObserverDto();
    observerObject = new CreateUpdateObserverDto();
    id: number;

    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        public _observerServiceProxy: ObserverServiceProxy,
        public _towinShipServiceProxy: TowinShipServiceProxy,
        public _userServiceProxy: UserServiceProxy,
        public bsModalRef: BsModalRef
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
        this._observerServiceProxy.getById(this.id).subscribe((result) => {
            this.qcUser = result;
            this.observerObject.id = this.qcUser.id;
            this.observerObject.userId = this.qcUser.userId;
            this.observerObject.name = this.qcUser.name;
            this.observerObject.nationalId = this.qcUser.nationalId;
            this.observerObject.nationalityName = this.qcUser.nationalityName;
            this.observerObject.townShipId = this.qcUser.townShipId;
            this.observerObject.phoneNumber = this.qcUser.phoneNumber;
            this.observerObject.address = this.qcUser.address;

            this._userServiceProxy.get(this.qcUser.userId).subscribe((result2) => {
                this.observerObject.emailAddress = result2.emailAddress;
                this.observerObject.name = result2.name;
                this.observerObject.userName = result2.userName;
            });
        });

       
        this.LoadTownShips();

    }




    LoadTownShips() {
        this._towinShipServiceProxy.getAllAgenciesList().subscribe(res => {
            this.townShips = res;
        });
    }




    save(): void {
        this.saving = true;
       
        this._observerServiceProxy.createOrUpdate(this.observerObject).subscribe(
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

