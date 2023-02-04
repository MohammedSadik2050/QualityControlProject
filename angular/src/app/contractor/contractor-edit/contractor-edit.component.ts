import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { AbpValidationError } from '../../../shared/components/validation/abp-validation.api';
import { ContractorDto, ContractorServiceProxy, CreateContractorDto, RegisterInput, UserDto, UserServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-contractor-edit',
  templateUrl: './contractor-edit.component.html',
  styleUrls: ['./contractor-edit.component.css']
})
export class ContractorEditComponent extends AppComponentBase
    implements OnInit {
    saving = false;
    contractor = new ContractorDto();
    currentUser = new UserDto();
    checkedRolesMap: { [key: string]: boolean } = {};
    id: number;

    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        public _contractorServiceProxy: ContractorServiceProxy,
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
        this._contractorServiceProxy.getById(this.id).subscribe((result) => {
            this.contractor = result;

            this._userServiceProxy.get(this.contractor.userId).subscribe((result2) => {
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

