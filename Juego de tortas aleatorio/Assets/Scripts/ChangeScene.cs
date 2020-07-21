using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeFightScene(string sSceneName)
    {
        SceneManager.LoadScene(sSceneName, LoadSceneMode.Single);
    }
}
