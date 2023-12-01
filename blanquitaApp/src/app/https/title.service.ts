import { Injectable } from '@angular/core';
import { BehaviorSubject , Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TitleService {
  private titleSubject = new BehaviorSubject<string>('');
  public title$ = this.titleSubject.asObservable();

  constructor() { }

  public changeTitle(title: string) {
    this.titleSubject.next(title);
  }

}