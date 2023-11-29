using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLib;

[AttributeUsage(AttributeTargets.Class)]
public class MyAttribute : Attribute
{
    public int Age { get; set; }
}
