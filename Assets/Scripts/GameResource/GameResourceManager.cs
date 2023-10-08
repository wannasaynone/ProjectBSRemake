using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ProjectBS.GameResource
{
    public static class GameResourceManager 
    {
        public static void LoadAsset<T>(string key, System.Action<T> onLoaded) where T : UnityEngine.Object
        {
            AsyncOperationHandle<T> downloadAsync = Addressables.LoadAssetAsync<T>(key);
            downloadAsync.Completed += delegate (AsyncOperationHandle<T> asyncOperation)
            {
                onLoaded?.Invoke(asyncOperation.Result);
            };
        }
    }
}