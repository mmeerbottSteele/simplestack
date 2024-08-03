import { Component } from '@angular/core';
import { SimpleService } from './simple.service';
import { Message } from './models/message.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'api-training';
  text = '';

  constructor(private _simpleService: SimpleService) {}

  onClick() {
    this._simpleService.getHelloWorld().subscribe({
      next: (res: Message) => {
        this.text = res.text;
      },
      error: (err) => {
        this.text = err;
      },
    });
  }
}
