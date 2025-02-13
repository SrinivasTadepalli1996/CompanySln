import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { Company } from '../../models/company.model';

@Component({
  selector: 'app-company-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './company-form.component.html',
  styleUrls: ['./company-form.component.css']
})
export class CompanyFormComponent {
  @Input() company: Company | null = null;
  @Output() formSubmit = new EventEmitter<Company>(); // Emit form data
  @Output() cancel = new EventEmitter<void>(); // Emit cancel event

  companyForm = new FormGroup({
    name: new FormControl('', Validators.required),
    exchange: new FormControl('', Validators.required),
    ticker: new FormControl('', Validators.required),
    isin: new FormControl('', Validators.required),
    website: new FormControl('')
  });

  isEditMode: boolean = false; // Track edit mode

  ngOnInit() {
    if (this.company) {
      this.isEditMode = true; // ✅ Enter edit mode if a company is passed
      this.companyForm.patchValue(this.company);
      this.companyForm.controls['ticker'].disable();
      this.companyForm.controls['isin'].disable();
    } else {
      this.isEditMode = false; // ✅ If adding, keep form enabled
      this.companyForm.enable();
    }
  }

  submitForm() {
    if (this.companyForm.valid) {
      const companyData = { ...this.company, ...this.companyForm.value } as Company;
      this.formSubmit.emit(companyData); // ✅ Emit form data to parent
    }
  }

  cancelForm() {
    this.cancel.emit(); // ✅ Notify parent to close form
  }
}
