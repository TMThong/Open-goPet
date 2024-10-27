using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.User
{
    public record FriendRequest(int userId, int targetId, DateTime time)
    {
        
    }
}