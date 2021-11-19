using BlackRiver.EntityModels;
using System.Collections.Generic;
using System.Linq;

namespace BlackRiver.Data
{
    public static class DataExtensions
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }
    }
}
