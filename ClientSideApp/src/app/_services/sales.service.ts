import { UrlConstants } from '../_constants';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Sale, SaleStatistic } from '../_models';
import { DatePipe } from '@angular/common';

@Injectable({ providedIn: 'root'})
export class SalesService {
    url: string;
    statisticUrl: string;

    constructor(
        private http: HttpClient,
        private datepipe: DatePipe,
    ) { 
        this.url = UrlConstants.SALES_URL;
        this.statisticUrl = UrlConstants.SALES_STATISTIC_URL;
    }

    getAll() {
        return this.http.get<Sale[]>(`${this.url}`);
    }

    getById(id:number) {
        return this.http.get<Sale>(`${this.url}/${id}`);
    }

    register(sale: Sale) {
        const myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
        return this.http.post(`${this.url}`, JSON.stringify(sale), { headers: myHeaders, responseType: 'json' });
    }

    update(sale: Sale) {
        const myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
        return this.http.put(`${this.url}/${sale.id}`, JSON.stringify(sale), { headers: myHeaders, responseType: 'json' });
    }

    deleteById(id:number) {
        return this.http.delete<Sale>(`${this.url}/${id}`);
    }

    getStatistic(scale:number, startDate:Date, endDate:Date) {
        let startDateString = this.datepipe.transform(startDate, 'yyyy-MM-dd');
        let endDateString = this.datepipe.transform(endDate, 'yyyy-MM-dd');
        return this.http.get<SaleStatistic>(`${this.statisticUrl}?scale=${scale}&startdate=${startDateString}&enddate=${endDateString}`);
    }
}