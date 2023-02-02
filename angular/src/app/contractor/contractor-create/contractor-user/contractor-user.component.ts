import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../../shared/app-component-base';
import { AppAuthService } from '../../../../shared/auth/app-auth.service';
import { AbpValidationError } from '../../../../shared/components/validation/abp-validation.api';
import { Contractor, ContractorDto, ContractorServiceProxy, CreateContractorDto, CreateUserDto, RegisterInput, RoleServiceProxy, UserServiceProxy } from '../../../../shared/service-proxies/service-proxies';

@Component({
    selector: 'app-contractor-user',
    templateUrl: './contractor-user.component.html',
    styleUrls: ['./contractor-user.component.css']
})
export class ContractorUserComponent extends AppComponentBase implements OnInit {
    saving = false;
    contractor = new ContractorDto(); 
    currentUser = new RegisterInput(); 
    contractorObject = new CreateContractorDto();
    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        public _contractorServiceProxy: ContractorServiceProxy,
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
       
    }

   

    save(): void {
        this.saving = true;

        this.contractor.userId = this._sessionService.userId;
        this.contractorObject.contractorInfo = this.contractor;
        this.contractorObject.registerInput = this.currentUser;

        this._contractorServiceProxy.create(this.contractorObject).subscribe(
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
