import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from './services/auth.service';
import { API_BASE_URL } from './app.constants';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const token = authService.token();

  const apiRequest = req.url.startsWith('http')
    ? req
    : req.clone({ url: `${API_BASE_URL}${req.url}` });

  if (!token) {
    return next(apiRequest);
  }

  return next(
    apiRequest.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    })
  );
};
