using Reqnroll;

namespace SimpleSeleniumFramework.Support
{
    [Binding]
    public class UserTransformations(UserManager userManager)
    {
        private readonly UserManager _userManager = userManager;

        [StepArgumentTransformation]
        public BrowserUser TransformUser(string name) => _userManager.GetUser(name);
    }
}
