using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/questions")]
    public class QuestionController: QuizBaseController
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestionsAsync()
        {
            var questions = await _questionService.GetAllAsync();
            return Ok(questions);
        }
    }
}
