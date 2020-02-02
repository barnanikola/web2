import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AgmCoreModule } from '@agm/core';
import { AgmSnazzyInfoWindowModule } from '@agm/snazzy-info-window';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './interceptors/token.interceptor';

import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { LogInComponent } from './auth/log-in/log-in.component';
import { RegisterComponent } from './auth/register/register.component';
import { LinijeComponent } from './linije/linije.component';
import { RedVoznjeComponent } from './red-voznje/red-voznje.component';
import { CenovnikComponent } from './cenovnik/cenovnik.component';
import { AccountComponent } from './account/account.component';
import { KontrolerComponent } from './kontroler/kontroler.component';
import { VerifikacijaComponent } from './verifikacija/verifikacija.component';



@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LogInComponent,
    RegisterComponent,
    LinijeComponent,
    RedVoznjeComponent,
    CenovnikComponent,
    AccountComponent,
    KontrolerComponent,
    VerifikacijaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyDw23HuyCwqET5LV9E8SsN8y7jhlIRdf9Y'
    }),
    AgmSnazzyInfoWindowModule
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
