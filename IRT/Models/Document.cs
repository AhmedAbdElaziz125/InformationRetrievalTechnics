namespace IRT.Models
{
    public class Document
    {
        public string? id { get; set; }
        public List<Term> documentTerms { get; set; } = new List<Term>();
    }
}
