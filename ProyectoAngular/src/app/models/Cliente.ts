import { Factura } from "./Factura";

export interface Cliente
{
    id:number;
    nombre: string;
    apellido: string; 
    edad: number;
    correoElectronico: string; 
    Facturas:Factura[];
}

export interface IDelete{
    id:Number;
    user:string;
}