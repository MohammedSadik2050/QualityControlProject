import { Component, Injector, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '../../shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '../../shared/paged-listing-component-base';
import { AgencyServiceProxy, ProjectDto, ProjectServiceProxy } from '../../shared/service-proxies/service-proxies';
import { CreateProjectComponent } from './create-project/create-project.component';

class PagedProjectRequestDto extends PagedRequestDto {
    keyword: string;
    isActive: boolean | null;
}

@Component({
    templateUrl: './project.component.html',
    animations: [appModuleAnimation()]
})
export class ProjectComponent extends PagedListingComponentBase<ProjectDto> {
    projects: ProjectDto[] = [];
    keyword = '';
    isActive: boolean | null;
    advancedFiltersVisible = false;

    constructor(
        injector: Injector,
        private _projectServiceProxy: ProjectServiceProxy,
        private _modalService: BsModalService
    ) {
        super(injector);
    }

    createProject(): void {
        this.showCreateOrEditUserDialog();
    }

    editProject(project: ProjectDto): void {
        this.showCreateOrEditUserDialog(project.id);
    }



    clearFilters(): void {
        this.keyword = '';
        this.isActive = undefined;
        this.getDataPage(1);
    }

    protected list(
        request: PagedProjectRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;
        request.isActive = this.isActive;

        this._projectServiceProxy
            .getAll(
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
                this.projects = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    protected delete(project: ProjectDto): void {
        abp.message.confirm(
            this.l('', project.name),
            undefined,
            (result: boolean) => {
                if (result) {
                    //this._projectServiceProxy.delete(project.id).subscribe(() => {
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
                CreateProjectComponent,
                {
                    class: 'modal-lg',
                }
            );
        } else {
            //createOrEditUserDialog = this._modalService.show(
            //    AgencyEditComponent,
            //    {
            //        class: 'modal-lg',
            //        initialState: {
            //            id: id,
            //        },
            //    }
            //);
        }

        createOrEditUserDialog.content.onSave.subscribe(() => {
            this.refresh();
        });
    }
}
