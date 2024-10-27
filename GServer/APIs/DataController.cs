using Dapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gopet.APIs.GopetApiExtentsion;
namespace Gopet.APIs
{
    [Route("api/data")]
    [ApiController]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class DataController : ControllerBase
    {
        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
