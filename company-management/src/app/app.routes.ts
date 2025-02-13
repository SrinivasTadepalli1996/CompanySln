import { Routes } from '@angular/router';
import { CompanyListComponent } from './components/company-list/company-list.component';
import { CompanyFormComponent } from './components/company-form/company-form.component';

export const routes: Routes = [
  { path: '', component: CompanyListComponent },
  { path: 'company/add', component: CompanyFormComponent },
  { path: 'company/edit/:id', component: CompanyFormComponent }
];



