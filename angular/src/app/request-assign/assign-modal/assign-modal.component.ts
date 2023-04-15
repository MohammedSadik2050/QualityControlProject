import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observer } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '../../../shared/paged-listing-component-base';
import { ObserverDto, ObserverServiceProxy, RequestServiceProxy, RequestViewDto, TowinShipServiceProxy, TownShipDto } from '../../../shared/service-proxies/service-proxies';
class PagedUsersRequestDto extends PagedRequestDto {
    keyword: string;
    townShip: number | null;
}
@Component({
  selector: 'app-assign-modal',
  templateUrl: './assign-modal.component.html',
  styleUrls: ['./assign-modal.component.css']
})
export class AssignModalComponent extends PagedListingComponentBase<ObserverDto> {
    users: ObserverDto[] = [];
    keyword = '';
    townShip: number = 0;
    townShips: TownShipDto[] = [];
    isActive: boolean | null;
    advancedFiltersVisible = false;
    id: number;
    currentObserver: number;
    @Output() onSave = new EventEmitter<any>();
    saving = false;
    constructor(
        injector: Injector,
        private _observerServiceProxy: ObserverServiceProxy,
        private _towinShipServiceProxy: TowinShipServiceProxy,
        private _requestServiceProxy: RequestServiceProxy,
        public bsModalRef: BsModalRef
    ) {
        super(injector);
    }

   

    editUser(observer: ObserverDto): void {
        this.saving = true;
        this._requestServiceProxy.assignRequest(this.id, observer.id).subscribe(
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

    


}
