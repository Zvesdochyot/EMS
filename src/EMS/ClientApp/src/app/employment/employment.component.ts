import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DataSource } from '@angular/cdk/collections';
import {Observable, ReplaySubject} from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EmploymentService } from './employment.service';
import { Employee } from './employee';
import { Position } from './position';

import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';

@Component({
  selector: 'app-employment',
  animations: [
    trigger('openClose', [
      state('open', style({
        height: '*',
        opacity: 1,
      })),
      state('closed', style({
        height: '0',
        opacity: 0,
      })),
      transition('open => closed', [
        animate('1s')
      ]),
      transition('closed => open', [
        animate('1s')
      ]),
    ]),
  ],
  templateUrl: './employment.component.html',
  styleUrls: ['./employment.component.css'],
  providers: [EmploymentService],
})
export class EmploymentComponent implements OnInit {
  isEmploymentFormOpened: boolean = true;
  isPositionFormOpened: boolean = true;

  employeeToCreate: Employee = new Employee('', '', '', null!, new Date(), null!);
  receivedEmployee: Employee | undefined;

  positionToCreate: Position = new Position('', '');
  receivedPosition: Position | undefined;

  employees: Employee[] = [];
  positions: Position[] = [];

  dataSource: any | undefined;
  displayedColumns: string[] = ['firstName', 'lastName', 'position', 'salary', 'hired', 'fired'];
  isLoading: boolean = false;

  employeeForm = new FormGroup({
    firstNameControl: new FormControl(this.employeeToCreate.firstName, [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(32),
    ]),
    lastNameControl: new FormControl(this.employeeToCreate.lastName, [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(32),
    ]),
    positionControl: new FormControl(
      this.employeeToCreate.position,
      Validators.required
    ),
    salaryControl: new FormControl(this.employeeToCreate.salary, [
      Validators.required,
      Validators.max(32767),
    ]),
    dateForm: new FormGroup({
      startDateControl: new FormControl(
        this.employeeToCreate.hired,
        Validators.required
      ),
      endDateControl: new FormControl(
        this.employeeToCreate.fired,
        Validators.nullValidator
      ),
    }),
  });

  positionForm = new FormGroup({
    titleControl: new FormControl(this.positionToCreate.title, [
      Validators.required,
      Validators.minLength(4),
      Validators.maxLength(64),
    ]),
    descriptionControl: new FormControl(
      this.positionToCreate.description,
      Validators.nullValidator
    ),
  });

  constructor(private employmentService: EmploymentService, private snackBar: MatSnackBar) {}

  ngOnInit() {
    this.readEmployees();
    this.readPositions();
  }

  toggleEmployment() {
    this.isEmploymentFormOpened = !this.isEmploymentFormOpened;
  }

  togglePosition() {
    this.isPositionFormOpened = !this.isPositionFormOpened;
  }

  addEmployeeData() {
    this.employees?.push(this.receivedEmployee!);
    this.dataSource?.setData(this.employees);
  }

  removeEmployeeData() {
    this.employees = this.employees.slice(0, -1);
    this.dataSource?.setData(this.employees);
  }

  submitEmployee(employee: Employee) {
    this.employmentService.createEmployee(employee).subscribe(
      (data: any) => {
        this.receivedEmployee = data;
      },
      (error) => {
        console.log(error);
        this.snackBar.open(
          'Internal server error, please try again later ⛔️', 'Close', {
            duration: 3000,
          }
        );
      },
      () => {
        this.addEmployeeData()
        this.employeeToCreate.firstName = this.employeeToCreate.lastName = this.employeeToCreate.position = '';
        this.employeeToCreate.salary = null!;
        this.employeeToCreate.hired = new Date();
        this.employeeToCreate.fired = null!;
        this.snackBar.open('Employee was added successfully! ✅', 'Got it!', {
          duration: 3000,
        });
      }
    );
  }

  submitPosition(position: Position) {
    this.employmentService.createPosition(position).subscribe(
      (data: any) => {
        this.receivedPosition = data;
      },
      (error) => {
        console.log(error);
        this.snackBar.open(
          'Internal server error, please try again later ⛔️', 'Close', {
            duration: 3000,
          }
        );
      },
      () => {
        this.positions?.push(Object.assign({}, position));
        this.positionToCreate.title = this.positionToCreate.description = '';
        this.snackBar.open('Position was added successfully! ✅', 'Got it!', {
          duration: 3000,
        });
      }
    );
  }

  readEmployees() {
    this.isLoading = true;
    this.employmentService.getEmployees().subscribe(
      (data: Employee[]) => {
        this.employees = data;
        this.dataSource = new EmployeesDataSource(data);
      },
      (error) => {
        console.log(error);
        this.snackBar.open('Internal server error, please try again later ⛔️', 'Close', {
          duration: 3000,
        });
        this.isLoading = false;
      },
      () => {
        this.isLoading = false;
      }
    );
  }

  readPositions() {
    this.employmentService.getPositions().subscribe(
      (data: Position[]) => {
        this.positions = data;
      },
      (error) => {
        console.log(error);
        this.snackBar.open('Internal server error, please try again later ⛔️', 'Close', {
          duration: 3000,
        });
      }
    );
  }
}

class EmployeesDataSource extends DataSource<Employee> {
  private _dataStream = new ReplaySubject<Employee[]>();

  constructor(initialData: Employee[]) {
    super();
    this.setData(initialData);
  }

  connect(): Observable<Employee[]> {
    return this._dataStream;
  }

  disconnect() {}

  setData(data: Employee[]) {
    this._dataStream.next(data);
  }
}
