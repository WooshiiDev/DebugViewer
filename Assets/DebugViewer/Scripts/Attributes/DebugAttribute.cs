using System;

namespace DebugViewer
    {
    [AttributeUsage (AttributeTargets.Field, AllowMultiple = true)]
    public class DebugAttribute : Attribute
        {
        public string Category { get; private set; }
        public string Name { get; private set; }

        public DebugAttribute(string catergory, string name, object target = null)
            {
            this.Category = catergory;
            this.Name = name;
            }
        }
    }