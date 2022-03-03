import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
    ) { }

  public ngOnInit(): void {
    this.spinner.show();
    this.carregarEventos();
  }

  modalRef?: BsModalRef;

  public widthImg = 150;
  public marginImg = 2;
  public showImg = false;

  public eventos: Evento[] = [];
  public filteredEventos: Evento[] = [];
  private filtroListado = ''
  public eventoId: number = 0;
  public eventoTema: string = ''

  public get filterList(): string{
    return this.filtroListado;
  }

  public set filterList(value: string){
    this.filtroListado = value;
    this.filteredEventos = this.filterList ? this.filterEventos(this.filterList) : this.eventos;
  }

  public filterEventos(filterBy: string): Evento[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: { tema: string; local: string}) => evento.tema.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  public carregarEventos(){
    this.eventoService.getEventos().subscribe({
      next: (eventos: Evento[]) => {
        this.eventos = eventos,
        this.filteredEventos = this.eventos
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregador Eventos', 'Erro!');
      },
      complete: () => this.spinner.hide()
    })
  }

  openModal(template: TemplateRef<any>, event: any, eventoId: number, eventoTema: string): void {
    event.stopPropagation();
    this.eventoId = eventoId;
    this.eventoTema = eventoTema;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.eventoService.deleteEvento(this.eventoId).subscribe(
      (data: any)=> {
        if (data.message === 'Deletado'){
          //console.log(data);
          this.spinner.hide();
          this.toastr.success(`${this.eventoTema} (${this.eventoId}) deletado com sucesso`, 'Deletado!');
          this.carregarEventos();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar ${this.eventoTema} (${this.eventoId})`);
        this.spinner.hide();
      },
      () => this.spinner.hide()
      )
    }

  decline(): void {
    this.modalRef?.hide();
  }

  detalheEvento(id: number): void{
    this.router.navigate([`eventos/detalhe/${id}`]);
  }

}
