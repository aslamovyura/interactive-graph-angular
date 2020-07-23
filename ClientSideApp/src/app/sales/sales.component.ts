import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { Sale } from '../_models';
import { SalesService, AlertService } from '../_services';
import { AppConstants } from '../_constants/app-constants';
import { DatePipe } from '@angular/common';

@Component({
    selector: 'sales-app',
    templateUrl: './sales.component.html'
})
export class SalesComponent implements OnInit { 

    @ViewChild('readOnlyTemplate', {static: false}) readOnlyTemplate: TemplateRef<any>;
    @ViewChild('editTemplate', {static: false}) editTemplate: TemplateRef<any>;

    sales: Sale[];
    editedSale: Sale;
    isNewSale: boolean;
    isLoading: boolean;
    imgSrc: string;

    // Pagination controls
    page: number = 1;

    constructor(
        private salesService: SalesService,
        private alertService: AlertService,
        private datepipe: DatePipe,
    ) {
        this.sales = new Array<Sale>();
        this.isLoading = false;
        this.imgSrc = AppConstants.LOADING_GIF;
    }

    // Actions on initialization.
    ngOnInit(): void {
        this.loadSales();
    }

    // Load sales from server.
    loadSales(): void {

        this.isLoading = true;

        this.salesService.getAll()
        .subscribe(
            (data: Sale[]) => {
                this.sales = data;
                
                this.isLoading = false;
            },
            error => {
                this.sales = null;
                console.error(error);
                this.alertService.error(AppConstants.CONNECTION_ISSUES);
                this.isLoading = false;
            }
        );

    }

    // Register new sale.
    addSale(): void {

        const insert = (arr, index, newItem) => [
            ...arr.slice(0, index),
            newItem,
            ...arr.slice(index)
          ]

        this.editedSale = new Sale();
        this.sales = insert(this.sales, 0, this.editedSale); // add new sale to the top of the array.
        this.isNewSale = true;
    }

    // Edit existing sale.
    editSale(sale: Sale) {
        this.editedSale = new Sale();
        this.editedSale.id = sale.id;
        this.editedSale.date = sale.date;
        this.editedSale.amount = sale.amount;
    }

    // Delete sale.
    deleteSale(id: number) {
        this.isLoading = true;
        this.salesService.deleteById(id)
            .subscribe(
                data => {
                    this.loadSales(),
                    this.alertService.success(AppConstants.REMOVE_SALE_SUCCESS);
                    this.isLoading = false;
                },
                error => {
                    console.error(error);
                    this.alertService.error(AppConstants.REMOVE_SALE_FAIL);
                    this.isLoading = false;
                }
            )
    }

    // Load appropriate template.
    loadTemplate(sale: Sale) {
        if (this.editedSale && this.editedSale.id === sale.id) {
            return this.editTemplate;
        } 
        else {
            return this.readOnlyTemplate;
        }
    }

    // Save new sale.
    save() {

        this.isLoading = true;
        console.log('new sale:', this.editedSale);

        if (this.isNewSale) {
            // Add new sale.
            this.salesService.register(this.editedSale)
                .subscribe(
                    data => {
                        this.loadSales();
                        this.isNewSale = false;
                        this.editedSale = null;
                        this.alertService.success(AppConstants.REGISTER_SALE_SUCCESS);
                        this.isLoading = false;
                    },
                    error => {
                        this.alertService.error(AppConstants.REGISTER_SALE_FAIL);
                        this.cancel();
                        this.isLoading = false;
                    });
        } else {
            // Update existing sale.
            this.salesService.update(this.editedSale)
                .subscribe(
                    data => {
                        this.loadSales();
                        this.alertService.success(AppConstants.UPDATE_SALE_SUCCESS);
                        this.isLoading = false;
                    },
                    error => {
                        this.alertService.error(AppConstants.UPDATE_SALE_FAIL);
                        this.cancel();
                        this.isLoading = false;
                    });
            this.editedSale = null;
        }
    }

    // Cancel sale editing/registering.
    cancel() {
        // If cancel while registering new sale, remove last sale. 
        if (this.isNewSale) {
            this.sales.splice(0, 1); // remove new sale from the top of the array.
            this.isNewSale = false;
        }
        this.editedSale = null;
    }

    convertDateToString(date: Date) : String {
        return this.datepipe.transform(date, 'yyyy/MM/dd').toString();
    }
}