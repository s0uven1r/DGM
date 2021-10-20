import { Component, OnInit } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { LogoService } from '../service/logo.service';

@Component({
  selector: 'app-logo',
  templateUrl: './logo.component.html',
  styleUrls: ['./logo.component.css']
})
export class LogoComponent implements OnInit {
  fileToUpload: File | null = null;
  constructor( private logoService: LogoService) { 
    
  }

  ngOnInit(): void {

  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }

  uploadLogo() {
    var fileUploaded = this.fileToUpload;
    if(fileUploaded !== null){
      Swal.fire({
        title: "Upload Logo",
        text: "User Action",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Ok",
        cancelButtonText: "Cancel",
      }).then((result) => {
        if (result.value) {
          this.logoService
            .uploadLogo(fileUploaded)
            .pipe(
              catchError((err) => {
                return throwError(err);
              })
            )
            .subscribe(
              () => {
                Swal.fire("Uploaded!", "User Action", "success");
              },
              () => console.log("HTTP request completed.")
            );
        }
      });
    }
}
}
