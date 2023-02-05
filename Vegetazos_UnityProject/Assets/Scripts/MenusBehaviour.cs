using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenusBehaviour : MonoBehaviour
{
    public GameObject pausePopUpMenu;
    public GameObject howToPlayScreenMenu;


    [HideInInspector]
    public List<GameObject> openedMenusList;

    private void Awake()
    {
        if(pausePopUpMenu != null)
        {
            pausePopUpMenu.SetActive(false);
        }
        if (howToPlayScreenMenu != null)
        {
            howToPlayScreenMenu.SetActive(false);
        }
        openedMenusList.Clear();
    }

    public void OpenMenu(GameObject menuToOpen)
    {
        if (!openedMenusList.Contains(menuToOpen))
        {
            menuToOpen.SetActive(true);
            openedMenusList.Add(menuToOpen);
        }
    }
    public void CloseMenu(GameObject menuToClose)
    {
        menuToClose.SetActive(false);
        if (openedMenusList.Contains(menuToClose))
        {
            openedMenusList.Remove(menuToClose);
        }
    }
}
