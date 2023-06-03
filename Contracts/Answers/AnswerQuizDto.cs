using System;

namespace Contracts.Answers
{
    public class AnswerQuizDto
    {
        public Guid AnswerId { get; set; }
        public Guid QuizSessionId { get; set; }
    }
}
