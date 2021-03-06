import { Component,  OnInit, TemplateRef, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, OperatorFunction } from 'rxjs';
import {debounceTime, distinctUntilChanged, map, switchMap} from 'rxjs/operators';
import { AcmeApi, Activity } from 'src/app/api';

@Component({
    selector: 'app-registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit
{
    @ViewChild('content', {read: TemplateRef}) content: TemplateRef<any> | undefined;
    @ViewChild('errorContent', {read: TemplateRef}) errorContent: TemplateRef<any> | undefined;
    @ViewChild('alreadyRegisteredContent', {read: TemplateRef}) alreadyRegisteredContent: TemplateRef<any> | undefined;
    activities: Activity[] = [];
    submitted: boolean = false;
    registrationForm: FormGroup;
    registrationErrorCode: number = 0;

    constructor(
        private acmeApi: AcmeApi,
        private formBuilder: FormBuilder,
        private router: Router,
        private modalService: NgbModal
    ) {
        this.registrationForm = this.formBuilder.group({
            firstName: ['',[Validators.required, Validators.maxLength(50)]],
            lastName: ['', [Validators.required, Validators.maxLength(50)]],
            email: ['', [Validators.required, Validators.email, Validators.maxLength(50)]],
            activity: ['', [Validators.required]],
            comments: ['']
        });
    }

    ngOnInit() {
        this.acmeApi.getActivities({ searchKeyword: ''}).then((result) => {
            this.activities = result;
        });
    }

    signUp(){
        this.submitted = true;
        
        let hasActivityErrors = !this.isActivityFound();
        
        if (!this.registrationForm.invalid && !hasActivityErrors) {
            let participant = this.registrationForm.value;
            let activityId = participant.activity.id;

            this.acmeApi.signUp(activityId, { body: participant })
                .then((result) => {
                    this.modalService.open(this.content).result
                    .then(() => {
                        this.router.navigate(['/participants/' + activityId], { replaceUrl: true });
                    }).catch(()=> {
                        this.router.navigate(['/participants/' + activityId], { replaceUrl: true });
                    });
                    
                }).catch((registrationErrorCode) => {
                    if(registrationErrorCode.message == 1004) {
                        this.modalService.open(this.alreadyRegisteredContent);
                    } else {
                        this.modalService.open(this.errorContent);
                    }
                    // Create a reusable modal to render different error messages per error code.
                });
        }
    }

    isActivityFound() {
        let activity = this.registrationForm.get('activity');
        
        let isActivityFound = this.activities.filter(a => a.name == activity?.value.name).length > 0;

        if (activity?.value == '') {
            return true; // let required validator handle it
        } else if (!isActivityFound) {
            activity?.setErrors({'notfound': true})
        }

        return isActivityFound;
    }

    get f(): { [key: string]: AbstractControl } {
        return this.registrationForm.controls;
    }

    search: OperatorFunction<string, readonly Activity[]> = (text$: Observable<string>) =>
        text$.pipe(
            debounceTime(200),
            distinctUntilChanged(),
            map(term => term == '' ? [] : this.activities.filter(v => {
                let keyword = ''
                if(v.name != null) {
                    keyword = v.name;    
                }
                return keyword.toLowerCase().indexOf(term.toLowerCase()) > -1
            })
            .slice(0, 10)));
            
    formatter = (x: Activity) => x.name? x.name: '';
}

