import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ClientesService } from 'src/app/services/clientes.service';
import {Response} from 'src/app/models/Response';

@Component({
  selector: 'app-insert-cliente',
  templateUrl: './insert-cliente.component.html',
  styleUrls: ['./insert-cliente.component.css']
})

export class InsertClienteComponent implements OnInit {

  formGroup!: FormGroup;
 
  constructor(
    @Inject(MAT_DIALOG_DATA) private data:any,
    private matDialogref: MatDialogRef<InsertClienteComponent>,
    private formBuilder: FormBuilder,
    private _usuariosService: ClientesService,
    private _snackBar: MatSnackBar
  ) { }
  ngOnInit(): void {
    
  this.buildForm();
  }

  private buildForm() {
    this.formGroup = this.formBuilder.group({
      nombre: new FormControl("", Validators.required),
      apellido: new FormControl("", Validators.required),
      edad: new FormControl("", Validators.required),
      correoElectronico: new FormControl("", Validators.required),
    });
  }

  openSnackBar(message:string) {
    this._snackBar.open(message, undefined, {
      duration: 2000,
    });
  }

  onSubmit() {
    if(this.formGroup.valid){

      this._usuariosService.SetCliente(this.formGroup.value).subscribe((result)=>{
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
