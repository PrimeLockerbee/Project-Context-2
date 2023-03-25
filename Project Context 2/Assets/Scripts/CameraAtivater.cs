using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAtivater : MonoBehaviour
{
    private Camera myCamera;
    public float cameraDuration = 3.0f;

    private bool cameraActive = false;

    private void Start()
    {
        myCamera = GameObject.Find("EndingCamera").GetComponent<Camera>();
    }

    public void ActivateCamera()
    {
        if (!cameraActive)
        {
            cameraActive = true;
            StartCoroutine(ActivateCameraCoroutine());
        }
    }

    private IEnumerator ActivateCameraCoroutine()
    {
        myCamera.enabled = true;
        yield return new WaitForSeconds(cameraDuration);
        myCamera.enabled = false;
        cameraActive = false;
    }
}
