using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // needed for the input system asset in unity

public class Player_Controller : MonoBehaviour
{
    public float moveSpeed = 1f; // move speed which can be assigned in the unity inspector.
    [SerializeField] private Vector2 moveDirection; // private variable to assign vector2 in OnMove function
    [SerializeField] private Rigidbody2D _rb; // private rigidbody which will be assigned in start 

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); //assigning rigidbody2d attached to gameObject the script is on.
        
    }

    //fixed update calls once per frame.
    private void FixedUpdate()
    {
        //player movement

    }

    private void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>().normalized; //assigning moveDirection vector2 with value obtained from Input System.
        Debug.Log("moveDirection is" + moveDirection);

    }
}
