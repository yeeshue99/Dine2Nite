using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public Animation menu;
    public GameObject closeMenu;

    public bool menuIsOnScreen = true;

    private ScreenChange sc;

    public void Start()
    {
        sc = GameObject.FindObjectOfType<ScreenChange>();

        if(menu.transform.localPosition.x <= 470)
        {
            menuIsOnScreen = false;
        }
        else
        {
            menuIsOnScreen = true;
        }
        MenuOut();
    }
    public void MenuIn()
    {
        if (!menuIsOnScreen)
        {
            closeMenu.SetActive(true);
            menu.Play("Menu_Slide_In");
            menuIsOnScreen = true;
        }
    }

    public void MenuOut()
    {
        if (menuIsOnScreen)
        {
            closeMenu.SetActive(false);
            menu.Play("Menu_Slide_Out");
            menuIsOnScreen = false;
        }
    }

    public void CallLoadScene(string sceneName)
    {
       sc.CallLoadScene(sceneName);
    }
}
