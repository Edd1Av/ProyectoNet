import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {IDelete} from 'src/app/models/Cliente';
import {Response} from 'src/app/models/Response';
import { Factura } from '../models/Factura';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
}) 



export class FacturasService {
  public urlBase: string;

  constructor(private http: HttpClient) {
    this.urlBase = environment.api;;
  }

  getFacturas(): Observable<Response> {
    return this.http.get<Response>(this.urlBase + "api/Factura/Facturas");
  }

  SetFactura(factura:Factura)  {
      return this.http.post<Response>(this.urlBase + "api/Factura",factura);
    }

  DeleteFactura(id:number): Observable<Response> {
      return this.http.delete<Response>(this.urlBase + "api/Factura/"+id);
  }
}