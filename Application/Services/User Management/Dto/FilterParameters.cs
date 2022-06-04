namespace Application.Services.User_Management.Dto
{
    public class FilterParameters
    {
        public string UserName { get; set; } = string.Empty;
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
