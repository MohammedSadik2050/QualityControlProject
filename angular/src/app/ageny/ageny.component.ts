import { Component, Injector, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '../../shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '../../shared/paged-listing-component-base';
import { AgencyDto, AgencyDtoPagedResultDto, AgencyServiceProxy, UserServiceProxy } from '../../shared/service-proxies/service-proxies';
import { AgencyCreateComponent } from './agency-create/agency-create.component';
import { AgencyEditComponent } from './agency-edit/agency-edit.component';

class PagedAgencyRequestDto extends PagedRequestDto {
    keyword: string;
    isActive: boolean | null;
}
@Component({
    templateUrl: './ageny.component.html',
    animations: [appModuleAnimation()]
})
export class AgenyComponent extends PagedListingComponentBase<AgencyDto> {
    agencies: AgencyDto[] = [];
    keyword = '';
    isActive: boolean | null;
    advancedFiltersVisible = false;

    constructor(
        injector: Injector,
        private _agencyServiceProxy: AgencyServiceProxy,
        private _modalService: BsModalService
    ) {
        super(injector);
    }

    createAgency(): void {
        this.showCreateOrEditUserDialog();
    }

    editAgency(user: AgencyDto): void {
        this.showCreateOrEditUserDialog(user.id);
    }

    

    clearFilters(): void {
        this.keyword = '';
        this.isActive = undefined;
        this.getDataPage(1);
    }

    protected list(
        request: PagedAgencyRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;
        request.isActive = this.isActive;

        this._agencyServiceProxy
            .getAll(
                this.appSession.userId,
                this.keyword,
               '' ,
                request.skipCount,
                request.maxResultCount
            )
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: PagedResultDto) => {
                this.agencies = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    protected delete(agency: AgencyDto): void {
        abp.message.confirm(
            this.l('', agency.name),
            undefined,
            (result: boolean) => {
                if (result) {
                    this._agencyServiceProxy.delete(agency.id).subscribe(() => {
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                        this.refresh();
                    });
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
                AgencyCreateComponent,
                {
                    class: 'modal-lg',
                }
            );
        } else {
            createOrEditUserDialog = this._modalService.show(
                AgencyEditComponent,
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
