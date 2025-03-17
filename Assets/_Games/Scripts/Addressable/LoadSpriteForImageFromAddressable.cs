using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class LoadSpriteForImageFromAddressable : MonoBehaviour
{
    public AssetReference assetReference;

    void OnEnable()
    {
        AddressableUtilities.LoadAsync<Sprite>(assetReference, (sprite) =>
        {
            GetComponent<Image>().sprite = sprite;
        });
    }
}
