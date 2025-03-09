using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class AddressableInit
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Init()
    {
        LogUtils.LogError("AddressableInit");
        Addressables.InitializeAsync().Completed += OnInitializeComplete;
    }

    static void OnInitializeComplete(AsyncOperationHandle<IResourceLocator> handle)
    {
        LogUtils.LogError($"OnInitializeComplete: {handle.Status}");
    }

}
