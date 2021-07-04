import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../auth/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    var authHeader = this.authenticationService.authHeader;
    if (authHeader) {
      request = request.clone({
        setHeaders: {
          Authorization: authHeader
        }
      });
    }

    return next.handle(request);
  }
}
