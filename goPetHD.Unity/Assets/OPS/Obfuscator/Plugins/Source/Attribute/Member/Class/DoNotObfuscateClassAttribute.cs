using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPS.Obfuscator.Attribute
{
    /// <summary>
    /// Add this to a Class, so the whole class with all its content will not get obfuscated! 
    /// But still its Method Bodys (String Obfuscation, Random Code generation ...)
    /// To not obfuscate Method Bodys too, add an additional Attribute: DoNotObfuscateMethodBodyAttribute to the classes or the specific methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
    public class DoNotObfuscateClassAttribute : System.Attribute
    {
    }
}
