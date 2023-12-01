import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/AuthGuard';
import { RedirectComponent } from './redirect/redirect.component';

const routes: Routes = [
  {
    path: 'tabs',
    loadChildren: () => import('./tabs/tabs.module').then(m => m.TabsPageModule),
    canActivate : [AuthGuard]
  },
  {
    path: 'corte-caja',
    loadChildren: () => import('./corte-caja/corte-caja.module').then( m => m.CorteCajaPageModule)
  },
  {
    path: 'orden',
    loadChildren: () => import('./orden/orden.module').then( m => m.OrdenPageModule)
  },
  {
    path : 'login',
    loadChildren: () => import('./login/login.module').then( m => m.LoginModule)
  },
  {
    path : '',
    component : RedirectComponent,
    pathMatch : 'full'
  }

];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
