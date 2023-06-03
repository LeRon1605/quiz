using Contracts.QuizSessions;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/sessions")]
    public class QuizSessionController: QuizBaseController
    {
        private readonly IQuizSessionService _quizSessionService;

        public QuizSessionController(IQuizSessionService quizSessionService)
        {
            _quizSessionService = quizSessionService;   
        }

        [HttpPost]
        public async Task<IActionResult> CreateSessionAsync()
        {
            var session = await _quizSessionService.CreateSessionAsync(new QuizSessionCreateDto()
            {
                UserId = GetCurrentUser().Id
            });

            return Ok(session);
        }

        [HttpGet("{id}/result")]
        public async Task<IActionResult> GetSessionResultAsync(Guid id)
        {
            var result = await _quizSessionService.GetResultOfQuizSessionAsync(id);
            return Ok(result);
        }
    }
}
