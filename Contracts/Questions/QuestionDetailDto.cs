using Contracts.Answers;
using System.Collections.Generic;
using System;

namespace Contracts.Questions
{
    public class QuestionDetailDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
