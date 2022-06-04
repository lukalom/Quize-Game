using Application.Services.GameStats_Management.Dto;
using Application.Services.Quiz_Management.Dto;
using Application.Services.Quote_Management.Dto;
using Infrastructure.Entities;
using Infrastructure.Enums;
using Infrastructure.IConfiguration;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;

namespace Application.Services.Quiz_Management
{
    public class QuizService : IQuizService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuizService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> CreateQuiz(CreateQuizDto requestDto)
        {

            var quizModel = await _unitOfWork.Quiz
                .Query(x => x.Title == requestDto.Title)
                .FirstOrDefaultAsync();

            if (quizModel != null)
            {
                throw new AppException($"Quiz With this title {requestDto.Title} already exists");
            }

            var quiz = new Quiz
            {
                GameMode = requestDto.Mode,
                Title = requestDto.Title
            };

            await _unitOfWork.Quiz.AddAsync(quiz);
            await _unitOfWork.SaveAsync();
            return "Quiz Added Successfully";
        }

        public async Task<List<GetQuizzesByGameModeDto>> GetQuizzesByGameMode(QuizGameMode mode)
        {

            var quizzes = await _unitOfWork.Quiz
                .Query(x => x.GameMode == mode).ToListAsync();

            var response = quizzes.Select(quiz =>
                new GetQuizzesByGameModeDto() { QuizId = quiz.Id, QuizName = quiz.Title }).ToList();

            return response;
        }

        public async Task<GetQuizDto> GetQuiz(int quizId)
        {
            var random = new Random();
            var quiz = await _unitOfWork.Quiz
                .Query(x => x.Id == quizId)
                .FirstOrDefaultAsync();

            if (quiz == null)
            {
                throw new AppException("Invalid Quiz");
            }

            var query = _unitOfWork.QuizQuot.Query(x => x.QuizId == quizId)
                .Include(x => x.Quote);
            var response = new GetQuizDto();

            if (quiz.GameMode != null && quiz.GameMode == QuizGameMode.Binary)
            {
                var quizQuotes = await query.ToListAsync();
                var binaryFormatList = new List<BinaryFormatQuestionDto>();
                foreach (var quizQuot in quizQuotes.Where(quizQuot => quizQuot.Quote != null))
                {
                    var index = random.Next(0, quizQuotes.Count);
                    var binaryFormat = new BinaryFormatQuestionDto
                    {
                        QuoteId = quizQuot.Quote.Id,
                        Quote = quizQuot.Quote.Description,
                        CorrectAuthor = quizQuot.Quote.Author,
                        Author = quizQuotes[index].Quote.Author
                    };

                    binaryFormatList.Add(binaryFormat);
                }

                response.BinaryFormat = binaryFormatList;
                return response;
            }

            var quizQuotList = await query.ToListAsync();
            var multipleChoiceFormatList = new List<MultipleChoiceFormatQuizDto>();
            foreach (var quizQuot in quizQuotList.Where(quizQuot => quizQuot.Quote != null))
            {

                var multipleChoiceFormat = new MultipleChoiceFormatQuizDto()
                {
                    Quote = quizQuot.Quote.Description,
                    CorrectAuthor = quizQuot.Quote.Author,
                    QuoteId = quizQuot.QuoteId
                };
                var i = 0;
                while (multipleChoiceFormat.IncorrectAuthors.Count != 2)
                {
                    var index = random.Next(0, quizQuotList.Count);
                    var author = quizQuotList[index].Quote.Author;
                    if (!multipleChoiceFormat.IncorrectAuthors.Contains(author)
                        && multipleChoiceFormat.CorrectAuthor != author)
                    {
                        multipleChoiceFormat.IncorrectAuthors.Add(author);
                    }
                }

                multipleChoiceFormatList.Add(multipleChoiceFormat);
            }

            response.MultipleChoiceFormat = multipleChoiceFormatList;
            return response;
        }

        public async Task<string> AddQuotInQuiz(CreateQuizQuestionDto requestDto)
        {
            if (requestDto.QuotesIdList.Distinct().Count() != requestDto.QuotesIdList.Count())
            {
                throw new AppException("You Cannot add same questions");
            }

            var quiz = await _unitOfWork.Quiz
                .Query(x => x.IsDisabled == false && x.Id == requestDto.QuizId)
                .FirstOrDefaultAsync();

            if (quiz == null)
            {
                throw new AppException("Invalid Quiz");
            }

            var quizQuot = new List<QuizQuot>();
            foreach (var quoteId in requestDto.QuotesIdList)
            {
                var quote = await _unitOfWork.Quote
                    .Query(x => x.Id == quoteId && x.IsDisabled == false)
                    .FirstOrDefaultAsync();

                if (quote == null)
                {
                    throw new AppException("Invalid Quote");
                }

                quizQuot.Add(new QuizQuot
                {
                    QuoteId = quote.Id,
                    QuizId = quiz.Id
                });
            }

            await _unitOfWork.QuizQuot.AddRangeAsync(quizQuot);
            await _unitOfWork.SaveAsync();
            return "Question added successfully in quiz";
        }

        public async Task<bool> CheckAnswer(CheckAnswerDto requestDto)
        {
            var quiz = await _unitOfWork.Quiz
                .Query(x => x.Id == requestDto.QuizId && x.IsDisabled == false)
                .FirstOrDefaultAsync();

            if (quiz == null)
            {
                throw new AppException("Invalid Quiz Id");
            }

            var question = await _unitOfWork.QuizQuot
                .Query(x =>
                    x.QuizId == quiz.Id
                    && x.QuoteId == requestDto.QuoteId)
                .Include(x => x.Quote)
                .FirstOrDefaultAsync();

            if (question.Quote.Author.ToLower() == requestDto.Author.ToLower() && requestDto.IsCorrect)
            {
                return true;
            }

            return question.Quote.Author.ToLower() != requestDto.Author.ToLower() && !requestDto.IsCorrect;
        }

        public async Task<string> SubmitQuiz(SubmitQuizDto requestDto)
        {
            var user = await _unitOfWork.User.Query(
                    x => x.UserName.ToLower() == requestDto.UserName!.ToLower())
                .FirstOrDefaultAsync();

            if (user == null) throw new AppException("Invalid user");

            var quiz = await _unitOfWork.Quiz
                .Query(x => x.Id == requestDto.QuizId)
                .FirstOrDefaultAsync();

            if (quiz == null)
            {
                throw new AppException("Invalid Quiz Id");
            }

            var gameStats = new List<GameStats>();
            foreach (var quizAnswer in requestDto.QuizAnswers)
            {
                var question = await _unitOfWork.QuizQuot
                    .Query(x =>
                        x.QuizId == quiz.Id
                        && x.QuoteId == quizAnswer.QuoteId)
                    .Include(x => x.Quote)
                    .FirstOrDefaultAsync();

                var gameStat = await _unitOfWork.GameStats
                    .Query(x =>
                                x.QuizId == quiz.Id
                                && x.UserId == user.Id
                                && x.QuotId == question!.QuoteId)
                                .FirstOrDefaultAsync();

                if (gameStat != null) throw new AppException("You Already answered this question");

                gameStats.Add(new GameStats()
                {
                    Answer = quizAnswer.Author,
                    QuizId = quiz.Id,
                    QuotId = quizAnswer.QuoteId,
                    UserId = user.Id,
                    IsCorrect = quizAnswer.IsCorrect
                });
            }

            await _unitOfWork.GameStats.AddRangeAsync(gameStats);
            await _unitOfWork.SaveAsync();
            return "Quiz Submitted Successfully";
        }

        public async Task<bool> CheckIfUserCompletedQuiz(string username, int quizId)
        {
            var user = await _unitOfWork.User.Query(x =>
                    x.UserName.ToLower() == username.ToLower())
                .FirstOrDefaultAsync();
            if (user == null) throw new AppException($"this username does not exists = {username}");


            var gameStat = await _unitOfWork.GameStats.Query(x =>
                    x.UserId == user.Id
                    && x.QuizId == quizId)
                .FirstOrDefaultAsync();

            return gameStat != null;
        }
    }
}
