using Reqnroll;

namespace SimpleSeleniumFramework.Support
{
    [Binding]
    public class UserTransformations
    {
        private readonly UserManager _userManager;

        public UserTransformations(UserManager userManager)
        {
            _userManager = userManager;
        }

        [StepArgumentTransformation]
        public BrowserUser TransformUser(string name)
        {
            return _userManager.GetUser(name);
        }
    }
}
