import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { QuizzesService } from 'src/app/services/quizzes.service';
import { UsersService } from 'src/app/services/users.service';
import { result } from 'src/app/shared/_models/question-result';
import { submitQuizDto } from 'src/app/shared/_models/submitQuizDto';

@Component({
  selector: 'app-quizzes',
  templateUrl: './quizzes.component.html',
  styleUrls: ['./quizzes.component.css'],
})
export class QuizzesComponent implements OnInit {
  quizzes: any = [];
  gameMode = '0';
  userClick: boolean = false;
  showQuestion: boolean = false;

  currentUserName: string;
  currentQuizId: string;
  currentQuotId: string;
  currentQuotAnswer: string;
  isCorrect: boolean;

  binaryFormat: any = [];
  multipleFormat: any = [];

  currentQuestion = 0;
  isNext: boolean = false;
  endQuiz: boolean = false;
  totalQuestions = 0;

  resultArr: result[] = [];
  submitQuizDto: submitQuizDto;

  constructor(
    private quizService: QuizzesService,
    private _alert: MatSnackBar,
    private userService: UsersService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.binary();
  }
  showForm(quizId: string) {
    this.userClick = !this.userClick;
    this.currentQuizId = quizId;
  }
  
  onUserSubmit(form: any) {
    this.userService.checkUser(form.value.userName).subscribe((data) => {
      if (data == true) {
        this.currentUserName = form.value.userName;
        this.showQuestion = true;
        this.checkIfQuizCompleted();
        this.userClick = false;
      } else {
        this._alert.open('Invalid UserName', 'Close', {
          duration: 3000,
          panelClass: ['red-snackbar', 'login-snackbar'],
        });
        //Redirect To create User
        this.router.navigate(['']);
      }
    }),
      (err) => console.log(err);
  }

  checkIfQuizCompleted() {
    this.quizService
      .checkIfQuizCompleted(this.currentUserName, +this.currentQuizId)
      .subscribe(
        (data) => {
          if (data === true) {
            this._alert.open(`Quiz Completed`, 'Close', {
              duration: 3000,
              panelClass: ['green-snackbar', 'login-snackbar'],
            });
          } else {
            this.getQuizes();
          }
        },
        (err) => {
          console.log(err.error);
          this.router.navigate(['']);
        }
      );
  }

  getQuizes() {
    this.quizService
      .getSelectedQuiz(this.currentQuizId)
      .subscribe((data: any) => {
        if (data.binaryFormat.length !== 0) {
          this.binaryFormat = data.binaryFormat;
          this.totalQuestions = this.binaryFormat.length;
        }
        if (data.multipleChoiceFormat.length !== 0) {
          this.multipleFormat = data.multipleChoiceFormat;
          this.totalQuestions = this.multipleFormat.length;
        }
      });
  }

  checkAnswer(
    clickedButton: boolean,
    quoteId: string,
    answer: string,
    correctAnswerForBinary: string = null
  ) {
    this.currentQuotId = quoteId;
    this.currentQuotAnswer = answer;

    if (correctAnswerForBinary === null) {
      correctAnswerForBinary = this.currentQuotAnswer;
    }

    if (this.currentQuotAnswer === correctAnswerForBinary && clickedButton) {
      this._alert.open('Correct Answer', 'Close', {
        duration: 3000,
        panelClass: ['green-snackbar', 'login-snackbar'],
      });
      this.isCorrect = true;
    } else if (
      this.currentQuotAnswer !== correctAnswerForBinary &&
      !clickedButton
    ) {
      this._alert.open('Correct Answer', 'Close', {
        duration: 3000,
        panelClass: ['green-snackbar', 'login-snackbar'],
      });
      this.isCorrect = true;
    } else {
      this._alert.open(
        `Incorrect correct answer is ${correctAnswerForBinary}`,
        'Close',
        {
          duration: 3000,
          panelClass: ['red-snackbar', 'login-snackbar'],
        }
      );
      this.isCorrect = false;
    }
    this.isNext = true;

    this.resultArr.push({
      author: this.currentQuotAnswer,
      quoteId: +this.currentQuotId,
      isCorrect: this.isCorrect,
    });

    console.log(this.resultArr);
  }

  goNext() {
    this.isNext = false;
    this.currentQuestion++;
    console.log(this.currentQuestion);
    console.log(this.totalQuestions);
    if (
      this.totalQuestions === this.currentQuestion &&
      this.currentQuestion !== 0
    ) {
      this.endQuiz = true;
    }
  }

  submitQuiz() {
    this.currentQuestion = 0;
    this.submitQuizDto = {
      userName: this.currentUserName,
      quizId: +this.currentQuizId,
      questionResult: this.resultArr,
    };

    this.quizService.submitQuiz(this.submitQuizDto).subscribe(
      (data) => {
        this._alert.open(`${data}`, 'Close', {
          duration: 3000,
          panelClass: ['green-snackbar', 'login-snackbar'],
        });
      },
      (err) => {
        this._alert.open(`Error ${err.errors.message}`, 'Close', {
          duration: 3000,
          panelClass: ['red-snackbar', 'login-snackbar'],
        });
      }
    );

    this._alert.open(`Quiz Submitted Successfully`, 'Close', {
      duration: 3000,
      panelClass: ['green-snackbar', 'login-snackbar'],
    });

    //Redirect Result Page and request user in search
    this.router.navigate(['']);
  }

  binary() {
    this.gameMode = '0';
    this.quizService.getQuizzes(this.gameMode).subscribe((data) => {
      this.quizzes = data;
    });
    this.resetVariables();
  }

  multiple() {
    this.gameMode = '1';
    this.quizService.getQuizzes(this.gameMode).subscribe((data) => {
      this.quizzes = data;
    });
    this.resetVariables();
  }

  resetVariables() {
    this.binaryFormat = [];
    this.multipleFormat = [];
    this.userClick = false;
    this.showQuestion = false;
    this.isNext = false;
    this.currentQuestion = 0;
    this.endQuiz = false;
  }
}
