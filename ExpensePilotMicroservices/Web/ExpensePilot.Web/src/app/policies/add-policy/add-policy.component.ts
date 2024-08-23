import { Component, OnDestroy, signal } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AddPolicy } from '../model/add-policy.model';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { PoliciesService } from '../services/policies.service';

@Component({
  selector: 'app-add-policy',
  templateUrl: './add-policy.component.html',
  styleUrl: './add-policy.component.css'
})
export class AddPolicyComponent implements OnDestroy {

  policy: AddPolicy;
  private addPolicySubscription?: Subscription;
  constructor(private policiesService: PoliciesService, private router: Router) {
    this.policy = {
      policyName: '',
      policyPurpose: '',
      policyDescription: '',
      lastUpdated: new Date()
    };
  }

  step = signal(0);
  setStep(index: number) {
    this.step.set(index);
  }
  nextStep() {
    this.step.update(i => i + 1);
  }
  prevStep() {
    this.step.update(i => i - 1);
  }

  onSubmit(policyForm: NgForm): void {
    if (policyForm.valid) {
      this.addPolicySubscription = this.policiesService.createPolicy(this.policy)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/policy')
          }
        });
    }
    else {
      Object.keys(policyForm.controls).forEach(field => {
        const control = policyForm.control.get(field);
        control?.markAsTouched({ onlySelf: true });
      });
    }
  }

  ngOnDestroy(): void {
    this.addPolicySubscription?.unsubscribe();
  }

}
