import {

    HttpEvent,
   
    HttpInterceptor,
   
    HttpHandler,
   
    HttpRequest,
   
    HttpErrorResponse
   
   } from '@angular/common/http';
   
   import { Observable, throwError } from 'rxjs';
   
   import { retry, catchError } from 'rxjs/operators';
   
   
   
   export class HttpErrorInterceptor implements HttpInterceptor {
   
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
   
      return next.handle(request)
   
        .pipe(
   
          retry(1),
   
          catchError((error: HttpErrorResponse) => {
            let errorMessage = '';
   
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