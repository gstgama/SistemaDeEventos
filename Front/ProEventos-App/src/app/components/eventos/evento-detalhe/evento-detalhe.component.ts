import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { EventoService } from '@app/services/evento.service';
import { Evento } from '@app/models/Evento';
import { Lote } from '@app/models/Lote';
import { LoteService } from '@app/services/lote.service';


@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private localeService: BsLocaleService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private eventoService: EventoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private loteService: LoteService) {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.carregarEvento();
    this.validation();
  }

  public eventoId: number;
  public form!: FormGroup;
  public evento = {} as Evento;
  public estadoSalvar: string = 'post';

  get modoSalvar(): boolean {
    return this.estadoSalvar === 'put';
  }

  get lotes(): FormArray {
    return this.form.get('lotes') as FormArray;
  }

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false
    }
  }

  carregarEvento() {
    this.spinner.show();
    this.eventoId = +this.activatedRoute.snapshot.paramMap.get('id');

    if (this.eventoId !== null || this.eventoId === 0) {
      this.estadoSalvar = 'put';
      this.eventoService.getEventoById(this.eventoId).subscribe(
        (evento: Evento) => {
          this.evento = { ...evento };
          this.form.patchValue(this.evento);
          this.evento.lotes.forEach(lote => {
            this.lotes.push(this.criarLote(lote));
          })
          // this.carregarLotes();
        },
        (error: any) => {
          console.log(error);
          this.toastr.error('Não foi possivel carregar o evento.', 'Erro!')
          this.spinner.hide();
        },
        () => this.spinner.hide()
      );
    }
    else {
      this.spinner.hide();
    }
  }

  carregarLotes(): void {
    this.loteService.getLotesByEventosId(this.eventoId).subscribe(
      (lotesRetorno: Lote[]) => {
        lotesRetorno.forEach(lote => {
          this.lotes.push(this.criarLote(lote));
        })
      },
      (error: any) => {
        this.toastr.error('Erro ao tentar carregar lotes.', 'Erro');
        console.error(error);
      }
    )
  }

  public validation(): void {
    this.form = this.fb.group({
      tema: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50),
        ],
      ],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemURL: [''],
      lotes: this.fb.array([]),
    });
  }

  adicionarLote(): void {
    this.lotes.push(this.criarLote({ id: 0 } as Lote));
  }

  criarLote(lote: Lote): FormGroup {
    return this.fb.group({
      id: [lote.id],
      nome: [lote.nome, Validators.required],
      quantidade: [lote.quantidade, Validators.required],
      preco: [lote.preco, Validators.required],
      dataInicio: [lote.dataInicio, Validators.required],
      dataFim: [lote.dataFim, Validators.required],
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  cssValidator(campForm: FormControl | AbstractControl): any {
    return { 'is-invalid': campForm.errors && campForm.touched }
  }

  salvarEvento(): void {
    this.spinner.show();

    if (this.form.valid) {
      this.evento = (this.estadoSalvar === 'post')
        ? { ... this.form.value }
        : { id: this.evento.id, ... this.form.value };

      this.eventoService[this.estadoSalvar](this.evento).subscribe(
        (eventoRetorno: Evento) => {
          this.toastr.success('Evento salvo com sucesso.', 'Sucesso');
          this.router.navigate([`eventos/detalhe/${eventoRetorno.id}`]);
        },
        (error: any) => {
          console.error(error);
          this.toastr.error('Erro ao salvar evento!', 'Erro');
        }
      ).add(() => this.spinner.hide());
    }
  }

  salvarLotes(): void {
    this.spinner.show();

    if (this.form.controls['lotes'].valid) {
      this.loteService.saveLotes(this.eventoId, this.form.value.lotes)
        .subscribe(
          (lotes: Lote[]) => {
            this.toastr.success('Lotes salvos com sucesso.', 'Sucesso!');
          },
          (error: any) => {
            this.toastr.error('Erro ao tentar salvar lotes', 'Erro');
            console.log(error);
          }
        ).add(() => this.spinner.hide());

      this.spinner.hide();
    }
  }

}
