using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericChangeScene : MonoBehaviour
{
    private ScreenChange sc;
    // Start is called before the first frame update
    void Start()
    {
        sc = GameObject.FindObjectOfType<ScreenChange>();
    }

    public void CallLoadScene(string sceneName)
    {
        sc.CallLoadScene(sceneName);
    }
}
