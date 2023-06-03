using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswer>
    {
        public void Configure(EntityTypeBuilder<UserAnswer> builder)
        {
            builder.HasKey(x => new { x.QuestionId, x.QuizSessionId });

            builder.HasOne(x => x.QuizSession)
                   .WithMany(x => x.UserAnswers)
                   .HasForeignKey(x => x.QuizSessionId);

            builder.HasOne(x => x.Question)
                   .WithMany(x => x.UserAnswers)
                   .HasForeignKey(x => x.QuestionId);

            builder.HasOne(x => x.Answer)
                   .WithMany(x => x.UserAnswers)
                   .HasForeignKey(x => x.AnswerId);
        }


    }
}
