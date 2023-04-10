import { Component, Injector, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '../../shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '../../shared/paged-listing-component-base';
import { ObserverDto, ObserverServiceProxy, TowinShipServiceProxy, TownShipDto } from '../../shared/service-proxies/service-proxies';
import { ObserverCreateComponent } from './observer-create/observer-create.component';
import { ObserverEditComponent } from './observer-edit/observer-edit.component';

class PagedUsersRequestDto extends PagedRequestDto {
    keyword: string;
    townShip: number | null;
}

@Component({
    templateUrl: './observer.component.html',
    animations: [appModuleAnimation()]
})
export class ObserverComponent extends PagedListingComponentBase<ObserverDto> {
    users: ObserverDto[] = [];
    keyword = '';
    townShip :number = 0;
    townShips: TownShipDto[] = [];
    isActive: boolean | null;
    advancedFiltersVisible = false;

    constructor(
        injector: Injector,
        private _observerServiceProxy: ObserverServiceProxy,
        private _towinShipServiceProxy: TowinShipServiceProxy,
        private _modalService: BsModalService
    ) {
        super(injector);
    }

    createUser(): void {
        this.showCreateOrEditUserDialog();
    }

    editUser(user: ObserverDto): void {
        this.showCreateOrEditUserDialog(user.userId);
    }

    public resetPassword(user: ObserverDto): void {
        //  this.showResetPasswordUserDialog(user.userId);
    }

    clearFilters(): void {
        this.keyword = '';
        this.isActive = undefined;
        this.townShip = 0;
        this.getDataPage(1);
    }
   
    protected list(

        request: PagedUsersRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;
        request.townShip = this.townShip;

        this._observerServiceProxy
            .getAll(
                this.townShip,
                '',
                '',
                request.skipCount,
                request.maxResultCount
            )
            .pipe(
                finalize(() => {
                    finishedCallback();
                    if (this.townShips.length < 1) {
                        this.loadTownships();
                    }
                  
                })
            )
            .subscribe((result: PagedResultDto) => {
                this.users = result.items;
                this.showPaging(result, pageNumber);
            });
    }
    loadTownships() {
        this._towinShipServiceProxy.getAllAgenciesList().subscribe(res => {
            this.townShips = res;
        })
    }
    protected delete(user: ObserverDto): void {
        abp.message.confirm(
            this.l('UserDeleteWarningMessage', user.name),
            undefined,
            (result: boolean) => {
                if (result) {
                    //this._contractorServiceProxy.delete(user.id).subscribe(() => {
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
                ObserverCreateComponent,
                {
                    class: 'modal-lg',
                }
            );
        } else {
            createOrEditUserDialog = this._modalService.show(
                ObserverEditComponent,
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
