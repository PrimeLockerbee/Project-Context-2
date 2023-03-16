using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousemovement : MonoBehaviour
{
    public float RotationSpeed = 5;
    private Vector3 turn;

    // Update is called once per frame
    void Update()
    {
        turn = new Vector3(Input.GetAxis("Mouse X") * RotationSpeed, 0, 0);
        transform.localEulerAngles.Set(turn.x,turn.y,turn.z);

    }
}
