namespace Application.Services.Quiz_Management.Dto
{
    public class MultipleChoiceFormatQuizDto
    {
        public MultipleChoiceFormatQuizDto()
        {
            IncorrectAuthors = new List<string>();
        }

        public int QuoteId { get; set; }
        public string Quote { get; set; }
        public string CorrectAuthor { get; set; }
        public List<string> IncorrectAuthors { get; set; }

    }
}
