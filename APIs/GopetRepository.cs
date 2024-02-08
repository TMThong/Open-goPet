using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.APIs
{
    [Serializable]
    public record GopetRepository<T>(int Status, T Data)
    {
        public GopetRepository() : this(default, default) { }
    }
}
