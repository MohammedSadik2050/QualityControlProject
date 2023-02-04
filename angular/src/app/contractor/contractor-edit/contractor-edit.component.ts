import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { AbpValidationError } from '../../../shared/components/validation/abp-validation.api';
import { QCUserDto, QCUserServiceProxy, QCUserCreateDto, CreateUserDto, RegisterInput, RoleServiceProxy, UserServiceProxy, UserDto } from './../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-contractor-edit',
  templateUrl: './contractor-edit.component.html',
  styleUrls: ['./contractor-edit.component.css']
})
export class ContractorEditComponent extends AppComponentBase
    implements OnInit {
    saving = false;
    qcUser = new QCUserDto(); 
    currentUser = new UserDto();
    checkedRolesMap: { [key: string]: boolean } = {};
    id: number;

    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        public _qcUserServiceProxy: QCUserServiceProxy,
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
        this._qcUserServiceProxy.getById(this.id).subscribe((result) => {
            this.qcUser = result;

            this._userServiceProxy.get(this.qcUser.userId).subscribe((result2) => {
                this.currentUser = result2;
            });
        });
    }

   
 

    

   

    save(): void {
        this.saving = true;

        //this._userService.update(this.user).subscribe(
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

