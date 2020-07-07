import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class GdprService {
  baseUrl = environment.apiUrl + 'gdpr/';

  constructor(private http: HttpClient) {
  }

  deleteAccount(id) {
    return this.http.delete(this.baseUrl + 'user/delete/' + id)
  }
}
