using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public Animation menu;
    public GameObject closeMenu;

    bool menuIsOut = true;

    public void Start()
    {
        MenuOut();
    }
    public void MenuIn()
    {
        if (menuIsOut)
        {
            closeMenu.SetActive(true);
            menu.Play("MenuIn");
            menuIsOut = false;
        }
    }

    public void MenuOut()
    {
        if (!menuIsOut)
        {
            closeMenu.SetActive(false);
            menu.Play("MenuOut");
            menuIsOut = true;
        }
    }
}
