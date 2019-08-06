using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersists : MonoBehaviour
{
    int startingSceneIndex, currentSceneIndex;

    private void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersists>().Length;
        if(numScenePersists > 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }

         }

    private void Start()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    // Update is called once per frame
    void Update()
    {
         currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex != startingSceneIndex)
        {
            Debug.Log("GO is Destroyed!");
            Destroy(gameObject);
        }
    }
}
