using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.pet
{
    /// <summary>
    /// Dữ liệu mẫu cho trùng sinh thú cưng
    /// Reincarnation: Trùng sinh hoặc nghĩa là luân hồi
    /// </summary>
    public class PetReincarnation 
    {
        public int Id { get; set; }
        /// <summary>
        /// Pet cần
        /// </summary>
        public int PetId { get; set; }
        /// <summary>
        /// Số thẻ trùng sinh cần
        /// </summary>
        public int NumCard { get; set; } = 1;
        /// <summary>
        /// Pet thành sau khi trùng sinh
        /// </summary>
        public int PetIdReincarnation { get; set; }
    }
}
