import { Injectable, OnDestroy, NgZone } from "@angular/core";
import { HubConnection } from '@aspnet/signalr';
import { Subject } from "rxjs/Subject";

Injectable()
export class SocketClientService implements OnDestroy {

  constructor(
    private hubConnection: HubConnection,
    private zone: NgZone,
  ) {
  }

  read<T>(action: string) {
    let subject = new Subject<T>();

    this.hubConnection.on(action, (response: T) => {
      this.zone.run(() => {
        subject.next(response);

        if (subject.observers.length === 0) {
          this.ngOnDestroy();
        }
      });
    });

    return subject.asObservable();
  }

  ngOnDestroy(): void {
    this.hubConnection.stop();
  }
}
