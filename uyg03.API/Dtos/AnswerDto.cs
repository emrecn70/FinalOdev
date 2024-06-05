using uyg03.Models;

namespace uyg03.Dtos
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string Content { get; set; }


        public int QuestionId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        
    }
}
