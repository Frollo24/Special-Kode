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

    //Loads the next scene (the one defined in setNextScene)
    public void ChangeScenes()
    {
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    //Makes nextScene be the name of the scene we want to be the next.
    public void SetNextScene(string snextScene)
    {
        nextScene = snextScene;
    }
}
