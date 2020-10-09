using System.Collections.Generic;
using UnityEngine;

namespace DebugViewer
    {
    /// <summary>
    /// Category of fields that draw them to the screen
    /// </summary>
    public class DebugCategory
        {
        /// <summary>
        /// The name of the category
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Is the category collapsed or shown?
        /// </summary>
        public bool IsShown { get; private set; }

        /// <summary>
        /// Draw position on the screen
        /// </summary>
        public Rect DrawPosition { get; private set; }

        /// <summary>
        /// List of fields that belong to this category
        /// </summary>
        public List<DebugField> information;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugCategory"/> class.
        /// </summary>
        /// <param name="name">Name of the category</param>
        public DebugCategory(string name)
            {
            information = new List<DebugField> ();
            DrawPosition = new Rect ();

            Name = name;
            }

        /// <summary>
        /// Add a field to the category to display
        /// </summary>
        /// <param name="info">Info to add</param>
        public void AddInfo(DebugField info)
            {
            information.Add (info);
            }

        /// <summary>
        /// Calculate the height of the debug category window
        /// </summary>
        /// <returns>Returns the full height of the category</returns>
        public float GetHeight()
            {
            float height = 20f;

            if (!IsShown)
                {
                return height;
                }

            if (information == null || information.Count == 0)
                {
                return height;
                }

            height = information.Count * 19f + 20f;

            for (int i = 0; i < information.Count; i++)
                {
                DebugField item = information[i];

                if (item.IsCollection)
                    {
                    height += 19f * (item.Value.Split ('\n').Length - 1);
                    }
                }

            return height;
            }

        /// <summary>
        /// Draw the category
        /// </summary>
        /// <param name="position"></param>
        public void DrawCategory(Rect position, GUIStyle headerStyle)
            {
            Rect drawRect = DrawPosition = position;
            Rect backgroundRect = new Rect (DrawPosition.x - 2, DrawPosition.y - 2, DrawPosition.width + 4, DrawPosition.height + 4);

            //Draw title
            if (!IsShown)
                {
                backgroundRect.height = 19;
                }

            //Draw background and title
            GUI.Box (backgroundRect, "", GUI.skin.box);
            GUI.Label (drawRect, Name, headerStyle);

            drawRect.y -= 3;
            IsShown = GUI.Toggle (drawRect, IsShown, "");

            //Show info
            if (IsShown)
                {
                for (int i = 0; i < information.Count; i++)
                    {
                    DebugField item = information[i];
                    item.UpdateValue ();

                    drawRect.y += 19f;

                    if (i != 0)
                        {
                        DebugField previousItem = information[i - 1];

                        //if (previousItem.IsArray)
                        //    {
                        //    int len = Mathf.Max(previousItem.Value.Split ('\n').Length - 2, 0);
                        //    drawRect.y += 19f * len;
                        //    }
                        }

                    if (item.IsCollection)
                        {
                        GUI.Label (drawRect, item.Name + ":");

                        string[] splits = item.Value.Split ('\n');
                        for (int j = 1; j < splits.Length; j++)
                            {
                            drawRect.y += 19f;
                            GUI.Label (drawRect, splits[j]);
                            }

                        drawRect.height += 19f * (splits.Length - 1);
                        }
                    else
                        {
                        GUI.Label (drawRect, item.Info);
                        }
                    }
                }
            }
        }
    }