using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeManager : MonoBehaviour
{
    public ScreenChange screenChange;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(LoadApp());
    }

    private IEnumerator LoadApp()
    {
        yield return new WaitForSeconds(3);
        screenChange.CallLoadScene(1);
    }

    public void LoadPaymentScene(string sceneName)
    {
        screenChange.CallLoadScene(sceneName);
    }
}
