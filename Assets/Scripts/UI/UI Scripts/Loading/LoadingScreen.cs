using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{

    //双模式，canvas/Scene按方法调用
    public void StartLoadingScene(string sceneName)
    {
        //确保 Loading UI 处于显示状态
        gameObject.SetActive(true);

        //启动加载协程
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public void StartLoadingCanvas(GameObject targetCanvas)
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadCanvasAsync(targetCanvas));
    }

    private IEnumerator LoadCanvasAsync(GameObject targetCanvas)
    {

        yield return null; 
        yield return null; 

        // 打开目标 Canvas
        if (targetCanvas != null)
        {
            targetCanvas.SetActive(true);
        }

        // 关闭 Loading UI
        gameObject.SetActive(false);
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // 开始异步加载，但不允许它自动激活
        // 这样 Unity 会在后台默默加载资源，而当前画面会停留在 Loading UI
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

 
        asyncOperation.allowSceneActivation = true;

        // 等待场景真正激活完毕
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        // 新场景已经成功打开，Loading UI 自己把自己关掉
        gameObject.SetActive(false);
    }

}
