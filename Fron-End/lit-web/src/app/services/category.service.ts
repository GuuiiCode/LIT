import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { BaseCategory, Category } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  API: string = 'http://localhost:3000/api/Category'

  constructor(private http: HttpClient) { }

  getAll(): Observable<Category[]> {
    return this.http.get<Category[]>(this.API);
  }

  getById(id: string): Observable<Category> {
    const url = `${this.API}/${id}`;
    return this.http.get<Category>(url);
  }

  create(category: BaseCategory): Observable<BaseCategory> {
    return this.http.post<BaseCategory>(this.API, category);
  }

  update(category: Category): Observable<Category> {
    const url =  `${this.API}/${category.id}`;
    return this.http.put<Category>(url, category);
  }

  delete(id: string): Observable<string> {
    const url =  `${this.API}/${id}`;
    return this.http.delete<string>(url);
  }
}
