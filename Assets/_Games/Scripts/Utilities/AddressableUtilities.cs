using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public static class AddressableUtilities
{
    public static T LoadSync<T>(AssetReference assetRef)
    {
        if (assetRef.OperationHandle.IsValid())
        {
            if (assetRef.OperationHandle.IsDone)
            {
                return (T)assetRef.OperationHandle.Result;
            }
            return (T)assetRef.OperationHandle.WaitForCompletion();
        }
        AsyncOperationHandle<T> handle = assetRef.LoadAssetAsync<T>();

        return handle.WaitForCompletion();

    }

    public static void LoadAsync<T>(AssetReference assetRef, Action<T> onLoaded)
    {
        if (assetRef.OperationHandle.IsValid())
        {
            if (!assetRef.OperationHandle.IsDone)
            {
                assetRef.OperationHandle.Completed += (loadRequest) =>
                {
                    if (loadRequest.IsValid() && loadRequest.Result != null)
                    {
                        onLoaded?.Invoke((T)loadRequest.Result);
                    }
                };
            }
            else
            {
                onLoaded?.Invoke((T)assetRef.OperationHandle.Result);
            }
        }
        else
        {
            assetRef.LoadAssetAsync<T>().Completed += (loadRequest) =>
            {
                if (loadRequest.IsValid() && loadRequest.Result != null)
                {
                    onLoaded?.Invoke(loadRequest.Result);
                }
            };
        }
    }

    public static void LoadSprite(AssetReference assetRef, SpriteRenderer spriteRenderer, Action<Sprite> onLoaded = null)
    {
        spriteRenderer.enabled = false;
        LoadAsync<Sprite>(assetRef, (r) =>
        {
            if (spriteRenderer == null)
            {
                ReleaseAsset(assetRef);
                return;
            }

            spriteRenderer.enabled = true;
            spriteRenderer.sprite = r;
            onLoaded?.Invoke(r);
        });
    }

    public static void LoadSprite(AssetReference assetRef, Image image, Action<Sprite> onLoaded = null)
    {
        if (image != null)
        {
            image.enabled = false;
        }
        LoadAsync<Sprite>(assetRef, (r) =>
        {
            if (image == null)
            {
                ReleaseAsset(assetRef);
                return;
            }

            image.enabled = true;
            image.sprite = r;
            onLoaded?.Invoke(r);
        });
    }

    public static AsyncOperationHandle<T> LoadAsync<T>(string key, Action<T> onLoaded)
    {
        var handle = Addressables.LoadAssetAsync<T>(key);

        if (handle.IsValid())
        {
            if (!handle.IsDone)
            {
                handle.Completed += (loadRequest) =>
                {
                    if (loadRequest.IsValid() && loadRequest.Result != null)
                    {
                        onLoaded?.Invoke((T)loadRequest.Result);
                    }
                };
            }
            else
            {
                onLoaded?.Invoke(handle.Result);
            }
        }

        return handle;
    }

    public static T Load<T>(string key)
    {
        return Addressables.LoadAssetAsync<T>(key).WaitForCompletion();
    }

    public static void LoadGameobjectAsync(AssetReference addressablePrefab, Vector3 pos, Action<GameObject> callback = null)
    {
        var handle = addressablePrefab.InstantiateAsync(pos, Quaternion.identity);
        handle.Completed += (_) =>
        {
            var p = _.Result;

            p.SetActive(true);
            callback?.Invoke(p);
        };
    }

    public static void LoadGameobjectAsync(AssetReference addressablePrefab, Transform parent, Action<GameObject> callback = null)
    {
        var handle = addressablePrefab.InstantiateAsync(parent);
        handle.Completed += (_) =>
        {
            if (parent == null)
            {
                ReleaseInstance(_.Result);
                return;
            }
            var p = _.Result;

            p.SetActive(true);
            callback?.Invoke(p);
        };
    }

    public static GameObject LoadGameobjectSync(AssetReference addressablePrefab, Transform parent)
    {
        var handle = addressablePrefab.InstantiateAsync(parent);
        return handle.WaitForCompletion();
    }

    public static GameObject LoadGameobjectSync(AssetReference addressablePrefab, Vector3 pos)
    {
        var handle = addressablePrefab.InstantiateAsync(pos, Quaternion.identity);
        return handle.WaitForCompletion();
    }


    public static void ReleaseInstance(GameObject gObj)
    {
        if (gObj == null)
        {
            return;
        }
        if (!Addressables.ReleaseInstance(gObj))
        {
            UnityEngine.Object.Destroy(gObj);
        }
        gObj = null;
    }

    public static void ReleaseAsset(AssetReference assetReference)
    {
        if (assetReference != null && assetReference.IsValid())
        {
            assetReference.ReleaseAsset();
        }
    }

    public static void ReleaseAsset(AsyncOperationHandle asyncOperationHandle)
    {
        Addressables.Release(asyncOperationHandle);
    }

}
