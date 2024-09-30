import { Component } from '@angular/core';

//decorator que fala que este cara é um componente angular
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Skinet';
}
