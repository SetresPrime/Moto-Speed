using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Main : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private WheelJoint2D _wheelJoint;
    [SerializeField]
    private float speed;

    private Vector3 tochPos;
    private Vector3 mousePos;
    private Vector3 cameraPos;
    private Vector3 BykePos;

    private bool isMobile;

    private JointMotor2D _motor;

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
            isMobile = true;
        _motor = _wheelJoint.motor;

    }
    void Update()
    {
        SetPos();

        MotorSpeed();

       if (Input.GetButton("Fire1") || Input.touchCount > 0)
            _wheelJoint.useMotor = true;
       else 
            _wheelJoint.useMotor = false;
    }
    private void SetPos()
    {
        tochPos = Input.touches[0].position;
        mousePos = Input.mousePosition;
        cameraPos = _camera.transform.position;
        BykePos = _camera.WorldToScreenPoint(transform.position);

        if (cameraPos.x != transform.position.x)
            _camera.transform.position = new Vector3(transform.position.x, cameraPos.y, cameraPos.z);
    }
    private void MotorSpeed()
    {
        _motor.motorSpeed = speed * ((isMobile ? tochPos.x : mousePos.x) > BykePos.x ? -1 : 1);
        _wheelJoint.motor = _motor;
    }
}
