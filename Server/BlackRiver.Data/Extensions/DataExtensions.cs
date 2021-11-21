using BlackRiver.EntityModels;
using System.Collections.Generic;
using System.Linq;

namespace BlackRiver.Data
{
    public static class DataExtensions
    {
        public static IEnumerable<Login> WithoutPasswords(this IEnumerable<Login> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static Login WithoutPassword(this Login user)
        {
            user.Password = null;
            return user;
        }
    }
}
