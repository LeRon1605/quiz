using Contracts;
using Contracts.Users;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    public class QuizBaseController: ControllerBase
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
