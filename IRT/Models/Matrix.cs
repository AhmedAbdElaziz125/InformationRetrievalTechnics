namespace IRT.Models
{
    public class Matrix
    {
        public List<string> DocumentsS { get; set; } = new List<string>();
        public List<Document> Documents { get; set; } = new List<Document>();
        public List<Term> Terms { get; set; } = new List<Term>();
        public List<string> TermsS { get; set; } = new List<string>();
    }
}
