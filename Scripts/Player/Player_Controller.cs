using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // needed for the input system asset in unity

public class Player_Controller : MonoBehaviour
{
    public float moveSpeed = 1f; // move speed which can be assigned in the unity inspector.
    [SerializeField] private Vector2 moveDirection; // private variable to assign vector2 in OnMove function
    [SerializeField] private Rigidbody2D _rb; // private rigidbody which will be assigned in start

    public bool playerCanMove; // bool variable which will be used later on to control whether the player is allowed to move or not
    //could move this bool into its own script which keeps track of player variables needed to be altered by many different scripts

    // creating vector2 array to store values of 8 directions
    private readonly Vector2[] directions = {
        new Vector2(0, 0).normalized, // Idle
        new Vector2(1, 0).normalized, // Right
        new Vector2(1, 1).normalized, // Up-Right
        new Vector2(0, 1).normalized, // Up
        new Vector2(-1, 1).normalized, // Up-Left
        new Vector2(-1, 0).normalized, // Left
        new Vector2(-1, -1).normalized, // Down-Left
        new Vector2(0, -1).normalized, // Down
        new Vector2(1, -1).normalized // Down-Right
    };

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); //assigning rigidbody2d attached to gameObject the script is on.
        playerCanMove = true; // giving bool the value of true as it sets itself to false unless specified. at the moment there is nothing to stop the player from moving.
    }

    //fixed update calls once per frame.
    private void FixedUpdate()
    {
        if (playerCanMove)
        {
            //player movement
            Vector2 movement = moveDirection * moveSpeed * Time.fixedDeltaTime; //creating instance variable to store vector2 movement.
            _rb.position += movement; // adding the movement variable to the rigidbody2d position co ordinates
        }
        
    }

    private void OnMove(InputValue value)
    {
        
        moveDirection = value.Get<Vector2>().normalized; //assigning moveDirection vector2 with value obtained from Input System.
        moveDirection = QuantizeDirection(moveDirection);
        Debug.Log("moveDirection is" + moveDirection); // sending debug of movedirection x and y values

    }

    private Vector2 QuantizeDirection(Vector2 input)
    {
        float maxDot = -Mathf.Infinity;
        Vector2 bestMatch = Vector2.zero;

        foreach (var direction in directions)
        {
            float dot = Vector2.Dot(input, direction);
            if (dot > maxDot)
            {
                maxDot = dot;
                bestMatch = direction;
            }
        }

        return bestMatch;
    }
}

