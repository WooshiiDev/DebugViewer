using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace DebugViewer
    {
    public class DebugField
        {
        /// <summary>
        /// Drawn text for the field
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Drawn value for the field
        /// </summary>
        public string Value { get; private set; }

        //Reflective
        private readonly string fieldName;

        private object target;

        private FieldInfo field;
        private Type fieldType;

        /// <summary>
        /// Is the field a collection?
        /// </summary>
        public bool IsCollection { get; private set; }

        /// <summary>
        /// Combination of <see cref="Name"/> and <see cref="Value"/> for drawing to the screen
        /// </summary>
        public string Info => $"{Name}: {Value}";

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugField"/> class.
        /// </summary>
        /// <param name="displayName">Displayed text/name for the field</param>
        /// <param name="fieldName">The name of the actual field</param>
        /// <param name="target">The target object the field belongs to</param>
        public DebugField(string displayName, string fieldName, object target)
            {
            //Set values
            this.Name = displayName;
            this.fieldName = fieldName;
            this.target = target;

            //Get initial value
            Value = GetFieldInfo (fieldName);
            }

        /// <summary>
        /// Get the stored value on the target field
        /// </summary>
        public void UpdateValue()
            {
            Value = GetFieldInfo (fieldName);
            }

        /// <summary>
        /// Get field information to display
        /// </summary>
        /// <returns>Returns the field value, but if null will return invalid</returns>
        private string GetFieldInfo(string fieldName)
            {
            string retVal = "";

            //Initial check
            if (field == null)
                {
                field = target.GetType ().GetField (fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

                //No point going on if it's still null
                if (field == null)
                    {
                    Debug.LogError ("Cannot find field of type");
                    return "Invalid value";
                    }

                fieldType = field.FieldType;
                IsCollection = typeof (IEnumerable).IsAssignableFrom (fieldType);
                }

            //Filter usecase types (collections, objects)

            //Iterate and add collection values
            if (IsCollection)
                {
                retVal = "\n";
                IEnumerable enumerable = (IEnumerable)field.GetValue (target);

                int i = 0;
                foreach (System.Object value in enumerable)
                    {
                    retVal += $"\t{i}: {value}\n";
                    i++;
                    }

                retVal = retVal.TrimEnd ();

                return retVal;
                }

            return field.GetValue (target).ToString ();
            }
        }
    }