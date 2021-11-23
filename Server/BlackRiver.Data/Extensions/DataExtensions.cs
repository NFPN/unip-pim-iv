using BlackRiver.EntityModels;
using System.Collections.Generic;
using System.Linq;

namespace BlackRiver.Data
{
    public static class DataExtensions
    {
        public static IEnumerable<UserLogin> WithoutPasswords(this IEnumerable<UserLogin> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static UserLogin WithoutPassword(this UserLogin user)
        {
            user.Password = null;
            return user;
        }
    }
}
