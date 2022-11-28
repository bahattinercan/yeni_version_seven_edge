using UnityEngine;
using UnityEngine.UI;

public class ScoreBarController : MonoBehaviour
{
    [SerializeField] private Image fill;

    public void AddScore(float scoreIncrease)
    {
        fill.fillAmount += scoreIncrease;
    }

    public bool IsFinished()
    {
        bool isFinished = (fill.fillAmount < 1) ? false : true;
        return isFinished;
    }
}