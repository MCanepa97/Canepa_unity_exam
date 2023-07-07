using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GoblinAI : MonoBehaviour
{
    public Vector2 direction;
    public bool hasCheddar=false;
    public GameObject barrell;
    public GameObject cheddar;
    public GameObject hole;
    public GameObject goblinGameObject;
    public float speed;
    bool rotating;
    private float pathToCheddar;
    public Animator animator;
    Rigidbody2D goblinBody;
    Vector2 movement;
    public int HP = 2;
    public bool isFlying = false;
    // Start is called before the first frame update
    void Start()
    {
        goblinBody= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasCheddar&&!isFlying)
        {
        //gets the distance between barrell and goblin
        pathToCheddar = Vector2.Distance(transform.position, cheddar.transform.position);
        direction = cheddar.transform.position- transform.position;
        transform.position = Vector2.MoveTowards(this.transform.position, cheddar.transform.position, speed*Time.deltaTime);
        }
        else if(hasCheddar)
        {
        float pathToHole = Vector2.Distance(transform.position, hole.transform.position);
        direction = hole.transform.position- transform.position;
        transform.position = Vector2.MoveTowards(this.transform.position, hole.transform.position, speed*Time.deltaTime);  
        }      

        direction.Normalize();
        
        Debug.Log(goblinBody.velocity.magnitude.ToString());
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        /*animator.SetFloat("Horizontal", direction.x);    
        animator.SetFloat("Vertical", direction.y);*/
    }  

    private void OnCollisionEnter2D(Collision2D other) 
    {
        FoundCheddar(other);
        if (other.collider.tag=="Wall"&&isFlying)
        {
            Destroy(goblinGameObject);
        }
    }
    

    public void FoundCheddar(Collision2D other)
    {
        if (other.collider.tag=="Cheddar")
        {
        
        animator.SetBool("HasCheddar",true);
        hasCheddar=true;
        }
        if (other.collider.tag=="Hole")
        {
            Debug.Log("SCAPPA COL CHEDDAR!");
            goblinBody.gameObject.SetActive(false);
        }
    }
    public async void Hit()
    {
        HP--;
        isFlying=true;
        animator.SetBool("IsFlying",true);
        
        if(hasCheddar)
        {
            hasCheddar=false;
            animator.SetBool("HasCheddar",false);
            cheddar.GetComponent<CheddarScript>().GoblinGotHit(); 
            if (HP<=0)
            {
            Destroy(goblinGameObject); 
            }
            await Task.Delay(400);
            isFlying=false;
            rotating=false;
            animator.SetBool("IsFlying",false);
        }
        
       
        
        
        
    }
}
