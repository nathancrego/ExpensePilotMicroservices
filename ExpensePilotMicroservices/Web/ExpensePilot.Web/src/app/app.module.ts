import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { NavbarComponent } from './features/navbar/navbar.component';
import { AddPolicyComponent } from './policies/add-policy/add-policy.component';
import { EditPolicyComponent } from './policies/edit-policy/edit-policy.component';
import { PolicyListComponent } from './policies/policy-list/policy-list.component';
import { MatAccordion, MatExpansionModule } from '@angular/material/expansion';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddRolesComponent } from './admin/rolemanagement/add-roles/add-roles.component';
import { EditRolesComponent } from './admin/rolemanagement/edit-roles/edit-roles.component';
import { RolesListComponent } from './admin/rolemanagement/roles-list/roles-list.component';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { AddUserComponent } from './admin/usermanagement/add-user/add-user.component';
import { EditUserComponent } from './admin/usermanagement/edit-user/edit-user.component';
import { UserListComponent } from './admin/usermanagement/user-list/user-list.component';
import { MatSelectModule } from '@angular/material/select';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { RegisterComponent } from './authentication/register/register.component';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from "jwt-decode";
import { AuthInterceptor } from './authentication/AuthInterceptor';
import { LoginComponent } from './authentication/login/login.component';
import { AddExpenseComponent } from './expense/add-expense/add-expense.component';
import { EditExpenseComponent } from './expense/edit-expense/edit-expense.component';
import { ListExpenseComponent } from './expense/list-expense/list-expense.component';
import { DashboardComponent } from './dashboard/dashboard/dashboard.component';
import { AddCategoryComponent } from './admin/expensecategory/add-category/add-category.component';
import { EditCategoryComponent } from './admin/expensecategory/edit-category/edit-category.component';
import { CategoryListComponent } from './admin/expensecategory/category-list/category-list.component';
import { AddExpensestatusComponent } from './admin/expensestatus/add-expensestatus/add-expensestatus.component';
import { EditExpensestatusComponent } from './admin/expensestatus/edit-expensestatus/edit-expensestatus.component';
import { ExpensestatusListComponent } from './admin/expensestatus/expensestatus-list/expensestatus-list.component';
import { AddDepartmentComponent } from './admin/departmentmanagement/add-department/add-department.component';
import { EditDepartmentComponent } from './admin/departmentmanagement/edit-department/edit-department.component';
import { DepartmentListComponent } from './admin/departmentmanagement/department-list/department-list.component';
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    AddPolicyComponent,
    EditPolicyComponent,
    PolicyListComponent,
    AddRolesComponent,
    EditRolesComponent,
    RolesListComponent,
    AddUserComponent,
    EditUserComponent,
    UserListComponent,
    LoginComponent,
    RegisterComponent,
    AddExpenseComponent,
    EditExpenseComponent,
    ListExpenseComponent,
    DashboardComponent,
    AddCategoryComponent,
    EditCategoryComponent,
    CategoryListComponent,
    AddExpensestatusComponent,
    EditExpensestatusComponent,
    ExpensestatusListComponent,
    AddDepartmentComponent,
    EditDepartmentComponent,
    DepartmentListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatExpansionModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatIconModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatSelectModule,
    MatAutocompleteModule,
    
  ],
  providers: [
    provideAnimationsAsync(),
    CookieService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
