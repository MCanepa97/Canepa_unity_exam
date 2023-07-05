using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAI : MonoBehaviour
{
    public Vector2 direction;
    private bool hasCheddar=false;
    public GameObject barrell;
    public GameObject hole;
    public float speed;
    private float pathToCheddar;
    public Animator animator;
    Rigidbody2D goblinBody;
    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        goblinBody= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasCheddar)
        {
        //gets the distance between barrell and goblin
        pathToCheddar = Vector2.Distance(transform.position, barrell.transform.position);
        direction = barrell.transform.position- transform.position;
        transform.position = Vector2.MoveTowards(this.transform.position, barrell.transform.position, speed*Time.deltaTime);
        }
        else
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
    }
    

    public void FoundCheddar(Collision2D other)
    {
        if (other.collider.tag=="Barrell")
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
}
