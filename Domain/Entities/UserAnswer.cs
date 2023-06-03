using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class UserAnswer
    {
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public Guid QuizSessionId { get; set; }
        public QuizSession QuizSession { get; set; }

        public bool IsCorrect { get; set; }

        public Guid AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
