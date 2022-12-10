using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public ButtonType buttonType;
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
                break;
            case ButtonType.Run:
                if (PlayerController.Instance.isRun)
                    PlayerController.Instance.isRun = false;
                else
                    PlayerController.Instance.isRun = true;

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
    Run
}