using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    //config params
    [SerializeField] int activeBlockCount; //set to private after debugging

    //cached reference
    SceneLoader mySceneLoader;

    private void Start()
    {
        mySceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void BlockCounter()
    {
        activeBlockCount++;
    }

    public void BlockSubtracter()
    {
        activeBlockCount--;
        if (activeBlockCount <= 0)
            mySceneLoader.LoadNextScene();
    }
}
