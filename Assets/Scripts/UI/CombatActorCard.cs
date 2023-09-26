using ProjectBS.Combat;
using ProjectBS.GameResource;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace ProjectBS.UI
{
    public class CombatActorCard : MonoBehaviour
    {
        [SerializeField] private Image characterImage;

        public void ShowWith(CombatActor combatActor)
        {
            AsyncOperationHandle downloadAsync = Addressables.LoadAssetAsync<Sprite>("Test" + Random.Range(1, 6));
            downloadAsync.Completed += DownloadAsync_Completed;
            transform.parent.gameObject.SetActive(true);
        }

        public void Hide()
        {
            transform.parent.gameObject.SetActive(false);
        }

        private void DownloadAsync_Completed(AsyncOperationHandle obj)
        {
            characterImage.sprite = obj.Result as Sprite;
            characterImage.SetNativeSize();
        }
    }
}