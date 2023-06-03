using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class QuizSession
    {
        public Guid Id { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
