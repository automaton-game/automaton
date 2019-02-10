import { ErrorsHandler } from './errors.handler';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { ErrorComponent } from './error.component';
import { ErrorsService } from './errors.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorHttpClientInterceptor } from './errorHttpClient.interceptor';

@NgModule({
  declarations: [
    ErrorComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [
    ErrorsService,
    {
      provide: ErrorHandler,
      useClass: ErrorsHandler,
    },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorHttpClientInterceptor, multi: true }
  ],
  exports: [ErrorComponent]
})
export class ErrorModule { }
