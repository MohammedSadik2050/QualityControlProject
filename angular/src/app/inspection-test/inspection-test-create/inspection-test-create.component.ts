import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { CreateUpdateInspectionTestDto, DropdownListDto, InspectionTestServiceProxy, InspectionTestTypes, LookupServiceProxy } from '../../../shared/service-proxies/service-proxies';

class InspectionTestTypesDto {
    keyword: string;
    isActive: boolean | null;
}
@Component({
  selector: 'app-inspection-test-create',
  templateUrl: './inspection-test-create.component.html',
  styleUrls: ['./inspection-test-create.component.css']
})

export class InspectionTestCreateComponent extends AppComponentBase implements OnInit {
    saving = false;
    inspectionTest = new CreateUpdateInspectionTestDto();
    inspectionTestTypes: DropdownListDto []= [];
    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        public _inspectionTestServiceProxy: InspectionTestServiceProxy,
        public _lookupServiceProxy: LookupServiceProxy,
        public bsModalRef: BsModalRef,
        public authService: AppAuthService,
        private _sessionService: AbpSessionService,

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this._lookupServiceProxy.inspectionTestTypes().subscribe(res => {
            this.inspectionTestTypes = res;
        });
    }



    save(): void {
        this.saving = true;
        this._inspectionTestServiceProxy.createOrUpdate(this.inspectionTest).subscribe(
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
