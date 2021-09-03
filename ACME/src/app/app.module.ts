import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RegistrationComponent } from './modules/registration/registration.component';
import { AcmeApi } from './api';
import { Router } from '@angular/router';
import { MockTokenCredential } from './components/mock-token-credential.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [
    {
      provide: AcmeApi,
      useFactory: AcmeApiFactory
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function AcmeApiFactory() {
  return new AcmeApi(new MockTokenCredential(), 'https://localhost:44331/');
}