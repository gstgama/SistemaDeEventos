import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  @Input() titulo: string = '';
  @Input() iconClass: string = 'fa fa-user';
  @Input() subtitulo: string = 'Desde 2021';
  @Input() botaoListar: boolean = false;

}
