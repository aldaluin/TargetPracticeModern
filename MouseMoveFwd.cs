using UnityEngine;
using System.Collections;

namespace CharlieAssets.Dev {

public class MouseMoveFwd : MonoBehaviour {
    public float speed = 15.0f;
    public float gravity = -32.0f;
    public int button = 1;

    private CharacterController _charControl;


    // Use this for initialization
    void Start()
    {
        _charControl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(button))
        {
            Vector3 movement = new Vector3(0 , gravity, speed);
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _charControl.Move(movement);
        }
    }
}}
