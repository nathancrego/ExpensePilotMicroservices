import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { Policy } from '../model/policy.model';
import { Subscription } from 'rxjs';
import { PoliciesService } from '../services/policies.service';
import { ActivatedRoute, Router } from '@angular/router';
import { EditPolicy } from '../model/edit-policy.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-edit-policy',
  templateUrl: './edit-policy.component.html',
  styleUrl: './edit-policy.component.css'
})
export class EditPolicyComponent implements OnInit, OnDestroy {

  id: number | null = null;
  policy?: Policy;


  paramSubscription?: Subscription;
  routeSubscription?: Subscription;
  updatePolicySubscription?: Subscription;

  constructor(private policiesService: PoliciesService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

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

  ngOnInit(): void {
    //Subscription to map the id and also convert id to string and then back to number
    this.routeSubscription = this.paramSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        const idParam = params.get('id');
        this.id = idParam !== null ? Number(idParam) : null;
        //Fetch policy from API
        if (this.id !== null) {
          this.policiesService.getPolicyById(this.id)
            .subscribe({
              next: (response) => {
                this.policy = response;
              },
              error: (err) => {
                console.error('Error fetching policy:', err);
              }
            });
        } else {
          console.warn('No valid policy ID found in route parameters.');
        }
      },
      error: (err) => {
        console.error('Error retrieving route parameters:', err);
      }
    });
  }

  onUpdate(editpolicyForm: NgForm): void {
    if (editpolicyForm.valid) {
      //Convert Model to request object
      if (this.policy && this.id) {
        var editPolicy: EditPolicy = {
          policyName: this.policy?.policyName,
          policyPurpose: this.policy?.policyPurpose,
          policyDescription: this.policy?.policyDescription,
          lastUpdated: new Date()
        };
        this.updatePolicySubscription = this.policiesService.updatePolicy(this.id, editPolicy)
          .subscribe({
            next: (response) => {
              this.router.navigateByUrl('/policy')
            }
          });
      }
    }
    else {
      Object.keys(editpolicyForm.controls).forEach(field => {
        const control = editpolicyForm.control.get(field);
        control?.markAsTouched({ onlySelf: true });
      });
    }
  }

  onCancel(): void {
    this.router.navigateByUrl('/policy');
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updatePolicySubscription?.unsubscribe();
  }

}
