import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { submitQuizDto } from '../shared/_models/submitQuizDto';

@Injectable({
  providedIn: 'root',
})
export class QuizzesService {
  gameMode = new Subject<string>();

  constructor(private http: HttpClient) {}

  getQuizzes(id: string) {
    return this.http.get('https://localhost:5001/api/Quiz?gamemode=' + id);
  }

  getSelectedQuiz(quizId: string) {
    return this.http.get('https://localhost:5001/api/Quiz/' + quizId);
  }

  submitQuiz(submitDto: submitQuizDto) {
    return this.http.post('https://localhost:5001/api/Quiz/SubmitQuiz', {
      userName: submitDto.userName,
      quizId: submitDto.quizId,
      quizAnswers: submitDto.questionResult,
    });
  }

  checkIfQuizCompleted(username: string, quizId: number) {
    return this.http.get(
      'https://localhost:5001/api/Quiz/CheckIfCompleted?userName=' +
        username +
        '&quizId=' +
        quizId
    );
  }
}
