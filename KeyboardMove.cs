using UnityEngine;
using System.Collections;

namespace CharlieAssets.Dev 
{

[RequireComponent (typeof(CharacterController))]
[AddComponentMenu ("Control Script/Keyboard Move")]
public class KeyboardMove : MonoBehaviour {
    public float speed = 6.0f;
    public float gravity = -32.0f;

    private CharacterController _charControl;


	// Use this for initialization
	void Start () {
        _charControl = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charControl.Move(movement);

	}
}}
