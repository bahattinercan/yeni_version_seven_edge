using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private Image fillEnergy;
    [SerializeField] private float fillAmount;
    [SerializeField] private GameObject carButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "collectableClothes")
        {
            CollectClothes(other.gameObject);
            HideCollactable(other.gameObject);
        }
        else if (other.tag == "collectableFruits")
        {
            CollectFruits(fillAmount);
            HideCollactable(other.gameObject);
        }
        else if (other.tag == "Cars")
        {
            carButton.SetActive(true);
            carButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = other.name;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cars")
        {
            carButton.SetActive(false);
        }
    }

    private void CollectClothes(GameObject UIToBeEnabled)
    {
        UIToBeEnabled.GetComponent<Collectable>().EnableClothesUI();
    }

    private void CollectFruits(float amount)
    {
        fillEnergy.fillAmount += amount;
    }

    private void HideCollactable(GameObject other)
    {
        other.tag = "Untagged";
        other.gameObject.SetActive(false);
    }

    public void Energy(float loseEnergy)
    {
        fillEnergy.fillAmount -= loseEnergy;
    }
}