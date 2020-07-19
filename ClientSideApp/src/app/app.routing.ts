import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { SalesComponent } from './sales';
import { NotFoundComponent } from './_components';

const appRoutes: Routes =[
    { path: '', component: HomeComponent},
    { path: 'home', component: HomeComponent },
    { path: 'sales', component: SalesComponent },

    { path: '**', component: NotFoundComponent},
];

export const routing = RouterModule.forRoot(appRoutes);