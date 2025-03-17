using System.Collections;
using System.Collections.Generic;
//using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Profiling;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class UITestController : PersistentSingleton<UITestController>
{
    [SerializeField] private Button _btnOnOffPanel;
    [SerializeField] private GameObject _panel;

    [Header("InputField")]
    [SerializeField] private TMP_InputField _inputField;

    [Header("Buttons")]
    [SerializeField] private Button _btnLoadScene;
    [SerializeField] private Button _btnLoadResource;
    [SerializeField] private Button _btnReleaseResource;

    [SerializeField] private Button _btnLoadAddressable;
    [SerializeField] private Button _btnReleaseAddressable;

    [SerializeField] private Button _btnCheckSize;
    [SerializeField] private Button _btnDownload;

    [SerializeField] private Image _image;

    private AsyncOperationHandle<Sprite> _operationHandle;

    private void Start()
    {
        Debug.LogError(Application.persistentDataPath);
        Caching.ClearCache();

        _btnOnOffPanel.onClick.RemoveAllListeners();
        _btnOnOffPanel.onClick.AddListener(() =>
        {
            _panel.SetActive(!_panel.activeInHierarchy);
        });

        _btnLoadScene.onClick.RemoveAllListeners();
        _btnLoadScene.onClick.AddListener(() =>
        {
            SceneController.instance.LoadScene(GetInputFieldText());
        });

        _btnLoadResource.onClick.RemoveAllListeners();
        _btnLoadResource.onClick.AddListener(() =>
        {

            _image.sprite = Resources.Load<Sprite>(GetInputFieldText());
            _image.SetNativeSize();
        });

        _btnReleaseResource.onClick.RemoveAllListeners();
        _btnReleaseResource.onClick.AddListener(() =>
        {
            _image.sprite = null;
            Resources.UnloadUnusedAssets();
        });

        _btnLoadAddressable.onClick.RemoveAllListeners();
        _btnLoadAddressable.onClick.AddListener(() =>
        {
            _operationHandle = AddressableUtilities.LoadAsync<Sprite>(GetInputFieldText(), (sprite) =>
            {
                _image.sprite = sprite;
                _image.SetNativeSize();
            });

        });

        _btnReleaseAddressable.onClick.RemoveAllListeners();
        _btnReleaseAddressable.onClick.AddListener(() =>
        {
            AddressableUtilities.ReleaseAsset(_image.sprite);
            _image.sprite = null;
        });

        _btnCheckSize.onClick.RemoveAllListeners();
        _btnCheckSize.onClick.AddListener(() =>
        {
            AddressableUtilities.GetDownloadSizeAsync(GetInputFieldText(), (size) =>
            {
                Debug.Log(size);
            }, () =>
            {
                Debug.LogError("GetDownloadSizeAsync fail!");
            });
        });

        _btnDownload.onClick.RemoveAllListeners();
        _btnDownload.onClick.AddListener(() =>
        {
            AddressableUtilities.DownloadAssetFromCloud(GetInputFieldText(), (p)=>
            {
                Debug.Log($"Downloading {p}");
            }, ()=>
            {
                Debug.Log($"Downloaded {GetInputFieldText()}");
            }, ()=>
            {
                Debug.LogError("DownloadAssetFromCloud fail!");
            });
        });
    }
     
    private void Update() {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Profiler.BeginSample("Test");
            List<string> list = new List<string>();
            for (int i = 0; i < 199999; i++)
            {
                list.Add("Test");
            }
            Profiler.EndSample();
        }
    }

    private string GetInputFieldText()
    {
        return _inputField.text;
    }

    // public void Test()
    // {
    //     string output = JsonConvert.SerializeObject("");
    // }
}
