import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./tabs/tabs.module').then(m => m.TabsPageModule)
  },  {
    path: 'corte-caja',
    loadChildren: () => import('./corte-caja/corte-caja.module').then( m => m.CorteCajaPageModule)
  },
  {
    path: 'orden',
    loadChildren: () => import('./orden/orden.module').then( m => m.OrdenPageModule)
  }

];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
