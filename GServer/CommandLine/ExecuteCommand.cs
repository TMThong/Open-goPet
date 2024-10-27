using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.CommandLine
{
    internal class ExecuteCommand : BaseCommand
    {
        public override string Description => "<execute> để thực thi lệnh";

        public override string CommandName => "execute";

        public override void Execute(params string[] args)
        {
             
        }
    }
}
