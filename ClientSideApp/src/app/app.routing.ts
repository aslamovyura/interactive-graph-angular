import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { NotFoundComponent } from './_components';

const appRoutes: Routes =[
    { path: '', component: HomeComponent},
    { path: 'home', component: HomeComponent },
    { path: '**', component: NotFoundComponent},
];

export const routing = RouterModule.forRoot(appRoutes);