import { NgNepaliDateDirective } from './ng-nepali-date.directive';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [NgNepaliDateDirective],
  exports: [NgNepaliDateDirective]
})
export class NgNepaliDateDirectiveModule { }
