using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // needed for the input system asset in unity

public class Player_Controller : MonoBehaviour
{
    public float moveSpeed = 1f; // move speed which can be assigned in the unity inspector.
    private Vector2 moveDirection; // private variable to assign vector2 in OnMove function
    private Rigidbody2D _rb; // private rigidbody which will be assigned in start 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMove(InputAction input)
    {
        
    }
}
