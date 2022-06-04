namespace Application.Services.GameStats_Management.Dto
{
    public class GetUserGeneralStatsDto
    {
        public GetUserGeneralStatsDto()
        {
            QuizzesTaken = new List<string>();
        }

        public int TotalQuestions { get; set; }
        public int Answers { get; set; }
        public List<string> QuizzesTaken { get; set; }
        public string AnswerRate { get; set; }
    }
}
