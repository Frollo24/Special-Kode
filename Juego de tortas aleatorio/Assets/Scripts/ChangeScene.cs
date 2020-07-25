using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    string nextScene = "SampleScene";


    //public void ChangeFightScene(string sSceneName)
    //{
    //    SceneManager.LoadScene(sSceneName, LoadSceneMode.Single);
    //}

    public void ChangeFightScene()
    {
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    public void SetNextScene(string snextScene)
    {
        nextScene = snextScene;
    }
}
