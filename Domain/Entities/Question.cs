using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        
        public Guid TrueAnswerId { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
