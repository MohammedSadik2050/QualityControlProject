import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AppAuthService } from '../../../shared/auth/app-auth.service';
import { CreateUpdateInspectionTestDto, DropdownListDto, InspectionTestServiceProxy, LookupServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-assign-test-form',
  templateUrl: './assign-test-form.component.html',
  styleUrls: ['./assign-test-form.component.css']
})
export class AssignTestFormComponent extends AppComponentBase implements OnInit {
    saving = false;
    inspectionTest = new CreateUpdateInspectionTestDto();
    inspectionTestTypes: DropdownListDto[] = [];
    testForms: DropdownListDto[] = [];
    id: number;
    @Output() onSave = new EventEmitter<any>();
    notify: any;

    constructor(
        injector: Injector,
        public _lookupServiceProxy: LookupServiceProxy,
        public _inspectionTestServiceProxy: InspectionTestServiceProxy,
        public bsModalRef: BsModalRef,
        public authService: AppAuthService,
        private _sessionService: AbpSessionService,

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.loadTestForms();
        this._lookupServiceProxy.inspectionTestTypes().subscribe(res => {
            this.inspectionTestTypes = res;
            this.loadInspectionTest(this.id)
        });
    }

    loadInspectionTest(id: number) {

        this._inspectionTestServiceProxy.get(id).subscribe(res => {
            this.inspectionTest = res;
        });
    }

    loadTestForms() {

        this._lookupServiceProxy.testForms().subscribe(res => {
            this.testForms = res;
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
