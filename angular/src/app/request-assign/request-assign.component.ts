import { Component, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '../../shared/paged-listing-component-base';
import { DropdownListDto, LookupServiceProxy, ProjectDto, ProjectServiceProxy, RequestDto, RequestServiceProxy, RequestViewDto } from '../../shared/service-proxies/service-proxies';
import { AssignModalComponent } from './assign-modal/assign-modal.component';
class PageRequestDto extends PagedRequestDto {
    keyword: string;
}
@Component({
    selector: 'app-request-assign',
    templateUrl: './request-assign.component.html',
    styleUrls: ['./request-assign.component.css']
})
export class RequestAssignComponent extends PagedListingComponentBase<RequestViewDto>{
    requests: RequestViewDto[] = [];
    projects: ProjectDto[] = [];
    keyword = '';
    projectId: number = 0;
    contractNumber: string = '';
    requestCode: string = '';
    statusId: number = 0;
    isActive: boolean | null;
    advancedFiltersVisible = false;
    requestStatuses: DropdownListDto[] = [];
    constructor(
        injector: Injector,
        public _projectServiceProxy: ProjectServiceProxy,
        private _requestServiceProxy: RequestServiceProxy,
        public _lookupServiceProxy: LookupServiceProxy,
        private _modalService: BsModalService,
        private router: Router
    ) {
        super(injector);
    }

    loadAllProject() {

        this._projectServiceProxy.getAllDropdown().subscribe(res => {
            this.projects = res;
        });
    }

    loadAllStatuses() {

        this._lookupServiceProxy.requestsStatus().subscribe(res => {
            this.requestStatuses = res;
        });
    }
    createAgency(): void {
        this.showCreateOrEditUserDialog();
    }

    editRow(row: RequestDto): void {
        this.showCreateOrEditUserDialog(row.id);
    }

    viewRow(row: any) {

        //this.router.navigateByUrl('/app/examinationRequest/view/' + row.id);
        window.open('/app/examinationRequest/view/' + row.id, "_blank");
    }



    clearFilters(): void {
        this.keyword = '';
        this.isActive = undefined;
        this.projectId = undefined;
        this.contractNumber = undefined;
        this.requestCode = undefined;
        this.statusId = undefined;
        this.getDataPage(1);
    }

    protected list(
        request: PageRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;
        this.loadAllProject();
        this.loadAllStatuses();
        this._requestServiceProxy
            .getAll(
                this.projectId, 0,
                this.contractNumber,
                this.requestCode,
                4,
                '',
                request.skipCount,
                request.maxResultCount
            )
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: PagedResultDto) => {
                this.requests = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    protected delete(row: RequestViewDto): void {
        abp.message.confirm(
            this.l('', row.name),
            undefined,
            (result: boolean) => {
                if (result) {

                }
            }
        );
    }


    private showCreateOrEditUserDialog(id?: number): void {


        let createOrEditUserDialog: BsModalRef;
        createOrEditUserDialog = this._modalService.show(
            AssignModalComponent,
            {
                class: 'modal-lg',
                initialState: {
                    id: id,
                },
            }
        );

        createOrEditUserDialog.content.onSave.subscribe(() => {
            this.refresh();
        });

    }
}
