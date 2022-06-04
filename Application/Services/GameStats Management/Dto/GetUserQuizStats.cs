namespace Application.Services.GameStats_Management.Dto
{
    public class GetUserQuizStats
    {
        public string Question { get; set; }
        public string Answered { get; set; }
        public bool IsCorrect { get; set; }
    }
}
