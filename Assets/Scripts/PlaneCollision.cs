using UnityEngine;

public class PlaneCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("collectableFruits") || other.gameObject.CompareTag("collectableClothes"))
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}