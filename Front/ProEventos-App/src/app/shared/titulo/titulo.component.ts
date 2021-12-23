import { Route } from '@angular/compiler/src/core';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {

  constructor(private router: Router) {}

  ngOnInit() {
  }

  @Input() titulo: string = '';
  @Input() iconClass: string = 'fa fa-user';
  @Input() subtitulo: string = 'Desde 2021';
  @Input() botaoListar: boolean = false;

  listar(): void{
    this.router.navigate([`/${this.titulo.toLocaleLowerCase()}/lista`]);
  }

}
