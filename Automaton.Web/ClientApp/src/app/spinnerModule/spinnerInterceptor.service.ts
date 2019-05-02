import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpEvent, HttpRequest } from '@angular/common/http';
import { SpinnerService } from './spinner.service';
import { Observable } from 'rxjs/Observable';
import { finalize, delay } from 'rxjs/operators';


@Injectable()
export class InterceptorService implements HttpInterceptor {

  constructor(private spinner: SpinnerService) {

  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.spinner.visibility.emit(true);
    return next.handle(req)
      .pipe(
        delay(10),
        finalize(() => this.spinner.visibility.emit(false))
      )
  }
}
