import { Component, OnInit } from "@angular/core";
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { Observable } from "rxjs";

@Component({
    selector: 'auto-complete-address-component',
    template: ``
  })
export class AutoCompleteActivitiesComponent {
    search = (text$: Observable<string>) => {
        return text$.pipe(
            debounceTime(100),
            distinctUntilChanged(),
            switchMap(term => this.getActivites(term))
        );
    };

    private getActivites(term: string){
        var promise = new Promise<string[]>((resolve, reject) => {
            
        });
        return promise;
    }
  }