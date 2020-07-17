import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent }   from './app.component';
import { routing }   from './app.routing';
import { AlertComponent } from './_components/alert.component';
import { NavComponent, FooterComponent, NotFoundComponent } from './_components';
import { HomeComponent } from './home/';

@NgModule({
    imports: [
        BrowserModule,
        routing
    ],
    declarations: [
        AppComponent,
        AlertComponent,
        NavComponent,
        FooterComponent,
        HomeComponent,
        NotFoundComponent
    ],
    providers: [
        // Add services here.
    ],
    bootstrap: [ AppComponent ]
})

export class AppModule { }