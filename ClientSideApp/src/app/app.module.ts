import { NgModule }      from '@angular/core';
import { AppComponent }   from './app.component';
import { routing }   from './app.routing';

@NgModule({
    imports: [
        routing
    ],
    declarations: [
        AppComponent,
        // Add components here.
    ],
    providers: [
        // Add services here.
    ],
    bootstrap: [ AppComponent ]
})

export class AppModule { }