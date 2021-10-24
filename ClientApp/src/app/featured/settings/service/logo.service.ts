import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiGateway } from "src/app/infrastructure/datum/apigateway/api-gateway";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class LogoService {
  private baseUrl = environment.apiIdentityUrl;
  private settingsUrl = ApiGateway.identity.settings.base;

  private uploadLogoUrl =
    this.settingsUrl + ApiGateway.identity.settings.uploadLogo;
  private getLogoUrl =
    this.settingsUrl + ApiGateway.identity.settings.getLogo;

  constructor(private http: HttpClient) {}

  uploadLogo(fileToUpload: File): Observable<any>  {
    const endpoint = `${this.baseUrl + this.uploadLogoUrl}`;
    const formData: FormData = new FormData();
    formData.append('Logo', fileToUpload, fileToUpload.name);
    return this.http.post<any>(endpoint, formData);
    }

    getLogo(): Observable<Blob>  {
      const endpoint = `${this.baseUrl + this.getLogoUrl}`;
      return this.http.get(endpoint, { responseType: 'blob' });
    }
}
