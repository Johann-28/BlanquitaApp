import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TabsPage } from './tabs.page';
import { CorteCajaPageModule } from '../corte-caja/corte-caja.module';
import { RoleGuard } from '../auth/RoleGuard';

const routes: Routes = [
  {
    path: '',
    component: TabsPage,
    children: [
      {
        path: 'tab1',
        canLoad : [RoleGuard],
        loadChildren: () => import('../tab1/tab1.module').then(m => m.Tab1PageModule)
      },
      {
        path: 'tab2',
        canLoad : [RoleGuard],
        loadChildren: () => import('../tab2/tab2.module').then(m => m.Tab2PageModule)
      },
      {
        path: 'combo',
        canLoad : [RoleGuard],
        loadChildren: () => import('../tab3/tab3.module').then(m => m.Tab3PageModule)
      },
      {
        path: 'corte-caja',
        loadChildren: () => import('../corte-caja/corte-caja.module').then(m => m.CorteCajaPageModule)
      },
      {
        path: 'orden',
        loadChildren: () => import('../orden/orden.module').then(m => m.OrdenPageModule)
      },
      {
        path: '',
        redirectTo: '/tabs/tab1',
        pathMatch: 'full'
      }
    ]
  },
  {
    path: '',
    redirectTo: '/tabs/tab1',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
})
export class TabsPageRoutingModule {}
