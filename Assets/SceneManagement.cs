using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchScene(string sceneName){
      SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
      SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        
    }
}
