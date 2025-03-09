using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        UpdateFPS();
    }

    public float updateInterval = 0.1f;

    private float accum = 0.0f; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval
    private float fps = 0.0f; // Current FPS

    void UpdateFPS()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            fps = accum / frames;
            // If you're using Unity UI Text, set the text
            // fpsText.text = format;

            // Debug.Log(format); // If you want to see the FPS in the console

            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(0, 0, Screen.width, Screen.height * 0.02f);
        style.alignment = TextAnchor.UpperRight;
        style.fontSize = Screen.height * 2 / 100;
        style.normal.textColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        string text = System.String.Format("{0} FPS", (int)fps);
        GUI.Label(rect, text, style);
    }
}
