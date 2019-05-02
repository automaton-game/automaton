import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { SpinnerComponent } from './spinner.component';
import { InterceptorService } from './spinnerInterceptor.service';
import { SpinnerService } from './spinner.service';

@NgModule({
  imports: [BrowserModule, FormsModule, HttpClientModule],
  declarations: [SpinnerComponent],
  bootstrap: [],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi: true },
    , SpinnerService],
  exports: [SpinnerComponent]
})
export class SpinnerModule { }
