import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Cliente } from 'src/app/models/Cliente';
import { FacturasService } from 'src/app/services/facturas.service';
import { ClientesService } from 'src/app/services/clientes.service';

@Component({
  selector: 'app-insert-factura',
  templateUrl: './insert-factura.component.html',
  styleUrls: ['./insert-factura.component.css']
})


export class InsertFacturaComponent implements OnInit {

  formGroup!: FormGroup;
 
  constructor(
    @Inject(MAT_DIALOG_DATA) private data:any,
    private matDialogref: MatDialogRef<InsertFacturaComponent>,
    private formBuilder: FormBuilder,
    private _facturasService: FacturasService,
    private _clientesService: ClientesService,
    private _snackBar: MatSnackBar
  ) { }
  clientes: Cliente[]=[];
  ngOnInit(): void {
  this.GetClientes();
  this.buildForm();
  
  }

  private GetClientes(){
    this._clientesService.getClientes().subscribe((result)=>{
      if(result.success==true && result.content!=null){
        this.clientes = result.content;
      }
      else{
        this.openSnackBar(result.message);
      }      
    });
  }

  private buildForm() {
    this.formGroup = this.formBuilder.group({
      idCliente: new FormControl("", Validators.required),
      folio: new FormControl("", Validators.required),
      saldo: new FormControl("", Validators.required),
      fechaFacturacion: new FormControl(),
    });
  }

  openSnackBar(message:string) {
    this._snackBar.open(message, undefined, {
      duration: 2000,
    });
  }

  onSubmit() {
    if(this.formGroup.valid){

      this._facturasService.SetFactura(this.formGroup.value).subscribe((result)=>{
        if (result.success) {
          this.openSnackBar(result.message);
          this.matDialogref.close();
        } else {
          this.openSnackBar(result.message);
        }
      }); 
    }else{
      this.openSnackBar("Introduzca los campos faltantes");
    }
  }

}
