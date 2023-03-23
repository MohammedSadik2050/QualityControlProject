import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../shared/app-component-base';
import { ProjectDto, ProjectServiceProxy, RequestWFDto, RequestWFServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-project-reject-modal',
  templateUrl: './project-reject-modal.component.html',
  styleUrls: ['./project-reject-modal.component.css']
})
export class ProjectRejectModalComponent extends AppComponentBase implements OnInit {
    saving = false;
    @Output() onSave = new EventEmitter<any>();
    id: number;
    consultantId: number;
    comment: string;
    project = new ProjectDto();
    constructor(
        injector: Injector,
        public bsModalRef: BsModalRef,
        public _requestWFServiceProxy: RequestWFServiceProxy,
        public _projectServiceProxy: ProjectServiceProxy,
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
                this.saveRequest();

            },
            () => {
                this.saving = false;
            }
        );
    }


    saveRequest() {

        this.project.status = 4;
        this._projectServiceProxy.createOrUpdate(this.project).subscribe(
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
