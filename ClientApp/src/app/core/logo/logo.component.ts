import { Component, OnInit } from '@angular/core';
import { LogoService } from 'src/app/featured/settings/service/logo.service';
import { Subscription } from "rxjs";

@Component({
  selector: 'app-logo',
  templateUrl: './logo.component.html',
  styleUrls: ['./logo.component.css']
})
export class LogoComponent implements OnInit {
  isImageLoading = false;
  imageToShow: any;
  subscription: Subscription = new Subscription();
  constructor(public logoService: LogoService,) { }

  ngOnInit(): void {
    this.getImageFromService();
  }

  getImageFromService() {
    this.isImageLoading = true;
    this.subscription.add(
      this.logoService.getLogo().subscribe(
        (data) => {
          this.createImageFromBlob(data);
          this.isImageLoading = false;
        },
        (error) => {
          this.isImageLoading = false;
          console.log(error);
        }
      )
    );
  }

  createImageFromBlob(image: Blob) {
    let reader = new FileReader();
    reader.addEventListener(
      "load",
      () => {
        this.imageToShow = reader.result;
      },
      false
    );

    if (image) {
      reader.readAsDataURL(image);
    }
  }
}
