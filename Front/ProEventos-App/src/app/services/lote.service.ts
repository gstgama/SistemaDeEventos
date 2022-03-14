import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lote } from '@app/models/Lote';
import { take } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';

@Injectable()
export class LoteService {

  baseURL = 'https://localhost:5001/api/lotes';

  constructor(private http: HttpClient) { }

  public getLotesByEventosId(eventoId: number): Observable<Lote[]> {
    return this.http
      .get<Lote[]>(`${this.baseURL}/${eventoId}`)
      .pipe(take(1));
  }

  public getLoteByIds(eventoId: number, loteId: number): Observable<Lote> {
    return this.http
      .get<Lote>(`${this.baseURL}/${eventoId}/${loteId}`)
      .pipe(take(1));
  }

  public saveLotes(eventoId: number, lotes: Lote[]): Observable<Lote[]> {
    return this.http
      .put<Lote[]>(`${this.baseURL}/${eventoId}`, lotes)
      .pipe(take(1));
  }

  public deleteLote(eventoId: number, loteId: number): Observable<any> {
    return this.http
      .delete<any>(`${this.baseURL}/${eventoId}/${loteId}`)
      .pipe(take(1));
  }

}
