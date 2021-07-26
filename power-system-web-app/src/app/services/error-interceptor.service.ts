import { ToastrService } from 'ngx-toastr';

import { Observable, throwError} from 'rxjs';
import { HttpErrorResponse, HttpEvent, HttpHandler,HttpInterceptor, HttpRequest } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptorService implements HttpInterceptor {
  constructor(private router: Router, private toastr:ToastrService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe( tap(() => {},
      (err: any) => {
      if (err instanceof HttpErrorResponse) {
        if (err.status !== 401 && err.status !== 403) {
          return throwError(err);
        }
        this.toastr.warning("You are not authorized for this operation","", {positionClass: 'toast-bottom-left'});
        this.router.navigate(['/dashboard']);
        return;
      }
      return throwError(err);
    }));
  } 
}
