using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.User
{
    public record Animation(sbyte numFrame, string frameImgPath, int vX, int vY, bool isDrawEnd, bool mirrorWithChar, sbyte type)
    {
        public const sbyte TYPE_ARCHIVENMENT = 0;
    }
}
