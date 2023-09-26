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

        private Data.CharacterData referenceCharacterData;

        public void ShowWith(CombatActor combatActor)
        {
            referenceCharacterData = combatActor.GetSource();

            AsyncOperationHandle downloadAsync = Addressables.LoadAssetAsync<Sprite>(referenceCharacterData.Address);
            downloadAsync.Completed += DownloadAsync_Completed;
        }

        public void Hide()
        {
            transform.parent.gameObject.SetActive(false);
        }

        private void DownloadAsync_Completed(AsyncOperationHandle obj)
        {
            transform.parent.gameObject.SetActive(true);
            characterImage.sprite = obj.Result as Sprite;
            characterImage.SetNativeSize();
            characterImage.transform.localPosition = new Vector3(referenceCharacterData.OffsetX, referenceCharacterData.OffsetY);
        }
    }
}