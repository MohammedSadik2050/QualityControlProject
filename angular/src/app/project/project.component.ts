import { Component, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '../../shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '../../shared/paged-listing-component-base';
import { AgencyDto, AgencyServiceProxy, AgencyTypeDto, DepartmentDto, DepartmentServiceProxy, DropdownListDto, LookupServiceProxy, ProjectDto, ProjectServiceProxy } from '../../shared/service-proxies/service-proxies';
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
    agencyTypes: AgencyTypeDto[] = [];
    agencies: AgencyDto[] = [];
    allAgencies: AgencyDto[] = [];
    projectAgencyTypeId = 0;
    projectAgencyId = 0;
    ProjectStatusId = -2;
    projectDepartmentId = 0;
    departments: DepartmentDto[] = [];
    allDepartments: DepartmentDto[] = [];
    allProjectStatus: DropdownListDto[] = [];
    constructor(
        private _departmentServiceProxy: DepartmentServiceProxy,
        private _lookupServiceProxy: LookupServiceProxy,
        private _agencyServiceProxy: AgencyServiceProxy,
        injector: Injector,
        private _projectServiceProxy: ProjectServiceProxy,
        private _modalService: BsModalService,
        private router: Router
    ) {
        super(injector);
    }

    createProject(): void {
        this.showCreateOrEditUserDialog();
    }

    editProject(project: ProjectDto): void {
        /* this.showCreateOrEditUserDialog(project.id);*/
        this.router.navigateByUrl('/app/projects/edit/' + project.id);
    }



    clearFilters(): void {
        this.keyword = '';
        this.isActive = undefined;
        this.projectAgencyId = 0;
        this.projectAgencyTypeId = 0;
        this.projectDepartmentId = 0;
        this.ProjectStatusId = -2;
        this.getDataPage(1);
    }

    protected list(
        request: PagedProjectRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;
        request.isActive = this.isActive;
        if (this.allAgencies.length<1) {
            this.loadAgencyTypes();
            this.loadAgencies();
            this.loadAllDepartments();
            this.loadAllStatus();
        }
      
        this._projectServiceProxy
            .getAll(
                this.keyword, this.ProjectStatusId,this.projectAgencyTypeId, this.projectAgencyId, this.projectDepartmentId,
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
    loadAllDepartments() {
        this._departmentServiceProxy.getAllDepartmentDropDown().subscribe(res => {
            this.allDepartments = res;
        });
    }

    loadAllStatus() {
        this._lookupServiceProxy.projectStatus().subscribe(res => {
            this.allProjectStatus = res;
        });
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
        this.projectAgencyId = 0;
        this.onAgencyChange(0);
        this.agencies = this.allAgencies.filter(s => s.agencyTypeId == event);
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
    onAgencyChange(event) {
        console.log(event);
        this.projectDepartmentId = 0;
        this.departments = this.allDepartments.filter(s => s.agencyId == event);
    }
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
