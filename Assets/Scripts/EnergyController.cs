using UnityEngine;
using UnityEngine.UI;

public class EnergyController : MonoBehaviour
{
    public Image fill;
    public float decreasePerSecond;

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("DecreaseEnergy", 0, 1f);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void DecreaseEnergy()
    {
        fill.fillAmount -= decreasePerSecond;
    }
}