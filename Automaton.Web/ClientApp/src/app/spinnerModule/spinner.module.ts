import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { SpinnerComponent } from './spinner.component';
import { InterceptorService } from './spinnerInterceptor.service';
import { SpinnerService } from './spinner.service';

@NgModule({
  imports: [BrowserModule, FormsModule, HttpClientModule],
  declarations: [SpinnerComponent],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi: true },
  SpinnerService],
  exports: [SpinnerComponent]
})
export class SpinnerModule { }
