import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { AcmeApi, Participant } from "src/app/api";

@Component({
    selector: 'app-participants',
    templateUrl: './participants.component.html',
    styleUrls: ['./participants.component.scss']
})
export class ParticipantsComponent implements OnInit{
    participants : Participant[] = []

    constructor(
        private activatedRoute : ActivatedRoute,
        private acmeApi: AcmeApi) {

    }

    ngOnInit(): void {
        let activityId = this.activatedRoute.snapshot.params.activityId;
        this.acmeApi.getParticipants(parseInt(activityId))
            .then((participants) => {
                this.participants = participants;
            });
    }

    get Event() {
        return this.participants.length == 0? '':this.participants[0].activityName;
    }
}