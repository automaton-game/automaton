import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap';

import { InterceptorService } from './spinnerInterceptor.service';
import { SpinnerModalComponent } from './spinnerModal/spinnerModal.component';

@NgModule({
  imports: [BrowserModule, FormsModule, HttpClientModule, ModalModule],
  declarations: [SpinnerModalComponent],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi: true },
  ],
  entryComponents: [SpinnerModalComponent],
})
export class SpinnerModule { }
