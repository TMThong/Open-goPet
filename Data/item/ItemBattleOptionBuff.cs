using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.item
{
    public record ItemBattleOptionBuff(int OptionId, int OptionValue, bool IsActive, int Turn);
}
