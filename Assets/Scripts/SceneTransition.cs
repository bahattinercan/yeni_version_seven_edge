using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animator closeAnimator;
    [SerializeField] private GameObject blackScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "door")
            Finished();
    }

    private void Finished()
    {
        blackScreen.SetActive(true);
        //closeAnimator.SetTrigger("close");
        StartCoroutine(CloseScene());
    }

    private IEnumerator CloseScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}