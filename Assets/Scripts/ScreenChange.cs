using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenChange : MonoBehaviour
{
    public Animator transition;
    [Range(0,2)]
    public float transitionTime = 0.5f;

    private void Start()
    {
        if (!transition.gameObject.activeSelf)
        {
            transition.gameObject.SetActive(true);
        }
    }

    public void CallLoadScene(int sceneNum)
    {
        StartCoroutine(LoadScene(sceneNum));
    }

    public void CallLoadScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    private IEnumerator LoadScene(int sceneNum)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneNum);
    }

    private IEnumerator LoadScene(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
