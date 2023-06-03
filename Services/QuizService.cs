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
    public class QuizService: QuizBaseService, IQuizService
    {
        private readonly RepositoryDbContext _context;
        private readonly IMapper _mapper;

        public QuizService(RepositoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AnswerQuizResultDto> SubmitAnswerAsync(Guid questionId, AnswerQuizDto answerQuizDto)
        {
            var question = await _context.Questions.Include(x => x.Answers).FirstOrDefaultAsync(x => x.Id == questionId);

            if (question is null)
            {
                throw new NotFoundException($"Question with id '{questionId}' does not exist.");
            }

            var session = await _context.QuizSessions.FindAsync(answerQuizDto.QuizSessionId);
            if (session is null)
            {
                throw new NotFoundException($"Invalid session with id '{answerQuizDto.QuizSessionId}'.");
            }
            if (session.UserId != GetCurrentUser().Id)
            {
                throw new ForbiddenRequestException($"You are not allowed to access session with id '${answerQuizDto.QuizSessionId}'");
            }

            if ((await _context.UserAnswers.CountAsync(x => x.QuizSessionId == answerQuizDto.QuizSessionId)) == question.Answers.Count)
            {
                throw new BadRequestException($"Session with id '${answerQuizDto.QuizSessionId}' has already finished.");
            }
 
            var selectedAnswer = await _context.Answers.FindAsync(answerQuizDto.AnswerId);
            if (selectedAnswer is null || selectedAnswer.QuestionId != question.Id)
            {
                throw new NotFoundException($"Invalid answer with id '{answerQuizDto.AnswerId}'.");
            }

            var isAnswered = await _context.UserAnswers.AnyAsync(x => x.QuestionId == questionId && x.QuizSessionId == answerQuizDto.QuizSessionId);
            if (isAnswered)
            {
                throw new BadRequestException($"You are already answer this question.");
            }

            var userAnswer = new UserAnswer()
            {
                QuizSessionId = answerQuizDto.QuizSessionId,
                QuestionId = questionId,
                AnswerId = answerQuizDto.AnswerId,
                IsCorrect = question.TrueAnswerId == answerQuizDto.AnswerId
            };

            if ((await _context.UserAnswers.CountAsync(x => x.QuizSessionId == answerQuizDto.QuizSessionId)) == question.Answers.Count - 1)
            {
                session.EndAt = DateTime.Now;
                _context.QuizSessions.Update(session);
            }
        
            await _context.UserAnswers.AddAsync(userAnswer);
            await _context.SaveChangesAsync();

            var correctAnswer = await _context.Answers.FindAsync(question.TrueAnswerId);

            return new AnswerQuizResultDto()
            {
                IsCorrect = question.TrueAnswerId == answerQuizDto.AnswerId,
                Question = _mapper.Map<QuestionDto>(question),
                SelectedAnswer = _mapper.Map<AnswerDto>(selectedAnswer),
                CorrectAnswer = _mapper.Map<AnswerDto>(correctAnswer)
            };
        }
    }
}
