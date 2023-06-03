using Contracts.Questions;

namespace Contracts.Answers
{
    public class AnswerQuizResultDto
    {
        public bool IsCorrect { get; set; }
        public QuestionDto Question { get; set; }
        public AnswerDto SelectedAnswer { get; set; }
        public AnswerDto CorrectAnswer { get; set; }
    }
}
