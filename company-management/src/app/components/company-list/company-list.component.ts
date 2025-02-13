import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompanyService } from '../../services/company.service';
import { Company } from '../../models/company.model';
import { CompanyFormComponent } from '../company-form/company-form.component';

@Component({
  selector: 'app-company-list',
  standalone: true,
  imports: [CommonModule, CompanyFormComponent], // ✅ Import CompanyFormComponent
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.css']
})
export class CompanyListComponent {
  companies: Company[] = [];
  selectedCompany: Company | null = null; // ✅ Track selected company
  showForm = false; // ✅ Manage form visibility

  constructor(private companyService: CompanyService) {}

  ngOnInit(): void {
    this.loadCompanies();
  }

  loadCompanies(): void {
    this.companyService.getCompanies().subscribe((data) => {
      this.companies = data;
      this.selectedCompany = null; // ✅ Reset selection after reload
    });
  }

  addCompany(): void {
    this.selectedCompany = null; // ✅ Clear selection for new company
    this.showForm = false; // ✅ Close the form first
    setTimeout(() => {
      this.showForm = true; // ✅ Open the form after reset
    }, 0);
  }
  

  editCompany(company: Company): void {
    this.selectedCompany = { ...company }; // ✅ Create a fresh copy
    this.showForm = false; // ✅ Close form first to reset state
    setTimeout(() => {
      this.showForm = true; // ✅ Reopen form to trigger ngOnInit()
    }, 0);
  }
  
  
  saveCompany(company: Company): void {
    if (company.id) {
      // ✅ Editing an existing company
      this.companyService.updateCompany(company.id, company).subscribe({
        next: () => {
          this.loadCompanies(); // ✅ Reload the list after update
          this.showForm = false;
          this.selectedCompany = null; // ✅ Reset selection to enable Delete buttons
        },
        error: (error) => {
          console.error("Update failed:", error);
          alert(error?.message ?? "Error updating company. Check the console for details.");
        }
      });
    } else {
      // ✅ Adding a new company
      this.companyService.createCompany(company).subscribe({
        next: (newCompany) => {
          this.companies.push(newCompany); // ✅ Add new company to the list
          this.showForm = false;
          this.selectedCompany = null; // ✅ Reset selection to enable Delete buttons
        },
        error: (error) => {
          console.error("Create failed:", error);
          alert(error?.message ?? "Error creating company. Check the console for details.");
        }
      });
    }
  }
  

  cancelForm(): void {
    this.showForm = false; // ✅ Hide form when cancelled
    this.selectedCompany = null; // ✅ Reset selection so Delete buttons are enabled again
  }  

  deleteCompany(id: number): void {
    this.companyService.deleteCompany(id).subscribe(() => this.loadCompanies());
  }
}
