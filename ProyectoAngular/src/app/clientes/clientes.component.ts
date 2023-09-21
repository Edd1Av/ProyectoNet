import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ClientesService } from '../services/clientes.service';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Cliente } from '../models/Cliente';
import { MatTableDataSource } from '@angular/material/table';
import { ConfirmComponent } from '../confirm/confirm.component';
import { InsertClienteComponent } from './insert-cliente/insert-cliente.component';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})

export class ClientesComponent implements OnInit {

  @ViewChild(MatPaginator) paginator!: MatPaginator;
displayedColumns: string[] = [
  "nombre",
  "apellido",
  "edad",
  "correo_electronico",
  "acciones"
];


constructor(private usuariosService: ClientesService,
  private dialog: MatDialog,
  private formBuilder: FormBuilder,
  private snackBar: MatSnackBar
  ) {
   
  }
  clientes: Cliente[]=[];
  dataSource!: MatTableDataSource<Cliente>;
  formGroup: any;

ngOnInit(): void {
  this.actualizarHistorico();
  this.buildForm();
  this.initializeFormGroup();
}


actualizarHistorico() {

  this.usuariosService.getClientes().subscribe((result)=>{
    if (result.success==true && result.content!=null) {
      this.clientes = result.content;
        this.dataSource = new MatTableDataSource<Cliente>(this.clientes);
        this.dataSource.paginator = this.paginator;
    } else {
      this.dataSource = new MatTableDataSource<Cliente>([]);
      this.dataSource.paginator = this.paginator;
      this.openSnackBar(result.message);
    }
  });
}

private buildForm() {
  this.formGroup = this.formBuilder.group({
    buscador: new FormControl(""),
  });
}

initializeFormGroup() {
  this.formGroup.setValue({
    buscador: "",
  });
}

filtrarTabla() {
  this.dataSource.filter = this.formGroup.get("buscador").value;
}


openDialogInsert(): void {
  let dialog = this.dialog.open(InsertClienteComponent, {
    width: "800px",
    disableClose: true,
  });
  dialog.afterClosed().subscribe((result) => {
    this.actualizarHistorico();
    this.filtrarTabla();
  });
}

mostrarDialogo(id:number): void {
  this.dialog
    .open(ConfirmComponent, {
      data: `¿Está seguro de eliminar a este cliente?`,
    })
    .afterClosed()
    .subscribe((confirmado: Boolean) => {
      if (confirmado) {
        this.usuariosService.DeleteCliente(id).subscribe((result)=>{
          if (result.success) {
            this.actualizarHistorico();
            this.openSnackBar(result.message);
          } else {
            this.openSnackBar(result.message);
          }
        });
      }
    });
}

openSnackBar(message:string) {
  this.snackBar.open(message, undefined, {
    duration: 3000,
  });
}

}
