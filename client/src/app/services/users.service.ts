import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  constructor(private http: HttpClient) {}

  checkUser(userName: string) {
    return this.http.get('https://localhost:5001/api/User/' + userName);
  }

  createUser(userName: string) {
    var formData: any = new FormData();
    formData.append('userName', userName);
    return this.http.post(
      'https://localhost:5001/api/User?userName=' + userName,
      formData
    );
  }

  userStatistics(userName: string) {
    return this.http.get('https://localhost:5001/api/GameStats/' + userName);
  }
}
