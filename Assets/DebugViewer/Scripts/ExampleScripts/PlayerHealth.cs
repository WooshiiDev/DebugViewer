using UnityEngine;

namespace DebugViewer
    {
    public class PlayerHealth : MonoBehaviour
        {
        [Header("Player Stats")]
        [Debug ("Player", "Max Health")] public int maxHealth = 100;
        [Debug ("Player", "Current Health")] public int currentHealth = 100;

        [Header ("Locomotion")]
        public int walkSpeed = 5;
        public int runSpeed = 10;

        [Header ("Damage")]
        public int meleeDamage = 9;
        public int shootDamage = 6;

        private void Awake()
            {
            DebugViewer.AddInformationToCatergory ("Movement", new DebugInformation ("Walk Speed", this, "walkSpeed"), true);
            DebugViewer.AddInformationToCatergory ("Movement", new DebugInformation ("Run Speed", this, "runSpeed"), true);

            DebugViewer.AddInformationToCatergory ("Damage", new DebugInformation ("Melee Damage", this, "meleeDamage"), true);
            DebugViewer.AddInformationToCatergory ("Damage", new DebugInformation ("Shoot Damage", this, "shootDamage"), true);
            }

        private void Update()
            {
            if (Input.GetKeyDown (KeyCode.Space))
                currentHealth--;
            }
        }
    }
