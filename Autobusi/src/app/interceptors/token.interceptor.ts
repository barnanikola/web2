import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const jwt = localStorage.jwt;
        if (jwt) {
            req = req.clone({
                setHeaders: {
                    Authorization: 'Bearer ' + jwt
                }
            });
        }
        return next.handle(req);
    }
}

