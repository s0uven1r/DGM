import {

  HttpEvent,

  HttpInterceptor,

  HttpHandler,

  HttpRequest,

  HttpErrorResponse

} from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Router } from '@angular/router';

import { Observable, throwError } from 'rxjs';

import { retry, catchError } from 'rxjs/operators';


@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router) {

  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(request)

      .pipe(

        retry(1),

        catchError((error: HttpErrorResponse) => {
          let errorMessage = '';
          switch (error.status) {
            case 403:
              const url = "/forbidden";
              this.router.navigateByUrl(url);
              return;
            default:
              break;
          }

          if (error.error instanceof ErrorEvent) {

            // client-side error

            errorMessage = `Error: ${error.error}`;

          } else {

            // server-side error

            errorMessage = ` ${error.error}`;

          }

          return throwError(errorMessage);

        })

      )

  }

}