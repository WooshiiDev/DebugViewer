using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DebugViewer
    {
    public class DebugViewer : MonoBehaviour
        {
        public bool showDebug;
        public bool searchForAttributes;

        public string[] defaultCatergories;

        private GUIStyle header;
        private static Dictionary<string, DebugCatergory> catergories = new Dictionary<string, DebugCatergory> ();

        private void Awake()
            {
            if (defaultCatergories != null)
                {
                for (int i = 0; i < defaultCatergories.Length; i++)
                    {
                    string catergory = defaultCatergories[i];

                    if (!catergories.ContainsKey (catergory))
                        catergories.Add (catergory, new DebugCatergory (catergory));
                    }
                }
            }

        private void Start()
            {
            //Only search for attributes if they're needed/wanted
            if (searchForAttributes)
                {
                //Look for all monobehaviour components
                var components = FindObjectsOfType<MonoBehaviour> ();

                //Iterate through
                for (int i = 0; i < components.Length; i++)
                    {
                    var component = components[i];
                    Type componentType = component.GetType ();

                    //Get all fields
                    FieldInfo[] types = UnityReflectionUtil.GetFields (componentType, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default);

                    //Iterate over fields and look for the DebugAttribute, if it exists, add it to the correct place

                    for (int j = 0; j < types.Length; j++)
                        {
                        var field = types[j];

                        Attribute attribute = field.GetCustomAttribute (typeof (DebugAttribute));

                        if (attribute != null)
                            {
                            DebugAttribute debugAttr = attribute as DebugAttribute;
                            AddInformationToCatergory (debugAttr.Catergory, new DebugInformation (debugAttr.Name, component, field.Name), true);
                            }
                        }
                    }
                }
            }

        private void OnGUI()
            {
            if (!showDebug)
                return;

            //Could put it in awake, but it's not going to take anything away being here
            if (header == null)
                {
                header = new GUIStyle ()
                    {
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.UpperCenter,
                    };

                header.normal.textColor = Color.white;
                }
       

            var cats = catergories.Values.ToArray ();

            DebugCatergory previousCat = null;
            Rect drawRect = new Rect ();

            //Iterate over catergories and draw information 
            for (int i = 0; i < cats.Length; i++)
                {
                var cat = cats[i];

                if (cat.information.Count == 0)
                    continue;

                float height = cat.GetHeight ();

                drawRect.Set (10, 10, 350, height);

                if (previousCat != null)
                    {
                    drawRect.y = previousCat.DrawPosition.y;
                    drawRect.y += (previousCat.IsShown ? previousCat.GetHeight () + 1f : 20f);

                    drawRect.height = height;
                    }

                cat.DrawCatergory (drawRect, header);
                previousCat = cat;
                }
            }

        /// <summary>
        /// Add a new information field to the catergory provided
        /// </summary>
        /// <param name="catergory">The catergory to add the information to</param>
        /// <param name="information">The information to add</param>
        /// <param name="addCatergory">Optional boolean to add the catergory if it doesn't already exist</param>
        public static void AddInformationToCatergory(string catergory, DebugInformation information, bool addCatergory = false)
            {
            if (!catergories.ContainsKey (catergory))
                {
                if (!addCatergory)
                    {
                    Debug.Log ("Cannot find catergory to add debug to!");
                    return;
                    }

                AddCatergory (catergory);
                }

            var cat = catergories[catergory];

            if (cat.information.Contains (information))
                {
                Debug.Log ("Catergory already contains debug information of " + information.name);
                return;
                }

            cat.AddInfo (information);
            }

        /// <summary>
        /// Add a new catergory
        /// </summary>
        /// <param name="catName"></param>
        public static void AddCatergory(string catName)
            {
            if (catergories.ContainsKey (catName))
                {
                Debug.LogWarning ("Cannot add catergory " + catName + " as it already exists.");
                return;
                }

            catergories.Add (catName, new DebugCatergory (catName));
            }

        /// <summary>
        /// Add a new catergory with an array of <seealso cref="DebugInformation"/>'s added to it
        /// </summary>
        /// <param name="catName">The name of the catergory to create</param>
        /// <param name="info">The DebugInformation values to add to the catergory</param>
        public static void AddCatergory(string catName, params DebugInformation[] info)
            {
            if (catergories.ContainsKey (catName))
                Debug.LogWarning ("Cannot add catergory " + catName + " as it already exists.");
            else
                catergories.Add (catName, new DebugCatergory (catName));

            if (info == null || info.Length == 0)
                return;

            for (int i = 0; i < info.Length; i++)
                {
                var element = info[i];
                catergories[catName].AddInfo (element);
                }
            }
        }
    }

