import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from 'src/app/account/account.service';
import { AdminService } from '../../admin.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  form: FormGroup;
  returnUrl: string;

  constructor(
    private adminService: AdminService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/shop';
  }

  ngOnInit(): void {
    this.form = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.pattern("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$")])
    })
  }

  onSubmit() {
    this.adminService.login(this.form.value).subscribe({
      next: user => {
        this.router.navigateByUrl(this.returnUrl);
      }
    })
  }
}
