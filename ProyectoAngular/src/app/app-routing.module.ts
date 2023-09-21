import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: 'Clientes', loadChildren: () => import('./clientes/clientes.module').then(m => m.ClientesModule)}, 
  { path: 'Facturas', loadChildren: () => import('./facturas/facturas.module').then(m => m.FacturasModule)},
  { path: '', component: HomeComponent},
  { path: 'home', component: HomeComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { 

  
}
