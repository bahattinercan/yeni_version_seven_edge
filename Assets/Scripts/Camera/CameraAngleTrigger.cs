using UnityEngine;

public class CameraAngleTrigger : MonoBehaviour
{
    [SerializeField] private CameraAngle switchToAngle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VirtualCameraController.Instance.ChangeCameraAngle(switchToAngle);
        }
    }
}