import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
})
export class UsersComponent implements OnInit {
  userStats: any = [];
  constructor(private userService: UsersService, 
    private _alert: MatSnackBar,
    private router: Router) {}

  ngOnInit(): void {}

  searchUser(form: any) {
    this.userService.userStatistics(form.value.userName).subscribe(
      (data) => {
        this.userStats = data;
      },
      (err) => {
        this._alert.open(`Error ${err.error.message}`, 'Close', {
          duration: 3000,
          panelClass: ['red-snackbar', 'login-snackbar'],
        });
      }
    );
  }

  createUser(createform: any) {
    this.userService.createUser(createform.value.userName).subscribe(
      (data): void => {
        this._alert.open(`User ${createform.value.userName} created`, 'Close', {
          duration: 3000,
          panelClass: ['green-snackbar', 'login-snackbar'],
        });
      },
      (err): void => {
        if (err.status === 200) {
          this._alert.open(
            `User ${createform.value.userName} created`,
            'Close',
            {
              duration: 3000,
              panelClass: ['green-snackbar', 'login-snackbar'],
            }
          );

          this.router.navigate(['/quizzes']);
        } else {
          this._alert.open(`Error ${err.error.message}`, 'Close', {
            duration: 3000,
            panelClass: ['red-snackbar', 'login-snackbar'],
          });
        }
      }
    );
  }
}
