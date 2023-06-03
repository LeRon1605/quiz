using Contracts.QuizSessions;
using System;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IQuizSessionService
    {
        Task<QuizSessionResultDto> GetResultOfQuizSessionAsync(Guid quizSessionId);
        Task<QuizSessionDto> CreateSessionAsync(QuizSessionCreateDto quizSessionCreateDto);
    }
}
