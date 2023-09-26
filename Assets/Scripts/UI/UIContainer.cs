using UnityEngine;

namespace ProjectBS.UI
{
    public class UIContainer : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] uiCollection;

        public T Get<T>() where T : MonoBehaviour
        {
            for (int i = 0; i < uiCollection.Length; i++)
            {
                if (uiCollection[i].GetType() == typeof(T))
                {
                    return uiCollection[i] as T;
                }
            }

            return null;
        }
    }
}