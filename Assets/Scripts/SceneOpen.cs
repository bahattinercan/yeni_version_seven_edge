using System.Collections;
using UnityEngine;

public class SceneOpen : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DisableBlackScreen());
    }

    private IEnumerator DisableBlackScreen()
    {
        yield return new WaitForSeconds(1.55f);
        gameObject.SetActive(false);
    }
}