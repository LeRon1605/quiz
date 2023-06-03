using AutoMapper;
using Contracts.Questions;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class QuestionService : QuizBaseService, IQuestionService
    {
        private readonly RepositoryDbContext _context;
        private readonly IMapper _mapper;

        public QuestionService(RepositoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<QuestionDetailDto>> GetAllAsync()
        {
            var questions = await _context.Questions.AsNoTracking().Include(x => x.Answers).ToListAsync();
            return _mapper.Map<List<QuestionDetailDto>>(questions);
        }
    }
}
