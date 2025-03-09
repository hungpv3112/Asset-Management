using UnityEngine;

public static class LayerUtils
{
    public static int GetLayerIndexFromLayerName(string layerName)
    {
        return LayerMask.NameToLayer(layerName);
    }

    public static int GetLayerMaskFromLayerName(string layerName)
    {
        return 1 << GetLayerIndexFromLayerName(layerName);
    }

    public static int GetLayerIndexFromLayerMask(LayerMask layerMask)
    {
        return (int)Mathf.Log(layerMask.value, 2);
    }

    public static bool CompareLayer(int layerIndex1, int layerIndex2)
    {
        return 1 << layerIndex1 == 1 << layerIndex2;
    }

    public static bool CompareLayer(int layer, LayerMask layerMask)
    {
        if (((1 << layer) & layerMask) != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
