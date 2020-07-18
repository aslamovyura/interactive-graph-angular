import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { FormsModule, ReactiveFormsModule }   from '@angular/forms';
import { HttpClientModule }   from '@angular/common/http';
import { ChartsModule } from 'ng2-charts';
import { routing }   from './app.routing';

import { AppComponent }   from './app.component';
import { AlertComponent } from './_components/alert.component';
import { NavComponent, FooterComponent, NotFoundComponent } from './_components';
import { HomeComponent } from './home/';
import { SalesComponent } from './sales/sales.component';

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
        // Add services here.
    ],
    bootstrap: [ AppComponent ]
})

export class AppModule { }