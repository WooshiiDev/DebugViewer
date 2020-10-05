using System.Collections.Generic;
using UnityEngine;

namespace DebugViewer
    {
    public class DebugCatergory
        {
        public string Name { get; private set; }
        public bool IsShown { get; private set; }

        public Rect DrawPosition { get; private set; }

        public List<DebugInformation> information;

        public DebugCatergory(string name)
            {
            information = new List<DebugInformation> ();
            DrawPosition = new Rect ();

            this.Name = name;
            }

        /// <summary>
        /// Add a field to the catergory to display
        /// </summary>
        /// <param name="info">Info to add</param>
        public void AddInfo(DebugInformation info)
            {
            information.Add (info);
            }

        /// <summary>
        /// Calculate the height of the debug catergory window
        /// </summary>
        /// <returns>Returns the full height of the catergory</returns>
        public float GetHeight()
            {
            if (!IsShown)
                return 20f;

            if (information == null || information.Count == 0)
                return 20f;

            return information.Count * 20f + 20f;
            }

        /// <summary>
        /// Draw the Catergory
        /// </summary>
        /// <param name="position"></param>
        public void DrawCatergory(Rect position, GUIStyle headerStyle)
            {
            Rect headerRect = DrawPosition = position;
            headerRect.height = 19;

            Rect backgroundRect = new Rect (DrawPosition.x - 2, DrawPosition.y - 2, DrawPosition.width + 4, DrawPosition.height + 4);

            //Draw title
            if (!IsShown)
                backgroundRect.height = 19;

            //Draw background and title
            GUI.Box (backgroundRect, "", GUI.skin.box);
            GUI.Label (headerRect, Name, headerStyle);

            headerRect.y -= 3;
            IsShown = GUI.Toggle (headerRect, IsShown, "");

            //Show info
            if (IsShown)
                {
                foreach (var item in information)
                    {
                    headerRect.y += 19;

                    item.UpdateValue ();
                    GUI.Label (headerRect, item.Info);
                    }
                }
            }
        }
    }
