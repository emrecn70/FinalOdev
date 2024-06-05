using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using uyg03.Dtos;
using uyg03.Models;

namespace uyg03.Controllers
{
    [Route("api/Questions")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public QuestionsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<QuestionDto> GetList()
        {
            var categories = _context.Questions.ToList();
            var categoryDtos = _mapper.Map<List<QuestionDto>>(categories);
            return categoryDtos;
        }


        [HttpGet]
        [Route("{id}")]
        public QuestionDto Get(int id)
        {
            var category = _context.Questions.Where(s => s.Id == id).SingleOrDefault();
            var categoryDto = _mapper.Map<QuestionDto>(category);
            return categoryDto;
        }
        [HttpGet]
        [Route("{id}/Questions")]
        public List<AnswerDto> GetQuestions(int id)
        {
            var answers = _context.Questions.Where(s => s.QuestionId == id).ToList();
            var answerDtos = _mapper.Map<List<AnswerDto>>(answers);
            return answerDtos;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ResultDto Post(QuestionDto dto)
        {
            if (_context.Questions.Count(c => c.Content == dto.Content) > 0)
            {
                result.Status = false;
                result.Message = "Girilen Soru Kayıtlıdır!";
                return result;
            }
            var category = _mapper.Map<Question>(dto);
            category.Updated = DateTime.Now;
            category.Created = DateTime.Now;
            _context.Questions.Add(category);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Soru Eklendi";
            return result;
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ResultDto Put(QuestionDto dto)
        {
            var category = _context.Questions.Where(s => s.Id == dto.Id).SingleOrDefault();
            if (category == null)
            {
                result.Status = false;
                result.Message = "Soru Bulunamadı!";
                return result;
            }
            category.Content = dto.Content;
            category.IsActive = dto.IsActive;
            category.Updated = DateTime.Now;

            _context.Questions.Update(category);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Soru Düzenlendi";
            return result;
        }


        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public ResultDto Delete(int id)
        {
            var category = _context.Questions.Where(s => s.Id == id).SingleOrDefault();
            if (category == null)
            {
                result.Status = false;
                result.Message = "Soru Bulunamadı!";
                return result;
            }
            _context.Questions.Remove(category);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Soru Silindi";
            return result;
        }
    }
}
