using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;


public class PlayerMovement : MonoBehaviour
{
    enum Facing
    {
        Up,
        Down,
        Left,
        Right
    }
    public GoblinAI goblinAI;
    private Facing isFacing= Facing.Down;
    private bool punching=false;
    [SerializeField] private float speed = 4f;
    private Rigidbody2D _rigidBody; // creates a variable that will store a rigidBody
    private Vector2 _movementInput; // creates a variable to store the player input, to be used in all methods
    private Vector2 _smoothedMovementInput; // a secondary movement vector to smooth out movement when input ends
    private Vector2 _velocityTracker; //a vector to keep track of the velocity of the change
    public Animator animator; //creazione di un animator
    
    private void OnCollisionEnter2D(Collision2D other) 
    {
        punchingTheBaddies(other);
        
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        punchingTheBaddies(other);  
    }
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
        
        _movementInput = inputValue.Get<Vector2>(); //represents the direction and magnitude (force) of the input
        
       
        if (Input.GetKeyUp("a")||Input.GetKey("a")&&Input.GetKeyUp("space")) 
        {
            isFacing=Facing.Left;
            
            animator.SetBool("facingLeft",true);
        }
        if (Input.GetKeyUp("d")||Input.GetKey("d")&&Input.GetKeyUp("space"))
        {
            isFacing=Facing.Right;
            animator.SetBool("facingRight",true);
        }
        if (Input.GetKeyUp("w")||Input.GetKey("w")&&Input.GetKeyUp("space"))
        {
            isFacing=Facing.Up;
            animator.SetBool("facingUp",true);
        }
        if (Input.GetKeyUp("s")||Input.GetKey("s")&&Input.GetKeyUp("space"))
        {
             isFacing=Facing.Down;
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
            isFacing=Facing.Up;
       
            punching=true;
            animator.SetBool("punchUp", true);
            speed=0;
            await Task.WhenAll (punchUp());
            
        }
        if (inputValue.isPressed&&Input.GetKey("s"))
        {
            isFacing=Facing.Down;
            
            punching=true;
            animator.SetBool("punchDown", true);
            speed=0;
            await Task.WhenAll (punchDown());
            
        }
        if (inputValue.isPressed&&Input.GetKey("d"))
        {
            isFacing=Facing.Right;
            
            punching=true;
            
            animator.SetBool("punchRight", true);
            speed=0;
            await Task.WhenAll (punchRight());
            
        }
        if (inputValue.isPressed&&Input.GetKey("a"))
        {
            isFacing=Facing.Left;
            punching=true;
           
            animator.SetBool("punchLeft", true);
            speed=0;
            await Task.WhenAll (punchLeft());
            
        }
        if (inputValue.isPressed)
        {
            punching=true;
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
        punching = false;
    }

    public async Task punchUp()
    {
        
        await Task.Delay(500);
        speed=4;
        animator.SetBool("punchUp", false);   
        punching = false;
    }
    public async Task punchDown()
    {
        
        await Task.Delay(500);
        speed=4;
        animator.SetBool("punchDown", false);   
        punching = false;
    }
    public async Task punchRight()
    {
        
        await Task.Delay(500);
        speed=4;
        animator.SetBool("punchRight", false);   
        punching = false;
    }
    public async Task punchLeft()
    {
        
        await Task.Delay(500);
        speed=4;
        animator.SetBool("punchLeft", false);   
        punching = false;
    }

public async void punchingTheBaddies(Collision2D other)
    {
        int force = 5;
        int slowingForce = 1;
        Vector2 punchingForce= new Vector2(0,10);
        Vector2 slowingDown = new Vector2(0,0);
        Rigidbody2D goblinBody = other.collider.GetComponent<Rigidbody2D>();
        GameObject theGoblin = other.gameObject;
        switch (isFacing){
        case Facing.Down:
        punchingForce= new Vector2(0,-force);
        slowingDown= new Vector2(0,slowingForce);
        break;
        case Facing.Up:
        punchingForce= new Vector2(0,force);
        slowingDown= new Vector2(0,-slowingForce);
        break;
        case Facing.Left:
        punchingForce=new Vector2(-force,0);
        slowingDown= new Vector2(slowingForce,0);
        break;
        case Facing.Right:
        punchingForce = new Vector2(force,0);
        slowingDown = new Vector2(-slowingForce,0);
        break;
        }      
            
        if(other.collider.tag=="Goblin"&&punching)
        {
            
            goblinBody.constraints= RigidbodyConstraints2D.FreezeRotation;
            goblinBody.AddForce(punchingForce, ForceMode2D.Impulse);
            punching = false;
            int counter=0;
            goblinAI.Hit();
            while (counter<5)
            {
                await Task.Delay(350);
                goblinBody.AddForce(slowingDown, ForceMode2D.Impulse);
                counter++;
                
            }
            
            
        }
    }
}