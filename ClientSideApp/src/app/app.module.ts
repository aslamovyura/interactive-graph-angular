import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule }   from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS }   from '@angular/common/http';
import { DatePipe } from '@angular/common';

import { ChartsModule } from 'ng2-charts';
import { routing }   from './app.routing';

import { AppComponent }   from './app.component';
import { NavComponent, FooterComponent, NotFoundComponent, AlertComponent } from './_components';
import { HomeComponent } from './home/';
import { SalesComponent } from './sales/sales.component';

import { ErrorInterceptor } from './_helpers';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        ChartsModule,
        HttpClientModule,
        routing
    ],
    declarations: [
        AppComponent,
        AlertComponent,
        NavComponent,
        FooterComponent,
        HomeComponent,
        SalesComponent,
        NotFoundComponent
    ],
    providers: [
        {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true}, // Catch HTTP errors.
        [DatePipe]
    ],
    bootstrap: [ AppComponent ]
})

export class AppModule { }