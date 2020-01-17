import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl, ValidatorFn, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { User } from 'src/app/models/user.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  tipovi: string[] = ['obican', 'djak', 'penzioner'];
  registerForm: FormGroup;
  dokument = false;
  fileData: File = null;
  previewUrl: any = null;
  uploadFilePath: string = null;

  constructor(private fb: FormBuilder, private router: Router, private authService: AuthService) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      passwordGroup: this.fb.group({
        password: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', [Validators.required, Validators.minLength(6)]]
      }, {validator: this.checkPasswords}),
      ime: new FormControl(null, Validators.required),
      prezime: new FormControl(null, Validators.required),
      datRodj: new FormControl(null, Validators.required),
      adresa: new FormControl(null, Validators.required),
      tipKorisnika: new FormControl('obican'),
      url: new FormControl(null)
    });
  }

  onSubmit() {
    const formData: FormData = new FormData();
    if (this.fileData != null) {
      formData.append('Url', this.fileData, this.fileData.name);
    }
    // tslint:disable-next-line: max-line-length
    const user = new User (this.registerForm.value.email, this.registerForm.value.ime, this.registerForm.value.prezime, this.registerForm.value.datRodj, this.registerForm.value.adresa, this.registerForm.value.tipKorisnika, 'zahtev se procesira', 'Korisnik', this.registerForm.value.url, this.registerForm.value.passwordGroup.password, this.registerForm.value.passwordGroup.confirmPassword);
    this.authService.addUser(user).subscribe(data => {
      this.router.navigate(['/login']);
    }, err => {
      console.log(err);
    });
  }

  onFileSelected(fileInput: any) {
    this.fileData = fileInput.target.files[0] as File;
    this.preview();
  }

  preview() {
    const mimeType = this.fileData.type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }
    const reader = new FileReader();
    reader.readAsDataURL(this.fileData);
    // tslint:disable-next-line: variable-name
    reader.onload = (_event) => {
      this.previewUrl = reader.result;
    };
  }

  onTipChange(e) {
    if (this.registerForm.value.tipKorisnika === 'djak' || this.registerForm.value.tipKorisnika === 'penzioner') {
      this.dokument = true;
    } else {
      this.dokument = false;
    }
  }

  checkPasswords(group: FormGroup) {
    const pass = group.controls.password.value;
    const confirmPass = group.controls.confirmPassword.value;

    return pass === confirmPass ? null : { notSame: true };
  }
}
