import { Component, OnInit } from '@angular/core';
import { filter } from 'rxjs';
import { Evento } from '../models/Evento';
import { EventoService } from '../services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
  //providers: [EventoService]
})
export class EventosComponent implements OnInit {

  constructor(private eventoService: EventoService) { }

  public ngOnInit(): void {
    this.getEventos();
  }

  public widthImg: number = 150;
  public marginImg: number = 2;
  public showImg: boolean = false;

  public eventos: Evento[] = [];
  public filteredEventos: Evento[] = [];
  private _filterList: string = ''

  public get filterList(): string{
    return this._filterList;
  }

  public set filterList(value: string){
    this._filterList = value;
    this.filteredEventos = this.filterList ? this.filterEventos(this.filterList) : this.eventos;
  }

  public filterEventos(filterBy: string): Evento[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: { tema: string; local: string}) => evento.tema.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filterBy) !== -1
    )
  }

  public getEventos(){
    this.eventoService.getEventos().subscribe(
      (_eventos: Evento[]) => {
        this.eventos = _eventos,
        this.filteredEventos = this.eventos
      },
      error => console.log(error)
    )
  }
}
