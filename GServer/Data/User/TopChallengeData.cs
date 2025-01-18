using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.User
{
    /// <summary>
    /// Dữ liệu thử thách hàng đầu
    /// </summary>
    /// <param name="Id">Mã định danh</param>
    /// <param name="Type">Loại</param>
    /// <param name="Time">Thời gian</param>
    /// <param name="Turn">Lượt</param>
    /// <param name="Name">Tên</param>
    /// <param name="TeamId">Mã đội</param>
    public record TopChallengeData(int Id, int Type, TimeSpan Time, int Turn, string[] Name, int[] TeamId);
}
