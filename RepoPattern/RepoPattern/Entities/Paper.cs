namespace RepoPattern.Entities
{
    public class Paper : Entity
    {
        public string Publisher { get; set; }
        public string Title { get; set; }
        public int PaperId { get; set; }
        public DateTime Created { get; set; }
    }
}
