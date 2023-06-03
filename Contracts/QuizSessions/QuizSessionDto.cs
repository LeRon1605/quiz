using System;

namespace Contracts.QuizSessions
{
    public class QuizSessionDto
    {
        public Guid Id { get; set; }
        public DateTime StartAt { get; set; }
    }
}
