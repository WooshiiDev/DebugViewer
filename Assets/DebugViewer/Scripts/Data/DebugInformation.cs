using System;
using System.Reflection;
using UnityEngine;

namespace DebugViewer
    {
    public class DebugInformation
        {
        //Display
        public string name;
        public string info;

        //Reflective
        public object target;
        private Type targetType;
        private FieldInfo field;

        public string Info => $"{name}: {info}";

        public DebugInformation(string name, object target, string variable)
            {
            this.name = name;

            this.target = target;
            this.targetType = target.GetType ();

            this.info = variable;

            info = GetFieldInfo ();
            }

        /// <summary>
        /// Get field information to display
        /// </summary>
        /// <returns>Returns the field value, but if null will return invalid</returns>
        public string GetFieldInfo()
            {
            //Initial check
            if (field == null)
                field = targetType.GetField (info, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (field == null)
                {
                Debug.LogError ("Cannot find field of type");
                return "Invalid value";
                }
          
            return info = field.GetValue (target).ToString ();
            }

        public void UpdateValue()
            {
            info = GetFieldInfo ();
            }
        }
    }
