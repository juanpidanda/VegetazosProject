using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenusBehaviour : MonoBehaviour
{
    public GameObject pausePopUpMenu;


    [HideInInspector]
    public List<GameObject> openedMenusList;

    private void Awake()
    {
        pausePopUpMenu.SetActive(false);
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
