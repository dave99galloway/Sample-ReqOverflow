using Microsoft.Extensions.Configuration;
using SimpleSeleniumFramework.Drivers;

namespace SimpleSeleniumFramework.Support
{
    public class UserManager(DriverFactory driverFactory, IConfiguration configuration) : IDisposable
    {
        private readonly DriverFactory _driverFactory = driverFactory;
        private readonly IConfiguration _configuration = configuration;
        private readonly Dictionary<string, BrowserUser> _users = [];

        public BrowserUser GetUser(string name)
        {
            if (!_users.TryGetValue(name, out BrowserUser? value))
            {
                var driver = _driverFactory.CreateDriver();
                value = new BrowserUser(name, driver, _configuration);
                _users[name] = value;
            }
            return value;
        }

        public IEnumerable<BrowserUser> ActiveUsers => _users.Values;

        public void Dispose()
        {
            foreach (var user in _users.Values)
            {
                try
                {
                    user.Driver.Quit();
                    user.Driver.Dispose();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Exception disposong of user {user.Name}. Message: {ex.Message}");
                }
            }
            _users.Clear();
            GC.SuppressFinalize(this);
        }
    }
}
