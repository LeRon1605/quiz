using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Answer
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
