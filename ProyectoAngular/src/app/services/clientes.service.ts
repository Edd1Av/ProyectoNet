import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {Cliente, IDelete} from 'src/app/models/Cliente';
import {Response} from 'src/app/models/Response';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
}) 



export class ClientesService {
  public urlBase: string;

  constructor(private http: HttpClient) {
    this.urlBase = environment.api;
  }

  getClientes(): Observable<Response> {
    return this.http.get<Response>(this.urlBase + "api/Cliente/clientes");
  }

  SetCliente(cliente:Cliente)  {
    //cliente.user=User;
      return this.http.post<Response>(this.urlBase + "api/Cliente",cliente);
    }

  DeleteCliente(id:number): Observable<Response> {
      return this.http.delete<Response>(this.urlBase + "api/Cliente/"+id);
  }
}