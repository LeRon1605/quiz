using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<QuizSession> QuizSessions { get; set; }
    }
}
