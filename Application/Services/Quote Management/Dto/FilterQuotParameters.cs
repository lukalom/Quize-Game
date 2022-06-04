namespace Application.Services.Quote_Management.Dto
{
    public class FilterQuotParameters
    {
        public string Author { get; set; } = string.Empty;
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
