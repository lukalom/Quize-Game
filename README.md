General information - Quiz Game App You can Create Update Delete Get Quotes
generate quiz with quotes and authors.  Depending on the selected mode user will  
choose the correct answer from a list of answers, or simply 
answer with True/False to the question. 
when you finish the test you can see the results and you can see other users' general stats
answer rates etc.
 
the app was built with .Net Core Web Api for backend and frontend Angular.
I'm using Nlayer architecture, separating from each other, UI(API, client) 
business logic(application), Data Access(infrastructure). 
when you run the project Database will be automatically created and all data 
will be seeded for testing purposes. there will be inserted quotes, and quizzes both binary 
and multiple choices.

						Services
1.GameStats Management

method - GetUserGeneralStats(string userName)
where you can see user scoarboard general.

method - GetUserQuizStats(string userName, int quizId)
with this method, you can get data on what questions were provided in the quiz and 
how the user answered.

2.Quiz Management
3.Quote Management
4.User Management
