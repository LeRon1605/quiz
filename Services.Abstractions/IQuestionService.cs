using Contracts.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IQuestionService
    {
        Task<List<QuestionDetailDto>> GetAllAsync();
    }
}
