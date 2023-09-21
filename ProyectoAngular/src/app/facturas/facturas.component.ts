import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { FacturasService } from '../services/facturas.service';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Factura } from '../models/Factura';
import { MatTableDataSource } from '@angular/material/table';
import { ConfirmComponent } from '../confirm/confirm.component';
import { InsertFacturaComponent } from './insert-factura/insert-factura.component';

@Component({
  selector: 'app-facturas',
  templateUrl: './facturas.component.html',
  styleUrls: ['./facturas.component.css']
})

export class FacturasComponent implements OnInit {

  @ViewChild(MatPaginator) paginator!: MatPaginator;
displayedColumns: string[] = [
  "cliente",
  "folio",
  "saldo",
  "fecha_Facturacion",
  "fecha_Creacion",
  "acciones"
];


constructor(private facturasService: FacturasService,
  private dialog: MatDialog,
  private formBuilder: FormBuilder,
  private snackBar: MatSnackBar,
  ) {
   
  }
  facturas: Factura[]=[];
  dataSource!: MatTableDataSource<Factura>;
  formGroup: any;

ngOnInit(): void {

  this.actualizarHistorico();
  this.buildForm();
  this.initializeFormGroup();

}


actualizarHistorico() {
  this.facturasService.getFacturas().subscribe((result)=>{
    if (result.success && result.content!=null) {
        this.facturas=result.content;
      this.dataSource = new MatTableDataSource<Factura>(this.facturas);
      this.dataSource.paginator = this.paginator;
    } else {
      this.dataSource = new MatTableDataSource<Factura>([]);
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
  let dialog = this.dialog.open(InsertFacturaComponent, {
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
      data: `¿Está seguro de eliminar esta factura?`,
    })
    .afterClosed()
    .subscribe((confirmado: Boolean) => {
      if (confirmado) {
        this.facturasService.DeleteFactura(id).subscribe((result)=>{
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