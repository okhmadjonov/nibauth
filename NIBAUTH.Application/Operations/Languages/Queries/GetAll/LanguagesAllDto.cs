namespace NIBAUTH.Application.Operations.Languages.Queries.GetAll
{
    public class LanguagesAllDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Flag { get; set; }
        public bool? IsDefault { get; set; }
    }
}
