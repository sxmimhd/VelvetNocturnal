using UnityEngine;
using Unity.Cinemachine;

public class CameraConfinerSetup : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("CameraConfinerSetup started on: " + gameObject.name);

        CameraBounds bounds = FindAnyObjectByType<CameraBounds>();

        Debug.Log("Bounds: " + bounds);

        CinemachineConfiner2D confiner = GetComponent<CinemachineConfiner2D>();

        Debug.Log("Confiner: " + confiner);

        if (bounds == null || confiner == null)
            return;

        BoxCollider2D box = bounds.GetComponent<BoxCollider2D>();

        Debug.Log(box);

        confiner.BoundingShape2D = box;

        Debug.Log("Assigned!");

        confiner.InvalidateBoundingShapeCache();
    }
}