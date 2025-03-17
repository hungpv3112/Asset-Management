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

    void OnDisable()
    {
        var image = GetComponent<Image>();
        AddressableUtilities.ReleaseAsset(image.sprite);
        image.sprite = null;
    }
}
