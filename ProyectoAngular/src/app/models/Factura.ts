import { Cliente } from "./Cliente";

export interface Factura{
    id:number;
    idCliente:number;
    folio:string;
    saldo:number;
    fechaFacturacion:Date;
    fechaCreacion:Date;
    cliente:Cliente;
}

