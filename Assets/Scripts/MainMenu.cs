using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    public int ScenesIndex;

    public void PlayGame()
    {
        ScenesTransition.SwitchToScenes(ScenesIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
