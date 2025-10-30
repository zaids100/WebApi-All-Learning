import { Component, computed, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { ProductService } from '../core/services/product.service';
import { StorageService } from '../core/services/storage.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  private router = inject(Router);
  private productService = inject(ProductService);
  
  products = this.productService.getProducts();
  private storage = inject(StorageService);
  
  isAdmin = computed(() => {
    const raw = this.storage.getItem('user');
    if (!raw || raw === 'undefined') return false;
    try {
      const user = JSON.parse(raw);
      return user?.isAdmin === true;
    } catch {
      return false;
    }
  });

  addProduct() {
    this.router.navigate(['/products/add']);
  }
}
