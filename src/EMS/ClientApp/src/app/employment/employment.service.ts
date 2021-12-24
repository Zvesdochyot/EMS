import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from './employee';
import { Position } from './position';

@Injectable()
export class EmploymentService {
  url: string;

  constructor(private httpClient: HttpClient) {
    this.url = 'http://localhost:4200';
  }

  public createEmployee(employee: Employee) {
    const body = {
      firstName: employee.firstName,
      lastName: employee.lastName,
      position: employee.position,
      salary: employee.salary,
      hired: employee.hired,
      fired: employee.fired,
    };
    return this.httpClient.post(this.url + '/api/v1/employees/', body);
  }

  public createPosition(position: Position) {
    const body = { title: position.title, description: position.description };
    return this.httpClient.post(this.url + '/api/v1/positions/', body);
  }

  public getEmployees(): Observable<Employee[]> {
    return this.httpClient.get<Employee[]>(this.url + '/api/v1/employees/')
  }

  public getPositions(): Observable<Position[]> {
    return this.httpClient.get<Position[]>(this.url + '/api/v1/positions/')
  }
}
