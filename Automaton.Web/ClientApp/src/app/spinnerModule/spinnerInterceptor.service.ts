import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpEvent, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { finalize, delay } from 'rxjs/operators';
import { BsModalService } from 'ngx-bootstrap';
import { SpinnerModalComponent } from './spinnerModal/spinnerModal.component';


@Injectable()
export class InterceptorService implements HttpInterceptor {

  constructor(
    private modalService: BsModalService
  ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let modal = this.modalService.show(SpinnerModalComponent, { backdrop: 'static', animated: false });

    return next.handle(req)
      .pipe(
        delay(10),
      finalize(() => {
        modal.hide();
      }))
  }
}
