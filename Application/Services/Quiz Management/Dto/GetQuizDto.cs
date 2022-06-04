namespace Application.Services.Quiz_Management.Dto
{
    public class GetQuizDto
    {
        public GetQuizDto()
        {
            MultipleChoiceFormat = new List<MultipleChoiceFormatQuizDto>();
            BinaryFormat = new List<BinaryFormatQuestionDto>();
        }

        public List<MultipleChoiceFormatQuizDto> MultipleChoiceFormat { get; set; }
        public List<BinaryFormatQuestionDto> BinaryFormat { get; set; }
    }
}
