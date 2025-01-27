using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.pet
{
    /// <summary>
    /// Template của hiệu ứng của pet
    /// </summary>
    public sealed class PetEffectTemplate
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
        public int FrameNum { get; set; }
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

        #region Hằng số
        /// <summary>
        /// Là loại hiệu ứng hào quang
        /// </summary>
        public const sbyte TYPE_AURA = 0;
        /// <summary>
        /// Hào quang hệ lửa
        /// </summary>
        public const int ID_AURA_FIRE = 1;
        /// <summary>
        /// Hào quang hệ cây
        /// </summary>
        public const int ID_AURA_TREE = 2;
        /// <summary>
        /// Hào quang hệ đất
        /// </summary>
        public const int ID_AURA_ROCK = 3;
        /// <summary>
        /// Hào quang hệ sét
        /// </summary>
        public const int ID_AURA_THUNDER = 4;
        /// <summary>
        /// Hào quang hệ nước
        /// </summary>
        public const int ID_AURA_WATER = 5;
        /// <summary>
        /// Hào quang hệ bóng tối
        /// </summary>
        public const int ID_AURA_DARK = 6;
        /// <summary>
        /// Hào quang hệ sáng
        /// </summary>
        public const int ID_AURA_LIGHT = 7;

        public static int GetAuraIdByElement(int element)
        {
            return element switch
            {
                1 => ID_AURA_FIRE,
                2 => ID_AURA_TREE,
                3 => ID_AURA_ROCK,
                4 => ID_AURA_THUNDER,
                5 => ID_AURA_WATER,
                6 => ID_AURA_DARK,
                7 => ID_AURA_LIGHT,
                _ => 1,
            };
        }
        #endregion
    }
}
