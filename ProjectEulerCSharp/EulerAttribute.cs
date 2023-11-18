using System;

namespace ProjectEulerCSharp
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EulerAttribute(string title, string description) : Attribute
    {
        public string Title { get; } = title;
        public string Description { get; } = description;
    }
}