import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PolicyListComponent } from './policies/policy-list/policy-list.component';
import { AddPolicyComponent } from './policies/add-policy/add-policy.component';
import { EditPolicyComponent } from './policies/edit-policy/edit-policy.component';
import { AddRolesComponent } from './admin/rolemanagement/add-roles/add-roles.component';
import { RolesListComponent } from './admin/rolemanagement/roles-list/roles-list.component';
import { EditRolesComponent } from './admin/rolemanagement/edit-roles/edit-roles.component';
import { AddUserComponent } from './admin/usermanagement/add-user/add-user.component';
import { UserListComponent } from './admin/usermanagement/user-list/user-list.component';
import { EditUserComponent } from './admin/usermanagement/edit-user/edit-user.component';
import { RegisterComponent } from './authentication/register/register.component';
import { LoginComponent } from './authentication/login/login.component';
import { DashboardComponent } from './dashboard/dashboard/dashboard.component';
import { ListExpenseComponent } from './expense/list-expense/list-expense.component';
import { AddExpenseComponent } from './expense/add-expense/add-expense.component';
import { EditExpenseComponent } from './expense/edit-expense/edit-expense.component';
import { CategoryListComponent } from './admin/expensecategory/category-list/category-list.component';
import { AddCategoryComponent } from './admin/expensecategory/add-category/add-category.component';
import { EditCategoryComponent } from './admin/expensecategory/edit-category/edit-category.component';
import { ExpensestatusListComponent } from './admin/expensestatus/expensestatus-list/expensestatus-list.component';
import { AddExpensestatusComponent } from './admin/expensestatus/add-expensestatus/add-expensestatus.component';
import { EditExpensestatusComponent } from './admin/expensestatus/edit-expensestatus/edit-expensestatus.component';
import { AddDepartmentComponent } from './admin/departmentmanagement/add-department/add-department.component';
import { DepartmentListComponent } from './admin/departmentmanagement/department-list/department-list.component';
import { EditDepartmentComponent } from './admin/departmentmanagement/edit-department/edit-department.component';

const routes: Routes = [
  //{
  //  path: '',
  //  redirectTo: '/login', // Redirect to login by default
  //  pathMatch: 'full'
  //},
  {
    path: 'login',
    component:LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  //{
  //  path: '**',
  //  redirectTo:'login' //Redirect any unknown routes to login
  //},
  {
    path: 'dashboard',
    component: DashboardComponent
  },
  {
    path: 'expense',
    component:ListExpenseComponent
  },
  {
    path: 'expense/add',
    component: AddExpenseComponent
  },
  {
    path: 'expense/:id',
    component: EditExpenseComponent
  },
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
  {
    path: 'admin/roles/add',
    component: AddRolesComponent
  },
  {
    path: 'admin/roles',
    component: RolesListComponent
  },
  {
    path: 'admin/roles/:id',
    component: EditRolesComponent
  },
  {
    path: 'admin/users/add',
    component: AddUserComponent
  },
  {
    path: 'admin/users',
    component: UserListComponent
  },
  {
    path: 'admin/users/:id',
    component: EditUserComponent
  },
  {
    path: 'admin/expensecategory',
    component: CategoryListComponent
  },
  {
    path: 'admin/expensecategory/add',
    component: AddCategoryComponent
  },
  {
    path: 'admin/expensecategory/:id',
    component: EditCategoryComponent
  },
  {
    path: 'admin/expensestatus',
    component: ExpensestatusListComponent
  },
  {
    path: 'admin/expensestatus/add',
    component: AddExpensestatusComponent
  },
  {
    path: 'admin/expensestatus/:id',
    component: EditExpensestatusComponent
  },
  {
    path: 'admin/department/add',
    component: AddDepartmentComponent
  },
  {
    path: 'admin/department',
    component: DepartmentListComponent
  },
  {
    path: 'admin/department/:id',
    component: EditDepartmentComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
