using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public ButtonType buttonType;
    private Button btn;
    [SerializeField] private GameObject pauseMenu;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => GetMove());
    }

    public void GetMove()
    {
        switch (buttonType)
        {
            case ButtonType.Jump:
                PlayerController.Instance.Jump();
                break;

            case ButtonType.Crouch:
                PlayerController.Instance.Crouch();
                break;

            case ButtonType.Slide:
                PlayerController.Instance.Slide();
                break;

            case ButtonType.PutIn:
                PlayerController.Instance.GetVechicle();
                transform.parent.gameObject.SetActive(false);
                transform.parent.parent.GetChild(1).gameObject.SetActive(true);
                break;

            case ButtonType.Out:
                PlayerController.Instance.OutVechicle();
                transform.parent.parent.GetChild(0).gameObject.SetActive(true);
                transform.parent.gameObject.SetActive(false);
                transform.parent.parent.gameObject.SetActive(false);
                break;

            case ButtonType.Run:
                if (PlayerController.Instance.isRun)
                    PlayerController.Instance.isRun = false;
                else
                    PlayerController.Instance.isRun = true;

                break;
            case ButtonType.Try:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case ButtonType.Exit:
                if (SceneManager.GetActiveScene().buildIndex <= 1)
                    PlayerController.Instance.isPause = false;
                transform.parent.gameObject.SetActive(false);
                break;
            case ButtonType.Home:
                SceneManager.LoadScene(0);
                break;
            case ButtonType.Pause:
                if (SceneManager.GetActiveScene().buildIndex <= 1)
                    PlayerController.Instance.isPause = true;
                pauseMenu.SetActive(true);
                break;
            case ButtonType.Next:
                SceneManager.LoadScene(1);
                break;
        }
    }
}

public enum ButtonType
{
    Jump,
    Crouch,
    Slide,
    PutIn,
    Out,
    Run,
    Try,
    Home,
    Exit,
    Pause,
    Next
}