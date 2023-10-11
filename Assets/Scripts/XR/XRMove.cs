using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class XRMove : MonoBehaviour
{
    public float force = 10;

    public InputAction move;

    Vector2 leftJoystick => move.ReadValue<Vector2>();
    Rigidbody body;
    bool isUp;
    bool forceAdd;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    public void OnEnable()
    {
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    private void Update()
    {
        //Debug.Log(leftJoystick);
        if (!isUp && leftJoystick.y > 0.75f)
            isUp = true;
        if (isUp && leftJoystick.y < 0.4f)
        {
            isUp = false;
            forceAdd = false;
        }
    }

    private void FixedUpdate()
    {
        if (body.isKinematic)
            return;

        if (isUp && !forceAdd)
        {
            body.AddForce(Vector3.up * force, ForceMode.Impulse);
            forceAdd = true;
        }
    }
}
