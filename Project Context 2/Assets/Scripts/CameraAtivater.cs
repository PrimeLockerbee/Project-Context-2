using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAtivater : MonoBehaviour
{
    public Camera myCamera;
    public Camera playerCamera;

    public float cameraDuration = 3.0f;

    private bool cameraActive = false;

    private void Start()
    {
        ActivateCamera();
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
        playerCamera.enabled = false;
        yield return new WaitForSeconds(cameraDuration);
        myCamera.enabled = false;
        playerCamera.enabled = true;
        cameraActive = false;
        this.gameObject.SetActive(false);
    }
}
