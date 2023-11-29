import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ComboDTO } from '../dtos/combo-dto';

@Injectable({
  providedIn: 'root'
})
export class ComboService {
  private dataUrl = 'Combo'

  constructor(private http:HttpClient) { }

  public getCombos(): Observable<ComboDTO[]>{
    return this.http.get<ComboDTO[]>(this.dataUrl);
  }
}
