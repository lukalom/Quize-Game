import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { QuizzesService } from 'src/app/services/quizzes.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  title = 'Famous Quote Quiz Game';

  constructor(
    private quizService:QuizzesService) {}


  ngOnInit(): void {
  
  }


}
