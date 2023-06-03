using Contracts;
using Contracts.Users;

namespace Services
{
    public class QuizBaseService
    {
        private static CurrentUserDto CurrentUser = new CurrentUserDto
        {
            Id = QuizConstant.DEFAULT_USER_ID
        };

        protected CurrentUserDto GetCurrentUser()
        {
            return CurrentUser;
        }
    }
}
