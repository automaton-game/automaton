import { ErrorsHandler } from './ErrorsHandler';
import { NgModule, ErrorHandler } from '@angular/core';

@NgModule({
  declarations: [
  ],
  imports: [
  ],
  providers: [
    {
      provide: ErrorHandler,
      useClass: ErrorsHandler,
    }
  ]
})
export class ErrorModule { }
