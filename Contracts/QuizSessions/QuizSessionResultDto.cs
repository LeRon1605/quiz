using Contracts.Answers;
using System;
using System.Collections.Generic;

namespace Contracts.QuizSessions
{
    public class QuizSessionResultDto
    {
        public Guid Id { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public bool IsPassed { get; set; }
        public int TotalCorrectAnswers { get; set; }
        public int TotalIncorrectAnswers { get; set; }
        public List<AnswerQuizResultDto> Details { get; set; }
    }
}
