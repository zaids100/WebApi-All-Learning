import { inject } from '@angular/core';
import { Router, type CanActivateFn } from '@angular/router';
import { StorageService } from '../services/storage.service';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const storage = inject(StorageService);
  const token = storage.getItem('accessToken');

  if (!token) {
    router.navigate(['/login']);
    return false;
  }

  // Check if route requires admin role
  if (route.data?.['roles']?.includes('admin')) {
    const user = JSON.parse(storage.getItem('user') || '{}');
    if (!user.isAdmin) {
      router.navigate(['/dashboard']);
      return false;
    }
  }

  return true;
};