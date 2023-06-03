using Contracts.Answers;
using Contracts.QuizSessions;
using System;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IQuizService
    {
        public Task<AnswerQuizResultDto> SubmitAnswerAsync(Guid questionId, AnswerQuizDto answerQuizDto);
    }
}
