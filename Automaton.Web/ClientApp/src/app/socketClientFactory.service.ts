import { Injectable } from "@angular/core";
import { HubConnectionBuilder } from '@aspnet/signalr';
import { SocketClientService } from "./socketClient.service";

Injectable()
export class SocketClientServiceFactory {

  connect(url: string) {
    let hubConnection = new HubConnectionBuilder()
      .withUrl(url)
      .build();

    hubConnection.start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    return new SocketClientService(hubConnection);
  }
}
