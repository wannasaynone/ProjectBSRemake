using System.Collections.Generic;
using UnityEngine;

namespace ProjectBS
{
    public class UnityTicker : MonoBehaviour
    {
        private static List<ITickable> tickableObjects = new List<ITickable>();

        public static void Add(ITickable tickable)
        {
            if (!tickableObjects.Contains(tickable))
            {
                tickableObjects.Add(tickable);
            }
        }

        public static void Remove(ITickable tickable)
        {
            tickableObjects.Remove(tickable);
        }

        private static UnityTicker instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
        }

        private void Update()
        {
            for (int i = 0; i < tickableObjects.Count; i++)
            {
                if (tickableObjects[i] == null)
                {
                    tickableObjects.RemoveAt(i);
                    i--;
                    continue;
                }

                tickableObjects[i].Tick(Time.deltaTime);
            }
        }
    }
}