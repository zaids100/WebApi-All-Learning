import { Injectable, signal } from '@angular/core';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private products = signal<Product[]>([
    {
      id: 1,
      name: 'Laptop',
      description: 'High-performance laptop',
      price: 999.99,
      imageUrl: 'https://placeholder.com/150'
    },
    {
      id: 2,
      name: 'Smartphone',
      description: 'Latest model smartphone',
      price: 699.99,
      imageUrl: 'https://placeholder.com/150'
    }
  ]);

  getProducts() {
    return this.products;
  }

  addProduct(product: Omit<Product, 'id'>) {
    const newProduct = {
      ...product,
      id: Math.max(0, ...this.products().map(p => p.id)) + 1
    };
    
    this.products.update(products => [...products, newProduct]);
    return newProduct;
  }
}