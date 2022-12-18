using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameMenuButton : MonoBehaviour
{
    public MenuButtonType buttonType;
    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => GetMove());
    }

    public void GetMove()
    {
        switch (buttonType)
        {
            case MenuButtonType.Play:
                SceneManager.LoadScene(1);
                break;
            case MenuButtonType.Exit:
                Application.Quit();
                break;
        }
    }
}

public enum MenuButtonType
{
    Play,
    Shop,
    Setting,
    Help,
    Story,
    Exit
}