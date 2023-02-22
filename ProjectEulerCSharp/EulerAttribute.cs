using System;

namespace ProjectEulerCSharp
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EulerAttribute : Attribute
    {
        public EulerAttribute(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; }
        public string Description { get; }
    }
}