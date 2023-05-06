import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { AsphaltFieldDto, AsphaltFieldServiceProxy, RequestWFDto, RequestWFServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-asphalt-field',
  templateUrl: './asphalt-field.component.html',
  styleUrls: ['./asphalt-field.component.css']
})
export class AsphaltFieldComponent extends AppComponentBase implements OnInit {
    saving = false;
    @Output() onSave = new EventEmitter<any>();
    id: number;
    testId: number;
    requestTest = new AsphaltFieldDto();
    constructor(
        injector: Injector,
        public bsModalRef: BsModalRef,
        public _requestWFServiceProxy: RequestWFServiceProxy,
        public _asphaltFieldServiceProxy: AsphaltFieldServiceProxy,
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.loadExistTest();
    }
    loadExistTest() {
        this._asphaltFieldServiceProxy.getByRequestandTest(this.testId, this.id).subscribe(res => {

            this.requestTest = res;
        });
    }
    save(): void {
        this.saving = true;
        var workFlow = new RequestWFDto();
        workFlow.requestId = this.id;
        workFlow.currentUserId = this.appSession.userId;
        workFlow.actionName = "قام المراقب بإضافه نتيجه إختبار";
        workFlow.actionNotes = "المراقب أضاف اختبار حقلي";

        this._requestWFServiceProxy.createOrUpdate(workFlow).subscribe(
            () => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.saveRequestTest();

            },
            () => {
                this.saving = false;
            }
        );
    }


    saveRequestTest() {

        //this.request.status = 5;
        this.requestTest.requestId = this.id;
        this.requestTest.requestInspectionTestId = this.testId;
        this._asphaltFieldServiceProxy.createOrUpdate(this.requestTest).subscribe(
            res => {
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
