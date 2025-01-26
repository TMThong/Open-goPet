using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.pet
{
    /// <summary>
    /// Template của hiệu ứng của pet
    /// </summary>
    public class PetEffectTemplate
    {
        /// <summary>
        /// ID của template
        /// </summary>
        public int IdTemplate { get; set; }
        /// <summary>
        /// Tên của template
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Mô tả của template
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Đường dẫn của icon
        /// </summary>
        public string IconPath { get; set; }
        /// <summary>
        /// Đường dẫn của frame
        /// </summary>
        public string FramePath { get; set; }
        /// <summary>
        /// Số frame
        /// </summary>
        public sbyte FrameNum { get; set; }
        /// <summary>
        /// Tấn công
        /// </summary>
        public int Atk { get; set; }
        /// <summary>
        /// Phòng thủ
        /// </summary>
        public int Def { get; set; }
        /// <summary>
        /// Máu
        /// </summary>
        public int Hp { get; set; }
        /// <summary>
        /// Mana
        /// </summary>
        public int Mp { get; set; }
        /// <summary>
        /// Trí tuệ
        /// </summary>
        public int Int { get; set; }
        /// <summary>
        /// Sức mạnh
        /// </summary>
        public int Str { get; set; }
        /// <summary>
        /// Nhanh nhẹn
        /// </summary>
        public int Agi { get; set; }
        /// <summary>
        /// Toạ độ vẽ X
        /// </summary>
        public int vX { get; set; }
        /// <summary>
        /// Toạ độ vẽ Y
        /// </summary>
        public int vY { get; set; }
        /// <summary>
        /// Thời gian mỗi frame
        /// </summary>
        public int FrameTime { get; set; }
        /// <summary>
        /// Có vẽ trước không
        /// </summary>
        public bool IsDrawBefore { get; set; }
        /// <summary>
        /// Loại hiệu ứng
        /// </summary>
        public sbyte Type { get; set; }
    }
}
