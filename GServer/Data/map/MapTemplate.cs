namespace Gopet.Data.Map
{
    public class MapTemplate
    {
        public int mapId { get; private set; }
        public string name { get; private set; }
        public int[] npc { get; set; }
        public int[] boss { get; private set; }
        public int[] numPetDie { get; private set; }

        public string getName(Player player)
        {
            return player.Language.MapLanguage[this.mapId];
        }

        #region HẰNG SỐ
        /// <summary>
        /// Thành Phố Linh Thú
        /// </summary>
        public const int THÀNH_PHỐ_LINH_THÚ = 11;
        /// <summary>
        /// Ải
        /// </summary>
        public const int ẢI = 12; // ải
        /// <summary>
        /// Linh Lâm
        /// </summary>
        public const int LINH_LÂM = 13; // Linh Lâm
        /// <summary>
        /// Linh Mộc
        /// </summary>
        public const int LINH_MỘC = 14; // Linh Mộc
        /// <summary>
        /// Đại Linh Cảnh
        /// </summary>
        public const int ĐẠI_LINH_CẢNH = 15; // Đại Linh Cảnh
        /// <summary>
        /// Đường lên đỉnh núi
        /// </summary>
        public const int ĐƯỜNG_LÊN_ĐỈNH_NÚI = 16; // Đường lên đỉnh núi
        /// <summary>
        /// Thung lũng Hoàng Nham
        /// </summary>
        public const int THUNG_LŨNG_HOÀNG_NHAM = 17; // Thung lũng Hoàng Nham
        /// <summary>
        /// Núi Phục Quang
        /// </summary>
        public const int NÚI_PHỤC_QUANG = 18; // Núi Phục Quang
        /// <summary>
        /// Đấu trường
        /// </summary>
        public const int ĐẤU_TRƯỜNG = 19; // Đấu trường
        /// <summary>
        /// Lôi đài
        /// </summary>
        public const int LÔI_ĐÀI = 20; // Lôi đài
        /// <summary>
        /// Thạch Động
        /// </summary>
        public const int THẠCH_ĐỘNG = 21; // Thạch Động
        /// <summary>
        /// Chợ trời
        /// </summary>
        public const int CHỢ_TRỜI = 22; // Chợ trời
        /// <summary>
        /// Băng động 1
        /// </summary>
        public const int BĂNG_ĐỘNG_1 = 23; // Băng động 1
        /// <summary>
        /// Sông băng
        /// </summary>
        public const int SÔNG_BĂNG = 24; // Sông băng
        /// <summary>
        /// Băng động 2
        /// </summary>
        public const int BĂNG_ĐỘNG_2 = 25; // Băng động 2
        /// <summary>
        /// Vùng đất phong ấn
        /// </summary>
        public const int VÙNG_ĐẤT_PHONG_ẤN = 26; // Vùng đất phong ấn
        /// <summary>
        /// Đài tưởng niệm
        /// </summary>
        public const int ĐÀI_TƯỞNG_NIỆM = 27; // Đài tưởng niệm
        /// <summary>
        /// Quảng trường chính
        /// </summary>
        public const int QUẢNG_TRƯỜNG_CHÍNH = 28; // Quảng trường chính
        /// <summary>
        /// Thành phố Thiên Thần
        /// </summary>
        public const int THÀNH_PHỐ_THIÊN_THẦN = 29; // Thành phố Thiên Thần
        /// <summary>
        /// Khu vực bang hội
        /// </summary>
        public const int KHU_VỰC_BANG_HỘI = 30; // Khu vực bang hội
        /// <summary>
        /// Ải Thượng Giới
        /// </summary>
        public const int ẢI_THƯỢNG_GIỚI = 31; // ải thượng giới
        /// <summary>
        /// Chốt chặn cuối cùng
        /// </summary>
        public const int CHỐT_CHẶN_CUỐI_CÙNG = 32; // Chốt chặn cuối cùng
        /// <summary>
        /// Những cây cầu
        /// </summary>
        public const int NHỮNG_CÂY_CẦU = 33; // Những cây cầu
        /// <summary>
        /// Vùng chiến sự
        /// </summary>
        public const int VÙNG_CHIẾN_SỰ = 34; // Vùng chiến sự
        #endregion
    }
}