using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    private Rigidbody2D _rigidBody; // creates a variable that will store a rigidBody
    private Vector2 _movementInput; // creates a variable to store the player input, to be used in all methods
    private Vector2 _smoothedMovementInput; // a secondary movement vector to smooth out movement when input ends
    private Vector2 _velocityTracker; //a vector to keep track of the velocity of the change
    public Animator animator; //creazione di un animator
    private void Awake() //calling this method when the scene is 1st initialized
    {
        _rigidBody= GetComponent<Rigidbody2D>(); // getting the rigid body of the player and will store it in the variable at line 7
    }
    private void FixedUpdate() // put any change to the rigidBody here!!
    {
        // setting the velocity (speEeEeEed) of the rigidBody, using 
        //a vector storing a parameter for X axis movement and one for Y axis movement
        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _movementInput,
            ref _velocityTracker,
            0.1f
        ); //transition speed
         animator.SetFloat("horizontal",_movementInput.x);
         animator.SetFloat("vertical",_movementInput.y);
         animator.SetFloat("speed",_movementInput.magnitude);

        _rigidBody.velocity = _smoothedMovementInput*speed;  // this will take the player input and use it as velocity of the rigidbody
        
        
    }

    // creation of the method onMove(it will recognize user input)
    private void OnMove(InputValue inputValue) // inputValue = the actual imputs given by the user
    {
        Debug.Log(_movementInput.x);
        _movementInput = inputValue.Get<Vector2>(); //represents the direction and magnitude (force) of the input
        
       
        if (Input.GetKeyUp("a")) 
        {
            animator.SetBool("facingLeft",true);
        }
        if (Input.GetKeyUp("d"))
        {
            animator.SetBool("facingRight",true);
        }
        if (Input.GetKeyUp("w"))
        {
            animator.SetBool("facingUp",true);
        }
        if (Input.anyKey)
        {
            animator.SetBool("facingUp",false);
            animator.SetBool("facingRight",false);
            animator.SetBool("facingLeft",false);
        } 
    }
    
    private async void OnPunch(InputValue inputValue)
    {
        

        if (inputValue.isPressed&&Input.GetKey("w")) 
        {
            animator.SetBool("punchUp", true);
            speed=0;
            await Task.WhenAll (punchUp());
            
        }
        if (inputValue.isPressed&&Input.GetKey("s"))
        {
            animator.SetBool("punchDown", true);
            speed=0;
            await Task.WhenAll (punchDown());
            
        }
        if (inputValue.isPressed&&Input.GetKey("d"))
        {
            animator.SetBool("punchRight", true);
            speed=0;
            await Task.WhenAll (punchRight());
            
        }
        if (inputValue.isPressed&&Input.GetKey("a"))
        {
            animator.SetBool("punchLeft", true);
            speed=0;
            await Task.WhenAll (punchLeft());
            
        }
        if (inputValue.isPressed)
        {
            animator.SetBool("punch",true);
            animator.SetBool("punchUp",false);
            animator.SetBool("punchDown",false);
            animator.SetBool("punchLeft",false);
            animator.SetBool("punchRight",false);
            speed=0;
            await Task.WhenAll (punch());
        }
    }
    public async Task punch()
    {
        await Task.Delay(500);
        speed=4;
        animator.SetBool("punch", false);   
    }

    public async Task punchUp()
    {
        await Task.Delay(500);
        speed=4;
        animator.SetBool("punchUp", false);   
    }
    public async Task punchDown()
    {
        await Task.Delay(500);
        speed=4;
        animator.SetBool("punchDown", false);   
    }
    public async Task punchRight()
    {
        await Task.Delay(500);
        speed=4;
        animator.SetBool("punchRight", false);   
    }
    public async Task punchLeft()
    {
        await Task.Delay(500);
        speed=4;
        animator.SetBool("punchLeft", false);   
    }
}
