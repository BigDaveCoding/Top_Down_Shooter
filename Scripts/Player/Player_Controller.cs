using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // needed for the input system asset in unity

public class Player_Controller : MonoBehaviour
{
    public float moveSpeed = 1f; // move speed which can be assigned in the unity inspector.
    [SerializeField] private Vector2 moveDirection; // private variable to assign vector2 in OnMove method
    [SerializeField] private Rigidbody2D _rb; // private rigidbody which will be assigned in start
    [SerializeField] private Vector2 shootDirection; // private variable to assign shooting direction

    
    public GameObject bulletPrefab; // creating gameobject to store bulet prefab to spawn bullet from onshoot method
    public float bulletSpeed; //float to control speed of the bullet
    public bool playerShooting; //bool to tell whther the player is shooting or not. assigned when input of OnShoot method is not vector2(0, 0)

    public bool playerCanMove; // bool variable which will be used later on to control whether the player is allowed to move or not
    //could move this bool into its own script which keeps track of player variables needed to be altered by many different scripts

    // creating vector2 array to store values of 8 directions and Idle (not moving)
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
        moveDirection = QuantizeDirection(moveDirection); // Put movedirection through Quantize function to return best matched direction in direction array.
        //Debug.Log("moveDirection is" + moveDirection);
        // sending debug of movedirection x and y values

    }

    private void OnShoot(InputValue value)
    {
        shootDirection = value.Get<Vector2>().normalized; //assigning direction of input (controller right stick) to shootDirection Vector2
        shootDirection = QuantizeDirection(shootDirection); // Quantizing shoot direction to one of the 8 directions.

        if(shootDirection == Vector2.zero)
        {
            playerShooting = false;
            Debug.Log("player is not shooting");
        }

        Debug.Log("Shoot direction is " + shootDirection);


        //What to do if shoot direction is 0, 0 vector2?
    }

    // method to take input and find the closest direction
    private Vector2 QuantizeDirection(Vector2 input)
    {
        float maxDot = -Mathf.Infinity; // initialize at very small value. will e used to find highest dot product found
        Vector2 bestMatch = Vector2.zero; // initialize bestmatch to 0, 0. Idle.

        foreach (var direction in directions)
        {
            float dot = Vector2.Dot(input, direction); // takes input and measures it against each direction in directions.

            if (dot > maxDot) // If the current dot product is greater than maxDot, it means the current direction is closer to the input direction than any previously checked direction.
            {
                maxDot = dot;
                bestMatch = direction;
            }
        }

        return bestMatch;
    }
}

