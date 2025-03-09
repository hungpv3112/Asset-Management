using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLoadNextSceneFromSceneLoadingData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        IEnumerator CoroutineLoadScene()
        {
            yield return new WaitForSeconds(0.5f);
            SceneController.instance.LoadLevel();
        }

        StartCoroutine(CoroutineLoadScene());

    }

}
