import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiGateway } from "src/app/infrastructure/datum/apigateway/api-gateway";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class DescritptiveImageService {
  private baseUrl = environment.apiIdentityUrl;
  private settingsUrl = ApiGateway.identity.settings.base;

  private uploadDescriptiveImageUrl =
    this.settingsUrl + ApiGateway.identity.settings.uploadDescriptiveImage;

  constructor(private http: HttpClient) {}

  uploadImages(filesToUpload: FileList): Observable<any> {
    const endpoint = `${this.baseUrl + this.uploadDescriptiveImageUrl}`;
    const formData: FormData = new FormData();
    for (var i = 0; i < filesToUpload.length; i++) {
      formData.append("Images", filesToUpload[i]);
    }
    debugger;
    return this.http.post<any>(endpoint, formData);
  }
}
