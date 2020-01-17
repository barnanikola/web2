import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LogInComponent } from './auth/log-in/log-in.component';
import { RegisterComponent } from './auth/register/register.component';
import { LinijeComponent } from './linije/linije.component';
import { RedVoznjeComponent } from './red-voznje/red-voznje.component';
import { CenovnikComponent } from './cenovnik/cenovnik.component';
import { AccountComponent } from './account/account.component';
import { KontrolerComponent } from './kontroler/kontroler.component';
import { VerifikacijaComponent } from './verifikacija/verifikacija.component';

const appRoutes: Routes = [
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: 'login', component: LogInComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'linije', component: LinijeComponent},
    { path: 'redvoznje', component: RedVoznjeComponent},
    { path: 'cenovnik', component: CenovnikComponent},
    { path: 'account', component: AccountComponent},
    { path: 'kontrola', component: KontrolerComponent},
    { path: 'verifikacija', component: VerifikacijaComponent}
];

@NgModule({
    imports: [RouterModule.forRoot(appRoutes)],
    exports: [RouterModule]
})
export class AppRoutingModule {}
