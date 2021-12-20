import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { BlockCopyPasteDirective } from "./blockcopypaste.directive";

@NgModule({
    declarations: [BlockCopyPasteDirective],
    imports: [
      CommonModule
    ],
    exports: [BlockCopyPasteDirective]
  })
  export class BlockCopyPasteModule { }
