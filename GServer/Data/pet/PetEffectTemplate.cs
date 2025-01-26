using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.pet
{
    public class PetEffectTemplate
    {
        public int IdTemplate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public string FramePath { get; set; }
        public sbyte FrameNum { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int Int { get; set; }
        public int Str { get; set; }
        public int Agi { get; set; }
        public int vX { get; set; }
        public int vY { get; set; }

        public int FrameTime { get; set; }

        public bool IsDrawBefore { get; set; }

        public sbyte Type { get; set; }
    }
}
