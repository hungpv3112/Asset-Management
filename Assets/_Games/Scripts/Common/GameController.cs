
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : PersistentSingleton<GameController>
{


    private void OnGUI() {
        Scene scene = SceneManager.GetActiveScene();
        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(0, 0, 200, 200);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = Screen.height * 2 / 50;
        style.normal.textColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        string text = scene.name;
        GUI.Label(rect, text, style);
    }
}
