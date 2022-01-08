import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { IProduct } from '../models/product.model';
import { environment } from 'src/environments/environment';

const baseUrl = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClient: HttpClient) { 
  }

  getAllProducts(): Observable<IProduct[]>{
    return this.httpClient.get<IProduct[]>(`${baseUrl}/GetAllProducts`,{});
  }

  getAllBidsForProductId(productId: string): Observable<IProduct>{
     return this.httpClient.get<IProduct>(`${baseUrl}/ShowBids/${productId}`);
  }



}
