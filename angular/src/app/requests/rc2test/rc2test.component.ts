import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { request } from 'http';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { RC2Dto, RC2ServiceProxy, RequestServiceProxy, RequestWFDto, RequestWFServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-rc2test',
  templateUrl: './rc2test.component.html',
  styleUrls: ['./rc2test.component.css']
})
export class Rc2testComponent extends AppComponentBase implements OnInit {
    saving = false;
    @Output() onSave = new EventEmitter<any>();
    id: number;
    testId: number;
    requestTest = new RC2Dto();
    constructor(
        injector: Injector,
        public bsModalRef: BsModalRef,
        public _requestWFServiceProxy: RequestWFServiceProxy,
        public _rc2ServiceProxy: RC2ServiceProxy,
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.loadExistTest();
    }
    loadExistTest() {
        this._rc2ServiceProxy.getByRequestandTest(this.testId, this.id).subscribe(res => {

            this.requestTest = res;
        });
    }
    save(): void {
        this.saving = true;
        var workFlow = new RequestWFDto();
        workFlow.requestId = this.id;
        workFlow.currentUserId = this.appSession.userId;
        workFlow.actionName = "قام المراقب بإضافه نتيجه إختبار";
        workFlow.actionNotes = "المراقب أضاف اختبار RC2/MV2";

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
        this._rc2ServiceProxy.createOrUpdate(this.requestTest).subscribe(
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
