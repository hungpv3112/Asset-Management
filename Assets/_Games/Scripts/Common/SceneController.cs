using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : NMSingleton<SceneController>
{
    private SceneState _preSceneState;
    public SceneState PreSceneState
    {
        get => _preSceneState;
    }

    private SceneState _currentSceneState;

    public SceneState CurrentSceneState
    {
        get => _currentSceneState;
    }

    public Action<SceneState> onChangeSceneState;

    protected override void Init()
    {
        _currentSceneState = SceneState.None;
    }

    private bool SetSceneState(SceneState sceneState)
    {
        _preSceneState = _currentSceneState;
        _currentSceneState = sceneState;
        onChangeSceneState?.Invoke(_currentSceneState);
        LogUtils.LogError("SetSceneState " + _currentSceneState);

        return true;

    }

    public void LoadMenu()
    {
        if (CurrentSceneState == SceneState.Menu && _isLoading)
        {
            return;
        }
        if (GameController.HasInstance)
        {
            GameController.Instance.StartCoroutine(CoroutineLoadScene(SceneState.Menu, "Menu"));	
        }
    }

    public void LoadLevel()
    {
        if (CurrentSceneState == SceneState.Level && _isLoading)
        {
            return;
        }
        if (GameController.HasInstance)
        {
            GameController.Instance.StartCoroutine(CoroutineLoadScene(SceneState.Level, "BaseLevel"));
        }
    }


    private AsyncOperation asyncScene;

    public Action<float> onLoadingScene;

    public Action<SceneState> onLoadingSceneDone;

    public const float MAX_TIME_WAIT_ANIMATION_LOADING = 1f;

    public bool IsFirstLoading()
    {
        return PreSceneState == SceneState.None && CurrentSceneState == SceneState.Menu;
    }


    private bool _isLoading = false;

    private IEnumerator CoroutineLoadScene(SceneState sceneState, string sceneName)
    {
        if (_isLoading)
        {
            while (_isLoading)
            {
                yield return null;
            }
        }


        _isLoading = true;
        if (!SetSceneState(sceneState))
        {
            yield break;
        }

        //Wait Anim Loading
        if (!IsFirstLoading())
        {
            yield return new WaitForSeconds(MAX_TIME_WAIT_ANIMATION_LOADING);
        }

        var progress = 0f;
        AsyncOperation asyncLoad = null;

        asyncLoad = LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;            
        
        while (progress <= 1f)
        {
            progress += Time.deltaTime;
            onLoadingScene?.Invoke(progress);
 
            yield return null;
        }

        while (!asyncLoad.isDone)
        {
            asyncLoad.allowSceneActivation = true;
            yield return null;
        }

        _isLoading = false;
        yield return null;
        onLoadingSceneDone?.Invoke(sceneState);
    }


    public void LoadScene(string name, LoadSceneMode mode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(name, mode);
    }

    public AsyncOperation LoadSceneAsync(string name, LoadSceneMode mode = LoadSceneMode.Single)
    {
        asyncScene = SceneManager.LoadSceneAsync(name, mode);
        return asyncScene;
    }

    public AsyncOperation LoadSceneAsync(int index, LoadSceneMode mode = LoadSceneMode.Single)
    {
        asyncScene = SceneManager.LoadSceneAsync(index, mode);
        return asyncScene;
    }

    public void UnloadSceneAsync(int index, Action callback)
    {
        var async = SceneManager.UnloadSceneAsync(index);
        async.completed += (r) =>
        {
            callback?.Invoke();
        };
    }

    public void UnloadSceneAsync(string name, Action callback)
    {
        var async = SceneManager.UnloadSceneAsync(name);
        async.completed += (r) =>
        {
            callback?.Invoke();
        };
    }
}


public enum SceneState
{
    None,
    Menu,
    Level,
    PVP,
    Preview
}

