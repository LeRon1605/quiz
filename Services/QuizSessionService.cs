using AutoMapper;
using Contracts.Answers;
using Contracts.Questions;
using Contracts.QuizSessions;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class QuizSessionService : QuizBaseService, IQuizSessionService
    {
        private readonly RepositoryDbContext _context;
        private readonly IMapper _mapper;

        public async Task<QuizSessionResultDto> GetResultOfQuizSessionAsync(Guid quizSessionId)
        {
            var session = await _context.QuizSessions.Include(x => x.UserAnswers)
                                                        .ThenInclude(x => x.Question)
                                                     .Include(x => x.UserAnswers)
                                                        .ThenInclude(x => x.Answer)
                                                     .FirstOrDefaultAsync(x => x.Id == quizSessionId);
            if (session is null)
            {
                throw new NotFoundException($"Invalid session with id '{quizSessionId}'.");
            }
            if (session.UserId != GetCurrentUser().Id)
            {
                throw new ForbiddenRequestException($"You are not allowed to access session with id '${quizSessionId}'");
            }
            if (session.EndAt == null || session.EndAt == DateTime.MinValue)
            {
                throw new BadRequestException($"Session with id '${quizSessionId}' has not finished yet.");
            }

            var details = new List<AnswerQuizResultDto>();

            foreach (var userAnswer in session.UserAnswers)
            {
                var correctAnswer = await _context.Answers.FindAsync(userAnswer.Question.TrueAnswerId);
                details.Add(new AnswerQuizResultDto
                {
                    IsCorrect = userAnswer.IsCorrect,
                    Question = _mapper.Map<QuestionDto>(userAnswer.Question),
                    SelectedAnswer = _mapper.Map<AnswerDto>(userAnswer.Answer),
                    CorrectAnswer = _mapper.Map<AnswerDto>(correctAnswer)
                });
            }

            var result = new QuizSessionResultDto()
            {
                Id = session.Id,
                StartAt = session.StartAt,
                EndAt = session.EndAt.Value,
                TotalCorrectAnswers = session.UserAnswers.ToList().Count(x => x.IsCorrect),
                TotalIncorrectAnswers = session.UserAnswers.ToList().Count(x => !x.IsCorrect),
                IsPassed = session.UserAnswers.ToList().Count(x => x.IsCorrect) > session.UserAnswers.Count() * 0.5,
                Details = details
            };

            return result;
        }

        public QuizSessionService(RepositoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<QuizSessionDto> CreateSessionAsync(QuizSessionCreateDto quizSessionCreateDto)
        {
            var quizSession = new QuizSession()
            {
                Id = Guid.NewGuid(),
                StartAt = DateTime.Now,
                EndAt = null,
                UserId = quizSessionCreateDto.UserId
            };

            await _context.QuizSessions.AddAsync(quizSession);
            await _context.SaveChangesAsync();

            return _mapper.Map<QuizSessionDto>(quizSession);
        }
    }
}
