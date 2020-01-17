import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { formatDate } from '@angular/common';

import { AuthService } from '../services/auth.service';
import { User } from '../models/user.model';
import { TicketService } from '../services/ticket.service';
import { Karta } from '../models/karta.model';
import { DomSanitizer } from '@angular/platform-browser';
import { ChangePasswordModel } from '../models/password.model';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  tipovi: string[] = ['obican', 'djak', 'penzioner'];
  karte: Karta[];
  user: User;

  accountDataForm: FormGroup;
  changePasswordForm: FormGroup;
  uploadImageForm: FormGroup;
  passwordGroup: FormGroup;

  fileData: File = null;
  previewUrl: any = null;
  imagePath = '';
  safeUrl: any;
  // tslint:disable-next-line: max-line-length
  constructor(private fb: FormBuilder, private authService: AuthService, private ticketService: TicketService, private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.initForm();
    this.authService.getUser().subscribe((data) => {
      this.user = data;
      // tslint:disable-next-line: max-line-length
      this.accountDataForm.patchValue({email: data.Email, ime: data.Ime, prezime: data.Prezime, adresa: data.Adresa, tipKorisnika: data.TipKorisnika, datRodj: data.DatRodj });
      this.authService.userChanged.next(this.user);
      localStorage.tipKorisnika = this.user.TipKorisnika;
      if (this.user.Url) {
        this.authService.getImage().subscribe(imageData => {
          const objectURL = URL.createObjectURL(imageData);
          this.safeUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
        });
      }
    }, err => {
      console.log(err);
    });
  }

  onSubmit() {
    const email = this.accountDataForm.value.email;
    const ime = this.accountDataForm.value.ime;
    const prezime = this.accountDataForm.value.prezime;
    const datRodj = this.accountDataForm.value.datRodj;
    const adresa = this.accountDataForm.value.adresa;
    const tipKorisnika = this.accountDataForm.value.tipKorisnika;

    this.authService.changeUserData(email, ime, prezime, datRodj, adresa, tipKorisnika).subscribe(data => {
      this.user = data;
      this.authService.userChanged.next(this.user);
    });
  }

  changePassword() {
    // tslint:disable-next-line: max-line-length
    const passwords: ChangePasswordModel  = new ChangePasswordModel(this.changePasswordForm.value.passwordGroup.oldPassword, this.changePasswordForm.value.passwordGroup.newPassword, this.changePasswordForm.value.passwordGroup.confirmNewPassword);
    this.authService.changePassword(passwords).subscribe(data => {
      console.log(data);
    }, err => {
      console.log(err);
    });
  }

  uploadImage() {
    this.authService.uploadImage(this.fileData).subscribe(data => {
      console.log(data);
      console.log('done');
    });
  }

  checkPasswords(group: FormGroup) {
    const pass = group.controls.newPassword.value;
    const confirmPass = group.controls.confirmNewPassword.value;
    if (pass === confirmPass) {
      if (pass.length >= 6) {
        return null;
      } else {
        return { length: true };
      }
    } else {
      return { notSame: true };
    }
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

  initForm() {
    this.accountDataForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      ime: new FormControl(null, Validators.required),
      prezime: new FormControl(null, Validators.required),
      datRodj: new FormControl(formatDate(null, 'yyyy-MM-dd', 'en'), Validators.required),
      adresa: new FormControl(null, Validators.required),
      tipKorisnika: new FormControl(null),
    });
    this.changePasswordForm = new FormGroup({
      passwordGroup: this.fb.group({
        oldPassword: [''],
        newPassword: [''],
        confirmNewPassword: ['']
      }, {validator: this.checkPasswords.bind(this)})
    });

    this.uploadImageForm = new FormGroup({
      url: new FormControl(null, Validators.required)
    });
  }
}
