using System;
using System.Collections.Generic;
using System.Linq;
using TodoAPI.Extensions;
using TodoAPI.Models;

namespace TodoAPI.Stores {
    public interface IUserStore {
        void CreateUser(User user);
        User GetUser(int userId);
        User GetUser(string username, string passwordHash);
    }

    public class UserStore : IUserStore {
        private readonly List<User> _users = new List<User>();

        public void CreateUser(User user) {
            user.Id = _users.MaxDefault(o => o.Id, 0) + 1;
            _users.Add(user);
        }

        public User GetUser(int userId) {
            return _users.Single(o => o.Id == userId);
        }

        public User GetUser(string username, string passwordHash) {
            return _users.Single(o =>
                string.Equals(o.Username, username, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(o.PasswordHash, passwordHash, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}