import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { RequestDto, RequestServiceProxy, RequestWFDto, RequestWFServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
    selector: 'app-reject-modal',
    templateUrl: './reject-modal.component.html',
    styleUrls: ['./reject-modal.component.css']
})
export class RejectModalComponent extends AppComponentBase implements OnInit {
    saving = false;
    @Output() onSave = new EventEmitter<any>();
    id: number;
    consultantId: number;
    comment: string;
    request = new RequestDto();
    constructor(
        injector: Injector,
        public bsModalRef: BsModalRef,
        public _requestWFServiceProxy: RequestWFServiceProxy,
        public _requestServiceProxy: RequestServiceProxy,
    ) {
        super(injector);
    }

    ngOnInit(): void {
    }

    save(): void {
        this.saving = true;
        var workFlow = new RequestWFDto();
        workFlow.requestId = this.id;
        workFlow.currentUserId = this.consultantId;
        workFlow.actionName = "تم رفض الطلب";
        workFlow.actionNotes = this.comment;

        this._requestWFServiceProxy.createOrUpdate(workFlow).subscribe(
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


    saveRequest() {
      
        this.request.status = 4;
        this._requestServiceProxy.createOrUpdate(this.request).subscribe(
            res => {
                this.notify.info(this.l('SavedSuccessfully'));
            },
            () => {
                this.saving = false;
            }
        );
    }

}
