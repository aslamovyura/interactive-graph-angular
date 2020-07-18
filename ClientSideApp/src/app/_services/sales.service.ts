import { UrlConstants } from '../_constants';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Sale } from '../_models';

@Injectable({ providedIn: 'root'})
export class SalesService {
    url: string;

    constructor(private http: HttpClient) { 
        this.url = UrlConstants.SALES_URL;
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
}