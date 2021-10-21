import { Component, ElementRef, OnInit } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { DescritptiveImageService } from '../service/descriptive-image.service';
import { ViewChild } from '@angular/core';

@Component({
  selector: 'app-descriptive-image',
  templateUrl: './descriptive-image.component.html',
  styleUrls: ['./descriptive-image.component.css']
})

export class DescriptiveImageComponent implements OnInit {
  fileToUpload: FileList | null = null;
  constructor( private service: DescritptiveImageService) { 
    
  }
  @ViewChild('inputFile') myInputVariable: ElementRef;
  ngOnInit(): void {

  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files;
  }

  uploadImage() {
    var fileUploaded = this.fileToUpload;
    if(fileUploaded !== null){
      Swal.fire({
        title: "Upload Descriptive Images",
        text: "User Action",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Ok",
        cancelButtonText: "Cancel",
      }).then((result) => {
        if (result.value) {
          this.service
            .uploadImages(fileUploaded)
            .pipe(
              catchError((err) => {
                return throwError(err);
              })
            )
            .subscribe(
              () => {
                this.reset();
                Swal.fire("Uploaded!", "User Action", "success");
              },
              () => console.log("HTTP request completed.")
            );
        }
      });
    }
  }
  reset() {
    this.myInputVariable.nativeElement.value = '';
  }
}
