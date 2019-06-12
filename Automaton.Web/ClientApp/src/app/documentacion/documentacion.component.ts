import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IClassInfo } from './Models/IClassInfo';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-documentacion',
  templateUrl: './documentacion.component.html',
})
export class DocumentacionComponent implements OnInit, OnDestroy {

  private sub: Subscription;

  constructor(private http: HttpClient) {
    
  }

  public assembly: Array<IClassInfo>;

  ngOnInit(): void {
    this.sub = this.http.get<{ lista: Array<IClassInfo> }>("api/Documentacion/Get").subscribe(s => this.assembly = s.lista);
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
