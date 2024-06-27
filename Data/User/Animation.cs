using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.User
{
    public record Animation(int numFrame, string frameImgPath, int vX, int vY, bool isDrawEnd, bool mirrorWithChar);
}
