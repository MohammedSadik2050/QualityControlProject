import { Component, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '../../shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '../../shared/paged-listing-component-base';
import { DropdownListDto, LookupServiceProxy, ProjectDto, ProjectServiceProxy, RequestDto, RequestServiceProxy, RequestViewDto } from '../../shared/service-proxies/service-proxies';
import { RequestCreateComponent } from './request-create/request-create.component';

class PageRequestDto extends PagedRequestDto {
    keyword: string;
}
@Component({

    templateUrl: './requests.component.html',
    animations: [appModuleAnimation()]
})
export class RequestsComponent extends PagedListingComponentBase<RequestViewDto>{
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
        //private _modalService: BsModalService,
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

        this.router.navigateByUrl('/app/examinationRequest/view/' + row.id);
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
                this.projectId,0,
                this.contractNumber,
                this.requestCode,
                this.statusId,0,
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
                    //this._inspectionTestServiceProxy.delete(row.id).subscribe(() => {
                    //    abp.notify.success(this.l('SuccessfullyDeleted'));
                    //    this.refresh();
                    //});
                }
            }
        );
    }

    //private showResetPasswordUserDialog(id?: number): void {
    //    this._modalService.show(ResetPasswordDialogComponent, {
    //        class: 'modal-lg',
    //        initialState: {
    //            id: id,
    //        },
    //    });
    //}

    private showCreateOrEditUserDialog(id?: number): void {

        if (!id) {
            //createOrEditUserDialog = this._modalService.show(
            //    RequestCreateComponent,
            //    {
            //        class: 'modal-lg',
            //    }
            //);
            this.router.navigateByUrl('/app/examinationRequest/create');
        } else {
            this.router.navigateByUrl('/app/examinationRequest/edit/' + id);
            //let createOrEditUserDialog: BsModalRef;
            //createOrEditUserDialog = this._modalService.show(
            //    InspectionTestEditComponent,
            //    {
            //        class: 'modal-lg',
            //        initialState: {
            //            id: id,
            //        },
            //    }
            //);
        }

        //createOrEditUserDialog.content.onSave.subscribe(() => {
        //    this.refresh();
        //});
    }
}
