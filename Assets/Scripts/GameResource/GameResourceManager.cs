using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ProjectBS.GameResource
{
    public class GameResourceManager 
    {
        public void LoadBundle(string key)
        {
            AsyncOperationHandle downloadAsync = Addressables.DownloadDependenciesAsync(key);
            downloadAsync.Completed += DownloadAsync_Completed;
        }

        private void DownloadAsync_Completed(AsyncOperationHandle obj)
        {
            UnityEngine.Debug.Log(obj.Result);
        }

        public void LoadAsset<T>(string key) where T : UnityEngine.Object
        {
            AsyncOperationHandle downloadAsync = Addressables.LoadAssetAsync<T>(key);
            downloadAsync.Completed += DownloadAsync_Completed;
        }
    }
}