import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { JuegoComponent } from './juego/juego.component';
import { FilaComponent } from './juego/fila/fila.component';
import { CeldaComponent } from './juego/celda/celda.component';
import { TableroComponent } from './juego/tablero/tablero.component';
import { JuegoManualComponent } from './juegoManual/juegoManual.component';
import { SpinnerModule } from './spinnerModule/spinner.module';
import { InstruccionesComponent } from './instrucciones/instrucciones.component';
import { AutenticacionModule } from './autenticacion/autenticacion.module';
import { SocketClientServiceFactory } from './socketClientFactory.service';
import { DocumentacionComponent } from './documentacion/documentacion.component';
import { ColorService } from './color.service';
import { PodiumModule } from './podium/podium.module';
import { VictoriasComponent } from './juego/victorias/victorias.component';
import { TorneoComponent } from './torneo/torneo.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    JuegoComponent,
    FilaComponent,
    CeldaComponent,
    TableroComponent,
    JuegoManualComponent,
    InstruccionesComponent,
    DocumentacionComponent,
    VictoriasComponent,
    TorneoComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'home', component: HomeComponent },
      { path: 'juego', component: JuegoComponent },
      { path: 'juego/victorias', component: VictoriasComponent },
      { path: 'juegoManual', component: JuegoManualComponent },
      { path: 'juegoManual/:id', component: JuegoManualComponent },
      { path: 'instrucciones', component: InstruccionesComponent },
      { path: 'documentacion', component: DocumentacionComponent },
      { path: 'torneo', component: TorneoComponent },
    ]),
    SpinnerModule,
    AutenticacionModule,
    PodiumModule,
  ],
  providers: [
    SocketClientServiceFactory,
    ColorService,
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    
  ]
})
export class AppModule { }
