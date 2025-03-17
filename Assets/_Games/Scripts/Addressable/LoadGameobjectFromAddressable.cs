using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class LoadGameobjectFromAddressable : MonoBehaviour
{
    public AssetReference assetReference;

    void OnEnable()
    {
        AddressableUtilities.LoadGameobjectAsync(assetReference, Vector3.zero, (obj) =>
        {
            
        });
    }
}
