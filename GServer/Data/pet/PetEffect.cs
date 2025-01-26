using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.pet
{
    /// <summary>
    /// Hiệu ứng của pet
    /// </summary>
    public class PetEffect
    {
        public int IdTemplate { get; set; }
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

        public PetEffect()
        {
        }

        public PetEffect(int idTemplate, int atk, int def, int hp, int mp, int @int, int str, int agi)
        {
            IdTemplate = idTemplate;
            Atk = atk;
            Def = def;
            Hp = hp;
            Mp = mp;
            Int = @int;
            Str = str;
            Agi = agi;
        }

        public PetEffect(PetEffectTemplate template)
        {
            IdTemplate = template.IdTemplate;
            Atk = template.Atk;
            Def = template.Def;
            Hp = template.Hp;
            Mp = template.Mp;
            Int = template.Int;
            Str = template.Str;
            Agi = template.Agi;
        }
        [JsonIgnore]
        public PetEffectTemplate Template
        {
            get
            {
                return GopetManager.PET_EFF_TEMP[IdTemplate];
            }
        }
    }
}
