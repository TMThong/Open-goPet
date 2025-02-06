using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPS.Obfuscator.Attribute
{
    /// <summary>
    /// Add this to an Class, Field, Method, whatever and its name will not get renamed (obfuscated)!
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class DoNotRenameAttribute : System.Attribute
    {
    }
}
