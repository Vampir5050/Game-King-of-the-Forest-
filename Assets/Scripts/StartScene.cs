using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public void NewScene()
    {
        SceneManager.LoadScene(1);
    }
}
