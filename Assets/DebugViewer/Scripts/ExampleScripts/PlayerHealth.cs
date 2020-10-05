using UnityEngine;

namespace DebugViewer
    {
    public class PlayerHealth : MonoBehaviour
        {
        [Debug ("Player", "Max Health")] public int maxHealth = 100;
        [Debug ("Player", "Current Health")] public int currentHealth = 100;

        private void Update()
            {
            if (Input.GetKeyDown (KeyCode.Space))
                currentHealth--;
            }
        }
    }
