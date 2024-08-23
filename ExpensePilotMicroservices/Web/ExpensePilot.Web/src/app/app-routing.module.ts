import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PolicyListComponent } from './policies/policy-list/policy-list.component';
import { AddPolicyComponent } from './policies/add-policy/add-policy.component';
import { EditPolicyComponent } from './policies/edit-policy/edit-policy.component';

const routes: Routes = [
  {
  path: 'policy',
  component: PolicyListComponent
  },
  {
  path: 'policy/add',
  component: AddPolicyComponent
  },
  {
  path: 'policy/:id',
  component: EditPolicyComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
