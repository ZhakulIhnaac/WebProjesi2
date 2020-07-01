import { Component, Injector, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { DefinitionServiceProxy, ExpertDto, SearchParamAnd, CountryDto, ProjectBudgetingServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '../../shared/app-component-base';
import { CreateEditExpertDialogComponent } from './create-edit-expert/create-edit-expert-dialog.component';
import { ExpertDetailDialogComponent } from './detail-expert/detail-expert.component';
import { ESearchParamOperator } from '../../shared/AppEnums';

@Component({
    templateUrl: './experts.component.html',
    animations: [appModuleAnimation()],
    styles: [
        `
          mat-form-field {
            padding: 10px;
          }
        `
    ]
})
export class ExpertsComponent extends AppComponentBase implements OnInit {
    expertList: ExpertDto[] = [];
    countryList: CountryDto[] = [];
    countryListFiltered: CountryDto[] = [];
    searchKeyExpertName: string = "";
    searchKeyExpertCountry: number;
    countrySelectBoxFilter: string;

    constructor(
        injector: Injector,
        private _projectBudgetingServiceProxy: ProjectBudgetingServiceProxy,
        private _definitionService: DefinitionServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
    }

    ngOnInit() {
        this.getExpertsForTable()
        this.getCountries()
    }

    searchExperts() {
        this.page = 1;
        this.maxResult = 10;
        this.getExpertsForTable();
    }

    resetSearch() {
        this.page = 1;
        this.maxResult = 10;
        this.searchKeyExpertName = undefined;
        this.searchKeyExpertCountry = undefined;
        this.getExpertsForTable();
    }

    filterCountry() {
        this.countryListFiltered = this.countryList.filter(x => x.countryName.toLowerCase().includes(this.countrySelectBoxFilter.toLowerCase()));
    }

    getExpertsForTable() {
        this.addSearchParamAnd("nationalityId", ESearchParamOperator.Equal, this.searchKeyExpertCountry);
        this.addSearchParamOr(
            [
                new SearchParamAnd({ column: "name", operatorType: ESearchParamOperator.Include, value: this.searchKeyExpertName }),
                new SearchParamAnd({ column: "surname", operatorType: ESearchParamOperator.Include, value: this.searchKeyExpertName })
            ]);
        this._projectBudgetingServiceProxy.getExpertForTable(this.maxResult, this.page, this.searchParamList).subscribe(result => {
            this.expertList = result.itemList;
            this.totalItems = result.totalItemCount;
            this.searchParamList.searchParamAndList.length = 0;
            this.searchParamList.searchParamOrList.length = 0;
            this.pageList = [...Array(Math.ceil(this.totalItems / this.maxResult)).keys()].map(i => i + 1);
        })
    }

    getCountries() {
        this._definitionService.getCountryForSelectBox().subscribe(result => {
            this.countryList = result;
            this.countryListFiltered = result;
        });
    }

    createEditDialog(expertId?: number): void {
        let createEditDialog;

        createEditDialog = this._dialog.open(CreateEditExpertDialogComponent, {
            data: { expertId },
            width: "150vh",
            maxHeight: "90vh"
        });

        createEditDialog.afterClosed().subscribe(result => {
            if (result) {
                this.ngOnInit();
            }
        });
    }

    createDetailDialog(expertId?: number): void {
        let detailDialog;

        detailDialog = this._dialog.open(ExpertDetailDialogComponent, {
            data: { expertId },
            width: "150vh",
            maxHeight: "90vh"
        });

        detailDialog.afterClosed().subscribe();
    }

    delete(expert: ExpertDto): void {
        abp.message.confirm(
            this.l('ExpertDeleteWarningMessage', expert.name, expert.surname), undefined,
            (result: boolean) => {
                if (result) {
                    this._projectBudgetingServiceProxy.deleteExpert(expert.id).subscribe(() => {
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                        this.ngOnInit();
                    });
                }
            }
        );
    }

}
