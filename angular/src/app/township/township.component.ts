import { Component, Injector, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '../../shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '../../shared/paged-listing-component-base';
import { TowinShipServiceProxy, TownShipDto } from '../../shared/service-proxies/service-proxies';
import { TownshipCreateComponent } from './township-create/township-create.component';
import { TownshipEditComponent } from './township-edit/township-edit.component';

class PagedTowinShiptRequestDto extends PagedRequestDto {
    keyword: string;
}

@Component({
    templateUrl: './township.component.html',
    animations: [appModuleAnimation()]
})

export class TownshipComponent extends PagedListingComponentBase<TownShipDto> {
    townShips: TownShipDto[] = [];
    keyword = '';
    departmentAgencyId: number | null;
    advancedFiltersVisible = false;

    constructor(
        injector: Injector,
        private _towinShipServiceProxy: TowinShipServiceProxy,
        private _modalService: BsModalService
    ) {
        super(injector);
    }

    create(): void {
        this.showCreateOrEditUserDialog();
    }

    edit(row: TownShipDto): void {
        this.showCreateOrEditUserDialog(row.id);
    }



    clearFilters(): void {
        this.keyword = '';
        this.departmentAgencyId = undefined;
        this.getDataPage(1);
    }

    protected list(
        request: PagedTowinShiptRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        //if (this.allAgencies.length < 1) {
        //    this.loadAgencyTypes();
        //    this.loadAgencies();
        //}

        //request.keyword = this.keyword;
        //request.currentAgencyId = this.agencyId;

        this._towinShipServiceProxy
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
                this.townShips = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    protected delete(row: TownShipDto): void {
        abp.message.confirm(
            this.l('', row.name),
            undefined,
            (result: boolean) => {
                if (result) {
                    this._towinShipServiceProxy.delete(row.id).subscribe(() => {
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                        this.refresh();
                    });
                }
            }
        );
    }


    private showCreateOrEditUserDialog(id?: number): void {
        let createOrEditUserDialog: BsModalRef;
        if (!id) {
            createOrEditUserDialog = this._modalService.show(
                TownshipCreateComponent,
                {
                    class: 'modal-lg',
                }
            );
        } else {
            createOrEditUserDialog = this._modalService.show(
                TownshipEditComponent,
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
