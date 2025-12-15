using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesTransition : MonoBehaviour
{
    public Text loadingPercentage;
    public Image loadingProgressBar;

    private static ScenesTransition instance;
    private static bool shouldPlayOpeningAnimation = false;

    private Animator animator;
    private AsyncOperation loadingSceneOperation;

    public static void SwitchToScenes(int ScenesIndex)
    {
        instance.animator.SetTrigger("TransitionStart");
        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(ScenesIndex); //Загрузка сцены
        instance.loadingSceneOperation.allowSceneActivation = false; //Запрет на переключение сцены после ее загрузки
    }

    void Start()
    {
        instance = this; //Записывает сам себя в глобальную переменную чтобы можно было вызвать статический SwitchToScenes из любого скрипта
        animator = GetComponent<Animator>();

        if (shouldPlayOpeningAnimation)
        {
            animator.SetTrigger("TransitionEnd");
        }
    }

    void Update()
    {
        if(loadingSceneOperation != null)
        {
            loadingPercentage.text = Mathf.RoundToInt(loadingSceneOperation.progress * 100) + "%";
            loadingProgressBar.fillAmount = loadingSceneOperation.progress;
        }
    }

    public void OnAnimationOver()
    {
        shouldPlayOpeningAnimation = true;
        loadingSceneOperation.allowSceneActivation = true;
    }
}
