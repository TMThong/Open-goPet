using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if Obfuscator_Free
#else
namespace OPS.Obfuscator.Attribute
{
    /// <summary>
    /// Add this to an Class to skip obfuscation of all Method Bodys, or to an specific Method to skip its Method Body.
    /// String Obfuscation, Random Code generation, ... are part of the Method Body obfuscation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DoNotObfuscateMethodBodyAttribute : System.Attribute
    {
    }
}
#endif