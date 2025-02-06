using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if Obfuscator_Free
#else
namespace OPS.Obfuscator.Attribute
{
    /// <summary>
    /// Add this to an Class, to disallow fake code adding! Or to disallow using the class to create new fake classes basing on it!
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
    public class DoNotUseClassForFakeCodeAttribute : System.Attribute
    {
    }
}
#endif