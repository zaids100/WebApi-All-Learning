import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductService } from '../../core/services/product.service';

@Component({
  selector: 'app-add-product',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="container mx-auto px-4 py-8">
      <h1 class="text-2xl font-bold text-gray-800 mb-6">Add New Product</h1>
      
      <form [formGroup]="productForm" (ngSubmit)="onSubmit()" class="max-w-lg">
        <div class="space-y-4">
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Product Name</label>
            <input type="text" formControlName="name"
              class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 outline-none" />
          </div>

          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Description</label>
            <textarea formControlName="description" rows="3"
              class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 outline-none"></textarea>
          </div>

          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Price</label>
            <input type="number" formControlName="price"
              class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 outline-none" />
          </div>

          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Image URL</label>
            <input type="url" formControlName="imageUrl"
              class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 outline-none" />
          </div>

          <div class="flex gap-4">
            <button type="submit" [disabled]="productForm.invalid"
              class="flex-1 bg-indigo-600 hover:bg-indigo-700 text-white font-medium py-2 rounded-lg transition disabled:opacity-50">
              Add Product
            </button>
            <button type="button" (click)="cancel()"
              class="flex-1 bg-gray-200 hover:bg-gray-300 text-gray-800 font-medium py-2 rounded-lg transition">
              Cancel
            </button>
          </div>
        </div>
      </form>
    </div>
  `
})
export class AddProductComponent {
  private router = inject(Router);
  private productService = inject(ProductService);
  private fb = inject(FormBuilder);

  productForm = this.fb.group({
    name: ['', [Validators.required]],
    description: ['', [Validators.required]],
    price: [0, [Validators.required, Validators.min(0)]],
    imageUrl: ['']
  });

  onSubmit() {
    if (this.productForm.invalid) return;

    this.productService.addProduct(this.productForm.value as any);
    this.router.navigate(['/dashboard']);
  }

  cancel() {
    this.router.navigate(['/dashboard']);
  }
}