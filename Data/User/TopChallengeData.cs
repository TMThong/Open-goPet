using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.User
{
    public record TopChallengeData(int Id, int Type, TimeSpan Time, int Turn, string[] Name, int[] TeamId);
}
