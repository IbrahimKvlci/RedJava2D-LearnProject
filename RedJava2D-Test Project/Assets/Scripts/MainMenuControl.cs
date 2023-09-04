using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField] GameObject[] levels,locks;
    void Start()
    {
        foreach (var level in levels)
        {
            level.SetActive(false);
            level.GetComponent<Button>().interactable = false;
        }

    }

    void Update()
    {
        
    }

    public void GetLevelButtons()
    {
        foreach (var level in levels)
        {
            level.SetActive(true);
        }
        for (int i = 0; i < PlayerPrefs.GetInt("levelcount"); i++)
        {
            levels[i].GetComponent<Button>().interactable = true;
            locks[i].SetActive(false);
        }
    }

    public void LoadScene(int sceneIndex)
    {
        if(sceneIndex == 0)
        {
            sceneIndex = PlayerPrefs.GetInt("levelcount");
        }
        SceneManager.LoadScene(sceneIndex);
    }
}
