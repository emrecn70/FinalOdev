using Microsoft.AspNetCore.Http.HttpResults;

namespace uyg03.Models

{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public List<Answer> Answers { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int QuestionId { get; internal set; }
    }
}
