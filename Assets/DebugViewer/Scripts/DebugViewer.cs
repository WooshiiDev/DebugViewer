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

        public string[] defaultCategories;

        private GUIStyle header;
        private static Dictionary<string, DebugCategory> categories = new Dictionary<string, DebugCategory> ();

        private void Awake()
            {
            if (defaultCategories != null)
                {
                for (int i = 0; i < defaultCategories.Length; i++)
                    {
                    string category = defaultCategories[i];

                    if (!categories.ContainsKey (category))
                        {
                        categories.Add (category, new DebugCategory (category));
                        }
                    }
                }
            }

        private void Start()
            {
            //Called in start to make sure everything is loaded propertly
            if (searchForAttributes)
                {
                FindAttributes ();
                }
            }

        private void OnGUI()
            {
            if (!showDebug)
                {
                return;
                }

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

            DebugCategory[] cats = categories.Values.ToArray ();

            DebugCategory previousCat = null;
            Rect drawRect = new Rect ();

            //Iterate over categories and draw information
            for (int i = 0; i < cats.Length; i++)
                {
                DebugCategory cat = cats[i];

                if (cat.information.Count == 0)
                    {
                    continue;
                    }

                float height = cat.GetHeight ();

                drawRect.Set (10, 10, 350, height);

                //Previous category checks
                if (previousCat != null)
                    {
                    drawRect.y = previousCat.DrawPosition.y;
                    drawRect.y += (previousCat.IsShown ? previousCat.GetHeight () + 5f : 20f);
                    }

                cat.DrawCategory (drawRect, header);
                previousCat = cat;
                }
            }

        private void FindAttributes()
            {
            //Look for all monobehaviour components
            MonoBehaviour[] components = FindObjectsOfType<MonoBehaviour> ();

            //Iterate through
            for (int i = 0; i < components.Length; i++)
                {
                MonoBehaviour component = components[i];
                Type componentType = component.GetType ();

                //Get all fields
                FieldInfo[] types = ReflectionUtil.GetFields (componentType, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.Static);

                //Iterate over fields and look for the DebugAttribute, if it exists, add it to the correct place
                for (int j = 0; j < types.Length; j++)
                    {
                    FieldInfo field = types[j];

                    Attribute attribute = field.GetCustomAttribute (typeof (DebugAttribute));

                    if (attribute != null)
                        {
                        DebugAttribute debugAttr = attribute as DebugAttribute;
                        AddInformationToCategory (debugAttr.Category, new DebugField (debugAttr.Name, field.Name, component), true);
                        }
                    }
                }
            }

        /// <summary>
        /// Add a new information field to the category provided
        /// </summary>
        /// <param name="category">The category to add the information to</param>
        /// <param name="information">The information to add</param>
        /// <param name="addCategory">Optional boolean to add the category if it doesn't already exist</param>
        public static void AddInformationToCategory(string category, DebugField information, bool addCategory = false)
            {
            //If the category doesn't exist add it (if allowed) or return;
            if (!categories.ContainsKey (category))
                {
                if (!addCategory)
                    {
                    Debug.Log ("Cannot find category to add debug to!");
                    return;
                    }

                AddCategory (category);
                }

            DebugCategory cat = categories[category];

            //No point adding data that already exists
            if (cat.information.Contains (information))
                {
                Debug.Log ("Category already contains debug information of " + information.Name);
                return;
                }

            cat.AddInfo (information);
            }

        /// <summary>
        /// Add a new category
        /// </summary>
        /// <param name="category">The new category name</param>
        public static void AddCategory(string category)
            {
            //Return if the category already exists
            if (categories.ContainsKey (category))
                {
                Debug.LogWarning ("Cannot add category " + category + " as it already exists.");
                return;
                }

            categories.Add (category, new DebugCategory (category));
            }

        /// <summary>
        /// Add a new category with an array of <seealso cref="DebugField"/>'s added to it
        /// </summary>
        /// <param name="category">The name of the category to create</param>
        /// <param name="info">The DebugInformation values to add to the category</param>
        public static void AddCategory(string category, params DebugField[] info)
            {
            //Only add the category if it doesn't already exist
            if (categories.ContainsKey (category))
                {
                Debug.LogWarning ("Cannot add category " + category + " as it already exists.");
                }
            else
                {
                categories.Add (category, new DebugCategory (category));
                }

            if (info == null || info.Length == 0)
                {
                return;
                }

            //Add fields to category
            for (int i = 0; i < info.Length; i++)
                {
                DebugField element = info[i];
                categories[category].AddInfo (element);
                }
            }
        }
    }