using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugViewer
    {
    [AttributeUsage (AttributeTargets.Field, AllowMultiple = true)]
    public class DebugAttribute : Attribute
        {
        public string Catergory { get; private set; }
        public string Name { get; private set; }

        public DebugAttribute(string catergory, string name, object target = null)
            {
            this.Catergory = catergory;
            this.Name = name;
            }
        }
    }
