import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap';
import { Ng2Webstorage } from 'ngx-webstorage'; //2.0.1

import { LoginComponent } from './login/login.component';
import { LoginComponentTemplate } from './login/login.template';
import { AutenticacionService } from './autenticacion.service';
import { TokenInterceptor } from './tokenInterceptor';


@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ModalModule.forRoot(),
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent },
    ]),

    // The forRoot method allows to configure the prefix, the separator and the caseSensitive option used by the library
    // Default values:
    // prefix: "ng2-webstorage"
    // separator: "|"
    // caseSensitive: false
    Ng2Webstorage.forRoot() 
  ],
  declarations: [
    LoginComponent,
    LoginComponentTemplate,
  ],
  providers: [
    AutenticacionService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  entryComponents: [LoginComponentTemplate],
  exports: [
    LoginComponent
  ]
})
export class AutenticacionModule { }
