import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { PodiumComponent } from './podium.component';

@NgModule({
  declarations: [
    PodiumComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
  ],
  providers: [
  ],
  exports: [
    PodiumComponent
  ]
})

export class PodiumModule { }
