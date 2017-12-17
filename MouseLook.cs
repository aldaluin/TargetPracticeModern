using UnityEngine;
using System.Collections;

namespace CharlieAssets.Dev {

public class MouseLook : MonoBehaviour {
    public enum RotationAxes
    {
        MouseXY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXY;
    public float SensitHoriz = 9.0f;
    public float SensitVert = 9.0f;
    public float MaxLookUp = 45.0f;
    public float MaxLookDown = 30.0f;

    private float _rotationX = 0;

	// Use this for initialization
	void Start () {
        // Turn off physics effects movement
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (axes == RotationAxes.MouseX)
        {
            // horizontal rotation
            transform.Rotate(0, Input.GetAxis("Mouse X") * SensitHoriz, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            // vertical rotation
            _rotationX -= Input.GetAxis("Mouse Y") * SensitVert;
            _rotationX = Mathf.Clamp(_rotationX, -MaxLookUp , MaxLookDown);
            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else
        {
            //both horizontal and vertical
            _rotationX -= Input.GetAxis("Mouse Y") * SensitVert;
            _rotationX = Mathf.Clamp(_rotationX, -MaxLookUp, MaxLookDown);
            float delta = Input.GetAxis("Mouse X") * SensitHoriz;
            float rotationY = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);

        }
    }
}}
