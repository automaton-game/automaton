import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { JuegoComponent } from './juego/juego.component';
import { FilaComponent } from './juego/fila/fila.component';
import { CeldaComponent } from './juego/celda/celda.component';
import { TableroComponent } from './juego/tablero/tablero.component';
import { JuegoManualComponent } from './juegoManual/juegoManual.component';
import { SpinnerModule } from './spinnerModule/spinner.module';
import { InstruccionesComponent } from './instrucciones/instrucciones.component';
import { LoginComponent } from './autenticacion/login/login.component';
import { AutenticacionService } from './autenticacion/autenticacion.service';
import { TokenInterceptor } from './autenticacion/tokenInterceptor';
import { LoginComponentTemplate } from './autenticacion/login/login.template';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    JuegoComponent,
    FilaComponent,
    CeldaComponent,
    TableroComponent,
    JuegoManualComponent,
    InstruccionesComponent,
    LoginComponent,
    LoginComponentTemplate,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'home', component: HomeComponent },
      { path: 'juego', component: JuegoComponent },
      { path: 'juegoManual', component: JuegoManualComponent },
      { path: 'juegoManual/:id', component: JuegoManualComponent },
      { path: 'instrucciones', component: InstruccionesComponent },
      { path: 'login', component: LoginComponent },
    ]),
    SpinnerModule,
    ModalModule.forRoot(),
  ],
  providers: [
    AutenticacionService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    LoginComponentTemplate
  ]
})
export class AppModule { }
