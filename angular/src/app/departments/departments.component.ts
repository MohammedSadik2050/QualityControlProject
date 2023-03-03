import { Component, Injector, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '../../shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '../../shared/paged-listing-component-base';
import { AgencyDto, AgencyServiceProxy, AgencyTypeDto, DepartmentDto, DepartmentServiceProxy } from '../../shared/service-proxies/service-proxies';
import { DepartmentCreateComponent } from './department-create/department-create.component';
import { DepartmentEditComponent } from './department-edit/department-edit.component';

class PagedDepartmentRequestDto extends PagedRequestDto {
    keyword: string;
    currentAgencyId: number | null;
}

@Component({
  
  templateUrl: './departments.component.html',
    animations: [appModuleAnimation()]
})
export class DepartmentsComponent extends PagedListingComponentBase<DepartmentDto> {
    departments: DepartmentDto[] = [];
    agencies: AgencyDto[] = [];
    agencyTypes: AgencyTypeDto[] = [];
    allAgencies: AgencyDto[] = [];
    departmentAgencyTypeId = 0;
    keyword = '';
    departmentAgencyId: number | null;
    advancedFiltersVisible = false;

    constructor(
        injector: Injector,
        private _agencyServiceProxy: AgencyServiceProxy,
        private _departmentServiceProxy: DepartmentServiceProxy,
        private _modalService: BsModalService
    ) {
        super(injector);
    }

    createAgency(): void {
        this.showCreateOrEditUserDialog();
    }

    edit(row: DepartmentDto): void {
        this.showCreateOrEditUserDialog(row.id);
    }



    clearFilters(): void {
        this.keyword = '';
        this.departmentAgencyId = undefined;
        this.getDataPage(1);
    }

    protected list(
        request: PagedDepartmentRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        if (this.allAgencies.length<1) {
            this.loadAgencyTypes();
            this.loadAgencies();
        }
      
        //request.keyword = this.keyword;
        //request.currentAgencyId = this.agencyId;

        this._departmentServiceProxy
            .getAll(
                this.departmentAgencyId,
                this.keyword,
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
                this.departments = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    protected delete(row: DepartmentDto): void {
        abp.message.confirm(
            this.l('', row.name),
            undefined,
            (result: boolean) => {
                if (result) {
                    this._departmentServiceProxy.delete(row.id).subscribe(() => {
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                        this.refresh();
                    });
                }
            }
        );
    }

    loadAgencies() {
        this._agencyServiceProxy.getAllAgenciesList().subscribe(res => {
            this.allAgencies = res;
        });
    }
    loadAgencyTypes() {

        this._agencyServiceProxy.getAllAgencyTypeList().subscribe(res => {
            this.agencyTypes = res;
        });
    }

    onTypeChange(event) {
        this.departmentAgencyId = 0;
        this.agencies = this.allAgencies.filter(s => s.agencyTypeId == event);
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
        let createOrEditUserDialog: BsModalRef;
        if (!id) {
            createOrEditUserDialog = this._modalService.show(
                DepartmentCreateComponent,
                {
                    class: 'modal-lg',
                }
            );
        } else {
            createOrEditUserDialog = this._modalService.show(
                DepartmentEditComponent,
                {
                    class: 'modal-lg',
                    initialState: {
                        id: id,
                    },
                }
            );
        }

        createOrEditUserDialog.content.onSave.subscribe(() => {
            this.refresh();
        });
    }
}
