import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { Policy } from '../model/policy.model';
import { PoliciesService } from '../services/policies.service';

@Component({
  selector: 'app-policy-list',
  templateUrl: './policy-list.component.html',
  styleUrl: './policy-list.component.css'
})
export class PolicyListComponent implements OnInit, OnDestroy {

  policyID: number | null = null;
  policies$?: Observable<Policy[]>;
  deletePolicySubscription?: Subscription;
  constructor(private policiesService: PoliciesService, private router: Router) { }

  readonly panelOpenState = signal(false);

  ngOnInit(): void {
    this.policies$ = this.policiesService.getAllPolicies();
  }

  onDelete(): void {
    if (this.policyID) {
      //Call service to delete policy
      this.deletePolicySubscription = this.policiesService.deletePolicy(this.policyID)
        .subscribe({
          next: (response) => {
            this.ngOnInit(); //to refresh the policy list
          }
        });
    }
  }

  ngOnDestroy(): void {
    this.deletePolicySubscription?.unsubscribe();
  }
}
