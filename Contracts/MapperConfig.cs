using AutoMapper;
using Contracts.Answers;
using Contracts.Questions;
using Contracts.QuizSessions;
using Domain.Entities;

namespace Contracts
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<Question, QuestionDto>();
            CreateMap<Question, QuestionDetailDto>();

            CreateMap<Answer, AnswerDto>();

            CreateMap<QuizSession, QuizSessionDto>();
        }
    }
}
