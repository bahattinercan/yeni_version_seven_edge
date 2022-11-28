using UnityEngine;
using UnityEngine.UI;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private Image fillEnergy;
    [SerializeField] private float fillAmount;

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
}