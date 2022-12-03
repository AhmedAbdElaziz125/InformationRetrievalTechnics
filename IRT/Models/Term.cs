namespace IRT.Models
{
    public class Term
    {
        public string? id { get; set; }
        public int? termFrequency { get; set; } = 0;
        public List<Document> postingList { get; set; } = new List<Document>();
    }
}
