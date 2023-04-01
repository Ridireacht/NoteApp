using NoteApp.Models;


namespace NoteApp.Authentication
{
    public class UserRepository : IUserRepository
    {
        // список для хранения всех пользователей
        private List<User> _users = new();


        // получение всех пользователей
        public async void GetUsers()
        {
            var context = new Context();
            _users = context.Users.ToList();
        }

        
        // процедура аутентификации
        public async Task<bool> Authenticate(string username, string password)
        {
            if (await Task.FromResult(_users.SingleOrDefault(x => x.Username == username && x.Password == password)) != null)
                return true;
            
            return false;
        }


        // получение списка имён пользователей
        public async Task<List<string>> GetUserNames()
        {
            List<string> usernames = new();

            foreach (var user in _users)
                usernames.Add(user.Username);

            return await Task.FromResult(usernames);
        }
    }
}
