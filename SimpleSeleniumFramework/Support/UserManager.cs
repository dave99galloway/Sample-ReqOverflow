using System;
using System.Collections.Generic;
using SimpleSeleniumFramework.Drivers;

namespace SimpleSeleniumFramework.Support
{
    public class UserManager : IDisposable
    {
        private readonly DriverFactory _driverFactory;
        private readonly Dictionary<string, BrowserUser> _users = new Dictionary<string, BrowserUser>();

        public UserManager(DriverFactory driverFactory)
        {
            _driverFactory = driverFactory;
        }

        public BrowserUser GetUser(string name)
        {
            if (!_users.ContainsKey(name))
            {
                var driver = _driverFactory.CreateDriver();
                _users[name] = new BrowserUser(name, driver);
            }
            return _users[name];
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
                catch
                {
                    // Ignore errors during disposal
                }
            }
            _users.Clear();
        }
    }
}
