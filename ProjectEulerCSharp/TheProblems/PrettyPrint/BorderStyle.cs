using System;

namespace TheProblems.PrettyPrint
{
    [Flags]
    public enum BorderStyle
    {
        None = 0,
        Solid = 1,
        Dashed = 1 << 1,
        Dotted = 1 << 2,
        Double = 1 << 3
    }
}