import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import * as moment from 'moment';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { ConcretFieldDto, ConcretFieldServiceProxy, RequestWFDto, RequestWFServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-concrete-field',
  templateUrl: './concrete-field.component.html',
  styleUrls: ['./concrete-field.component.css']
})
export class ConcreteFieldComponent extends AppComponentBase implements OnInit {
    saving = false;
    @Output() onSave = new EventEmitter<any>();
    id: number;
    testId: number;
    requestTest = new ConcretFieldDto();

    samplePreparationDatemodel: string = new Date().toLocaleDateString();
    samplePreparationEndDatemodel: string = new Date().toLocaleDateString();
    truckLeftDatemodel: string = new Date().toLocaleDateString();
    castingStartmodel: string = new Date().toLocaleDateString();
    truckSiteArrivingDatemodel: string = new Date().toLocaleDateString();
    constructor(
        injector: Injector,
        public bsModalRef: BsModalRef,
        public _requestWFServiceProxy: RequestWFServiceProxy,
        public _concretFieldServiceProxy: ConcretFieldServiceProxy,
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.loadExistTest();
    }
    loadExistTest() {
        this._concretFieldServiceProxy.getByRequestandTest(this.testId, this.id).subscribe(res => {

            this.requestTest = res;
            this.samplePreparationDatemodel = moment(this.requestTest.samplePreparationDate).format("YYYY-MM-DD");
            this.samplePreparationEndDatemodel = moment(this.requestTest.samplePreparationEndDate).format("YYYY-MM-DD");
            this.truckLeftDatemodel = moment(this.requestTest.truckLeftDate).format("YYYY-MM-DD");
            this.castingStartmodel = moment(this.requestTest.castingStartDate).format("YYYY-MM-DD");
            this.truckSiteArrivingDatemodel = moment(this.requestTest.truckSiteArrivingDate).format("YYYY-MM-DD");
        });
    }
    save(): void {
        this.saving = true;
        var workFlow = new RequestWFDto();
        workFlow.requestId = this.id;
        workFlow.currentUserId = this.appSession.userId;
        workFlow.actionName = "قام المراقب بإضافه نتيجه إختبار";
        workFlow.actionNotes = "المراقب أضاف اختبار خرساني";

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
        this.requestTest.samplePreparationDate = moment(this.samplePreparationDatemodel, "YYYY-MM-DD");
        this.requestTest.samplePreparationEndDate = moment(this.samplePreparationEndDatemodel, "YYYY-MM-DD");
        this.requestTest.truckLeftDate = moment(this.truckLeftDatemodel, "YYYY-MM-DD");
        this.requestTest.castingStartDate = moment(this.castingStartmodel, "YYYY-MM-DD");
        this.requestTest.truckSiteArrivingDate = moment(this.truckSiteArrivingDatemodel, "YYYY-MM-DD");
        this._concretFieldServiceProxy.createOrUpdate(this.requestTest).subscribe(
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
