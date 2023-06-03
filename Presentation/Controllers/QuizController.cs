using Contracts.Answers;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/quizzes")]
    public class QuizController: QuizBaseController
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost("{questionId}")]
        public async Task<IActionResult> SubmitAnswer(Guid questionId, AnswerQuizDto answerQuizDto)
        {
            var result = await _quizService.SubmitAnswerAsync(questionId, answerQuizDto);
            return Ok(result);
        }
    }
}
