import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs/Subscription';
import { INameSpaceInfo } from './Models/INameSpaceInfo';
import { IClassInfo } from './Models/IClassInfo';

@Component({
  selector: 'app-documentacion',
  templateUrl: './documentacion.component.html',
})
export class DocumentacionComponent implements OnInit, OnDestroy {

  private sub: Subscription;

  constructor(private http: HttpClient) {
    
  }

  public assembly: Array<INameSpaceInfo>;

  ngOnInit(): void {
    this.sub = this.http.get<{ lista: Array<INameSpaceInfo> }>("api/Documentacion/Get").subscribe(s => this.assembly = s.lista);
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  claseConContenido(classInfo: IClassInfo) {
    return classInfo.methods.length > 0 || classInfo.properties.length > 0;
  }
}
