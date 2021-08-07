using System;

namespace TheProblems.PrettyPrint
{
    [Flags]
    public enum BorderDisplay
    {
        None = 0,
        Top = 1,
        Right = 1 << 1,
        Bottom = 1 << 2,
        Left = 1 << 3,
        All = Top | Right | Bottom | Left
    }
}