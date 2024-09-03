using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadGame : MonoBehaviour
{

    public void LoadGames()
    {
        SaveScript load = GetComponent<SaveScript>();
        load.Load();
        StartScene start = GetComponent<StartScene>();
        start.NewScene();
    }
}
