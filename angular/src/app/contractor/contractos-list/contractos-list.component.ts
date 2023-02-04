import { Component, Injector, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '../../../shared/paged-listing-component-base';
import { QCUserDto, QCUserServiceProxy, UserServiceProxy } from '../../../shared/service-proxies/service-proxies'; 
import { ContractorUserComponent } from '../contractor-create/contractor-user/contractor-user.component';
import { ContractorEditComponent } from '../contractor-edit/contractor-edit.component';

//@Component({
//    selector: 'app-contractors-list',
//    templateUrl: './contractos-list.component.html',
//    styleUrls: ['./contractos-list.component.css']
//})
class PagedUsersRequestDto extends PagedRequestDto {
    keyword: string;
    isActive: boolean | null;
}
@Component({
    templateUrl: './contractos-list.component.html',
    animations: [appModuleAnimation()]
})
export class ContractorsListComponent extends PagedListingComponentBase<QCUserDto> {
    users: QCUserDto[] = [];
    keyword = '';
    isActive: boolean | null;
    advancedFiltersVisible = false;

    constructor(
        injector: Injector,
        private _qcUserServiceProxy: QCUserServiceProxy,
        private _modalService: BsModalService
    ) {
        super(injector);
    }

    createUser(): void {
        this.showCreateOrEditUserDialog();
    }

    editUser(user: QCUserDto): void {
        this.showCreateOrEditUserDialog(user.userId);
    }

    public resetPassword(user: QCUserDto): void {
        //  this.showResetPasswordUserDialog(user.userId);
    }

    clearFilters(): void {
        this.keyword = '';
        this.isActive = undefined;
        this.getDataPage(1);
    }

    protected list(
        request: PagedUsersRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;
        request.isActive = this.isActive;

        this._qcUserServiceProxy
            .getAll(
                this.appSession.userId,
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
                this.users = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    protected delete(user: QCUserDto): void {
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
                ContractorUserComponent,
                {
                    class: 'modal-lg',
                }
            );
        } else {
            createOrEditUserDialog = this._modalService.show(
                ContractorEditComponent,
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
