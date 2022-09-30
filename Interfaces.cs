using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    interface IStorage
    {
        Task<User> Register(User user);
        Task<User> Auth(User user);
    }
}
