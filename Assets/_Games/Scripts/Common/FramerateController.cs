using UnityEngine;

public class FramerateController : PersistentSingleton<FramerateController>
{
    protected override void Awake()
    {
        base.Awake();
        int frameRate = (int)Screen.currentResolution.refreshRateRatio.value;
        if (frameRate > 60)
        {
            if (frameRate % 90 == 0)
            {
                frameRate = 90;
            }else
            {
                frameRate = 60;
            }
        }
        SetTargetFrameRate(frameRate);
    }

    public void SetTargetFrameRate(int targetFrameRate)
    {
        Application.targetFrameRate = targetFrameRate;
        LogUtils.LogError("Application.targetFrameRate " + Application.targetFrameRate);
    }
}
