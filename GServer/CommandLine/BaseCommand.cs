using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.CommandLine
{
    public class BaseCommand
    {
        public virtual string Description { get; }

        public virtual string CommandName { get; }

        public virtual void Execute(params string[] args)
        {

        }
    }
}
