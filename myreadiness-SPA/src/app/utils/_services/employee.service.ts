import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../_models/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.baseUrl + 'employees');
  }

  getEmployeesNotInUsers(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.baseUrl + 'employees/employeesnotinusers');
  }

  getEmployee(id): Observable<Employee> {
    return this.http.get<Employee>(this.baseUrl + 'employees/' + id);
  }

  getEmployeeByFingerID(fingerid): Observable<Employee> {
    return this.http.get<Employee>(this.baseUrl + 'employees/searchbyfinger/' + fingerid);
  }

  getEmployeeByEmployeeName(name): Observable<Employee> {
    return this.http.get<Employee>(this.baseUrl + 'employees/searchbyname/' + name);
  }

  getReportingEmployees(postid): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.baseUrl + 'employees/reportingemployees/' + postid);
  }
}
