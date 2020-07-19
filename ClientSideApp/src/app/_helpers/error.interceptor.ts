import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlertService } from '../_services/alert.service';
import { AppConstants } from '../_constants/app-constants';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private alertService: AlertService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {

            console.log('Intercept error =>', err);

            switch(err.status)
            {
                case 400: // Bad Request
                    let errorMessage = typeof(err.error)=='string' ? err.error : '';
                    this.alertService.error(AppConstants.BAD_REQUEST + '\n' + errorMessage);
                    break;

                case 401: // Unauthorized
                    this.alertService.error(AppConstants.UNAUTHORIZED);
                    break;

                case 404: // Not Found
                    this.alertService.error(AppConstants.NOT_FOUND);
                    break;
                
                default: // Unknown Error
                    this.alertService.error(AppConstants.CONNECTION_ISSUES);
            }

            const error = err.error.message || err.statusText;
            return throwError(error);
        }))
    }
}