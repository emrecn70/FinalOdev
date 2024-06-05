namespace uyg03.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
