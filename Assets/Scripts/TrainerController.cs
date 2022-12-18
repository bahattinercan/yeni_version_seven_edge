using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainerController : MonoBehaviour
{
    [SerializeField] Image sliderImage;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private void Update()
    {
        if (sliderImage.fillAmount >= 1)
        {
            winPanel.SetActive(true);
        }
        else if (sliderImage.fillAmount <= 0)
        {
            losePanel.SetActive(true);
        }
    }
}
