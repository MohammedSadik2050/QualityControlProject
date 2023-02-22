import { Component, Injector, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '../../shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '../../shared/paged-listing-component-base';
import { AgencyServiceProxy, DropdownListDto, InspectionTestDto, InspectionTestServiceProxy, LookupServiceProxy } from '../../shared/service-proxies/service-proxies';
import { InspectionTestCreateComponent } from './inspection-test-create/inspection-test-create.component';
import { InspectionTestEditComponent } from './inspection-test-edit/inspection-test-edit.component';


class PagedAgencyRequestDto extends PagedRequestDto {
    keyword: string;
    isActive: boolean | null;
}
@Component({
    templateUrl: './inspection-test.component.html',
    animations: [appModuleAnimation()]
})
export class InspectionTestComponent extends PagedListingComponentBase<InspectionTestDto> {
    inspectionTests: InspectionTestDto[] = [];
    keyword = '';
    inspectionTestTypId: number = 0;
    isActive: boolean | null;
    advancedFiltersVisible = false;
    inspectionTestTypes: DropdownListDto[] = [];
    constructor(
        injector: Injector,
        public _lookupServiceProxy: LookupServiceProxy,
        private _inspectionTestServiceProxy: InspectionTestServiceProxy,
        private _modalService: BsModalService
    ) {
        super(injector);
    }

    createAgency(): void {
        this.showCreateOrEditUserDialog();
    }

    editRow(inspectionTest: InspectionTestDto): void {
        this.showCreateOrEditUserDialog(inspectionTest.id);
    }

    loadTypes() {
        this._lookupServiceProxy.inspectionTestTypes().subscribe(res => {
            console.log('Data');
            this.inspectionTestTypes = res;
        });
    }


    clearFilters(): void {
        this.keyword = '';
        this.isActive = undefined;
        this.inspectionTestTypId = undefined;
        this.getDataPage(1);
    }

    protected list(
        request: PagedAgencyRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;
        request.isActive = this.isActive;
       
        this._inspectionTestServiceProxy
            .getAll(
                this.inspectionTestTypId,
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
                this.inspectionTests = result.items;
                this.showPaging(result, pageNumber);
                if (this.inspectionTestTypes.length<1) {
                    this.loadTypes();
                }
              
            });
    }

    protected delete(row: InspectionTestDto): void {
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
        let createOrEditUserDialog: BsModalRef;
        if (!id) {
            createOrEditUserDialog = this._modalService.show(
                InspectionTestCreateComponent,
                {
                    class: 'modal-lg',
                }
            );
        } else {
            createOrEditUserDialog = this._modalService.show(
                InspectionTestEditComponent,
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