using NoteApp.Models;


namespace NoteApp.Authentication
{
    public class UserRepository : IUserRepository
    {
        // тестовый список пользователей
        private List<User> _users = new List<User>
        {
            new User
            {
                ID = 1, Username = "peter", Password = "peter123"
            },
            new User
            {
                ID = 2, Username = "joydip", Password = "joydip123"
            },
            new User
            {
                ID = 3, Username = "james", Password = "james123"
            }
        };

        
        // процедура аутентификации
        public async Task<bool> Authenticate(string username, string password)
        {
            if (await Task.FromResult(_users.SingleOrDefault(x => x.Username == username && x.Password == password)) != null)
            {
                return true;
            }
            return false;
        }


        // получение списка имён пользователей
        public async Task<List<string>> GetUserNames()
        {
            List<string> users = new List<string>();

            foreach (var user in _users)
            {
                users.Add(user.Username);
            }

            return await Task.FromResult(users);
        }
    }
}
