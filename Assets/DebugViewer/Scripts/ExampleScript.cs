using System.Collections.Generic;
using UnityEngine;

namespace DebugViewer
    {
    public class ExampleScript : MonoBehaviour
        {
        [Header ("Example Fields")]
        [Debug ("Player", "Max Health")] private int maxHealth = 100;

        [Debug ("Player", "Current Health")] private int currentHealth = 100;

        public int walkSpeed = 5;
        public int runSpeed = 10;

        public int meleeDamage = 9;
        public int rangeDamage = 6;

        //Other examples
        [Debug ("Collections", "Stat Names")] public string[] stats;

        [Debug ("Collections", "Integer Array")] public int[] intStats;
        [Debug ("Collections", "Float List")] public List<float> floatStats;
        [Debug ("Collections", "Dictionary [string, float]")] public Dictionary<string, float> dictionaryStats;

        [Debug ("Other", "Time")] public float time;

        private void Awake()
            {
            DebugViewer.AddInformationToCategory ("Movement", new DebugField ("Walk Speed", "walkSpeed", this), true);
            DebugViewer.AddInformationToCategory ("Movement", new DebugField ("Run Speed", "runSpeed", this), true);

            DebugViewer.AddInformationToCategory ("Damage", new DebugField ("Melee Damage", "meleeDamage", this), true);
            DebugViewer.AddInformationToCategory ("Damage", new DebugField ("Shoot Damage", "shootDamage", this), true);

            intStats = new int[4];

            floatStats = new List<float>
                {
                1,2,3,4,5,6
                };

            dictionaryStats = new Dictionary<string, float> ()
                {
                    {"Value 0", 1 },
                    {"Value 1", 2 },
                    {"Value 2", 3 },
                    {"Value 3", 4 },
                    {"Value 4", 5 },
                    {"Value 5", 6 },
                };

            RandomiseStats ();
            }

        private void Update()
            {
            if (Input.GetKeyDown (KeyCode.Space))
                {
                currentHealth--;
                RandomiseStats ();
                }

            time = Time.time;
            }

        private void RandomiseStats()
            {
            for (int i = 0; i < intStats.Length; i++)
                {
                intStats[i] = Random.Range (1, 100);
                }

            for (int i = 0; i < floatStats.Count; i++)
                {
                floatStats[i] = Random.Range (1, 100);
                }

            for (int i = 0; i < dictionaryStats.Count; i++)
                {
                dictionaryStats["Value " + i] = Random.Range (1, 100);
                }
            }

        private void RandmizeArray(ref int[] array)
            {
            array = new int[Random.Range (1, 3)];
            }
        }
    }