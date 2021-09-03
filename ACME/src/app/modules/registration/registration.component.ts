import { Component, OnInit } from '@angular/core';
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
    activities: Activity[] = [];
    constructor(
        private acmeApi: AcmeApi
    ) {}

    ngOnInit() {
        this.acmeApi.getActivities({ searchKeyword: ''}).then((result) => {
            this.activities = result;
        })
    }

    signUp(){
        
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

