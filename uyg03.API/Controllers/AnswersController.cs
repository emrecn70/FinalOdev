using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using uyg03.Dtos;
using uyg03.Models;

namespace uyg03.Controllers
{
    [Route("api/Answers")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public AnswersController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<AnswerDto> GetList()
        {
            var answers = _context.Answers.ToList();
            var answerDtos = _mapper.Map<List<AnswerDto>>(answers);
            return answerDtos;
        }


        [HttpGet]
        [Route("{id}")]
        public AnswerDto Get(int id)
        {
            var answer = _context.Answers.Where(s => s.Id == id).SingleOrDefault();
            var answerDto = _mapper.Map<AnswerDto>(answer);
            return answerDto;
        }

        [HttpPost]
        public ResultDto Post(AnswerDto dto)
        {
            if (_context.Answers.Count(c => c.Content == dto.Content) > 0)
            {
                result.Status = false;
                result.Message = "Girilen Cevap Kayıtlıdır!";
                return result;
            }
            var answer = _mapper.Map<Answer>(dto);
            answer.Updated = DateTime.Now;
            answer.Created = DateTime.Now;
            _context.Answers.Add(answer);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Cevap Eklendi";
            return result;
        }


        [HttpPut]
        public ResultDto Put(AnswerDto dto)
        {
            var answer = _context.Answers.Where(s => s.Id == dto.Id).SingleOrDefault();
            if (answer == null)
            {
                result.Status = false;
                result.Message = "Cevap Bulunamadı!";
                return result;
            }
            answer.Content = dto.Content;
            answer.Updated = DateTime.Now;
            answer.QuestionId = dto.QuestionId;
            _context.Answers.Update(answer);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Cevap Düzenlendi";
            return result;
        }

        
        [HttpDelete]
        [Route("{id}")]
        public ResultDto Delete(int id)
        {
            var answer = _context.Answers.Where(s => s.Id == id).SingleOrDefault();
            if (answer == null)
            {
                result.Status = false;
                result.Message = "Cevap Bulunamadı!";
                return result;
            }
            _context.Answers.Remove(answer);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Cevap Silindi";
            return result;
        }
    }
}
