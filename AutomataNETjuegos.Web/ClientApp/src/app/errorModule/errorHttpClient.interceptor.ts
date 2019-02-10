import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpErrorResponse } from "@angular/common/http";
import { Observable } from 'rxjs';
import { map, filter, tap } from 'rxjs/operators';
import { ErrorsService } from "./errors.service";
import { ApiErrors } from "./apiErrors";

@Injectable()
export class ErrorHttpClientInterceptor implements HttpInterceptor {

  constructor(private errorsService: ErrorsService) {

  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const started = Date.now();
    return next.handle(req).pipe(
      tap(event => {
        if (event instanceof HttpResponse) {
          const elapsed = Date.now() - started;
          console.log(`Request for ${req.urlWithParams} took ${elapsed} ms.`);
        }
      }, errorResp => {
        if (errorResp instanceof HttpErrorResponse) {
          if ('message' in errorResp.error && 'name' in errorResp.error) {
            this.errorsService.setError(errorResp.error);
          }
        }
      })
    )
  }
}
