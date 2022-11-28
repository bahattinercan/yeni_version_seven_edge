using Cinemachine;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    public static VirtualCameraController Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera leftCam, rightCam, frontCam, backCam, closeCam;

    [SerializeField] private CameraAngle currentAngle = CameraAngle.Back;

    private CinemachineVirtualCamera GetAngleCamera => currentAngle switch
    {
        CameraAngle.Front => frontCam,
        CameraAngle.Left => leftCam,
        CameraAngle.Right => rightCam,
        CameraAngle.Close => closeCam,
        CameraAngle.Back => backCam,
        _ => backCam
    };

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        leftCam.Priority = -10;
        rightCam.Priority = -10;
        frontCam.Priority = -10;
        backCam.Priority = -10;
        closeCam.Priority = -10;

        GetAngleCamera.Priority = 10;
    }

    public void ChangeCameraAngle(CameraAngle newAngle)
    {
        currentAngle = newAngle;

        UpdateCamera();
    }
}