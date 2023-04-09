import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AbpValidationError } from '../../../shared/components/validation/abp-validation.api';
import { AgencyDto, AgencyServiceProxy, QCUserCreateDto, QCUserDto, QCUserServiceProxy, RegisterInput, UserServiceProxy } from '../../../shared/service-proxies/service-proxies';

class UserTypes {
    name: string;
    arabicName: string;
    id: number | null;

}
@Component({
  selector: 'app-app-user-edit',
  templateUrl: './app-user-edit.component.html',
  styleUrls: ['./app-user-edit.component.css']
})
export class AppUserEditComponent extends AppComponentBase
    implements OnInit {
    saving = false;
    agencies: AgencyDto[] = [];
    userTypes: UserTypes[] = [];
    qcUser = new QCUserDto();
    currentUser = new RegisterInput();
    contractorObject = new QCUserCreateDto();
    checkedRolesMap: { [key: string]: boolean } = {};
    id: number;

    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        public _agencyServiceProxy: AgencyServiceProxy,
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
                this.currentUser.emailAddress = result2.emailAddress;
                this.currentUser.name = result2.name;
                this.currentUser.userName = result2.userName;
            });
        });

        this.addUserTypes();
        this.loadAgencies();

    }




    loadAgencies() {
        this._agencyServiceProxy.getAllAgenciesList().subscribe(res => {
            this.agencies = res;
        });
    }
    addUserTypes() {
        var userType = new UserTypes();
        userType.id = 1;
        userType.name = "Contractor";
        userType.arabicName = "مقاول";
        this.userTypes.push(userType);

        userType = new UserTypes();
        userType.id = 2;
        userType.name = "Consultant";
        userType.arabicName = "الاستشاري";
        this.userTypes.push(userType);

        userType = new UserTypes();
        userType.id = 3;
        userType.name = "ConsultingEngineer";
        userType.arabicName = "المهندس الاستشاري";
        this.userTypes.push(userType);

        userType = new UserTypes();
        userType.id = 4;
        userType.name = "SupervisingEngineer";
        userType.arabicName = "مهندس مشرف";
        this.userTypes.push(userType);

        userType = new UserTypes();
        userType.id = 5;
        userType.name = "LabProjectManager";
        userType.arabicName = "مدير المختبر";
        this.userTypes.push(userType);

        userType = new UserTypes();
        userType.id = 6;
        userType.name = "SupervisingQuality";
        userType.arabicName = "مشرف المواد";
        this.userTypes.push(userType);

        userType = new UserTypes();
        userType.id = 7;
        userType.name = "SupervisingProjects";
        userType.arabicName = "وكيل الجودة والامتثال";
        this.userTypes.push(userType);

        userType = new UserTypes();
        userType.id = 8;
        userType.name = "AmanaStaff";
        userType.arabicName = "موظف الامانة";
        this.userTypes.push(userType);

    }



    save(): void {
        this.saving = true;
        this.contractorObject.id = this.id;
        this.contractorObject.registerInput = this.currentUser;
        this.contractorObject.qcUserInput = this.qcUser;
        this.contractorObject.registerInput.surname = this.qcUser.name;
        this.contractorObject.registerInput.name = this.qcUser.name;
        this._qcUserServiceProxy.createOrUpdate(this.contractorObject).subscribe(
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

