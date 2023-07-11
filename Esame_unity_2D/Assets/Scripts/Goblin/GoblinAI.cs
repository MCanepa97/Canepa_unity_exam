using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GoblinAI : MonoBehaviour
{
    public static int goblinSlayed = 0;
    float rotationDegree;
    public GameObject cameraScripts;
    public Vector2 direction;
    public bool hasCheddar=false;
    public GameObject barrell;
    public GameObject cheddar;
    public GameObject hole;
    public GameObject goblinGameObject;
    public float speed;
    public bool rotating = false;
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

    private void Update() 
    {
        if (HP<=0)
        {
            Destroy(goblinGameObject); 
        }
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
        if (rotating)
        {
            
            transform.Rotate(0,0,rotationDegree);
            
        }
        direction.Normalize();
        
        Debug.Log("si muove sulle x di="+direction.x);
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
            hasCheddar=true;
            animator.SetBool("HasCheddar",hasCheddar);
            
        }
        if (other.collider.tag=="Hole"&&hasCheddar)
        {
            cameraScripts.GetComponent<GoblinSpawner>().cheddarGotStolen=true;
            //cameraScripts.GetComponent<GoblinSpawner>().numEnemies--;
            Debug.Log("SCAPPA COL CHEDDAR!");
            goblinBody.gameObject.SetActive(false);
        }
    }
    public async void Hit()
    {
        HP--;
        isFlying=true;
        if(direction.x>0.6||direction.x<-0.6)
        {
            
            rotating=true;
            if (direction.x>0)
            {
                rotationDegree=14.4f;
            }
            else if(direction.x<0)
            {
                rotationDegree=-14.4f;
            }
        }
        animator.SetBool("IsFlying",true);
        if(hasCheddar)
        {
            Debug.Log("cheddar should Fall");
            hasCheddar=false;
            animator.SetBool("HasCheddar",false);
            cheddar.GetComponent<CheddarScript>().GoblinGotHit(); 
            
        }
        await Task.Delay(500);
        isFlying=false;
        rotating=false;
        animator.SetBool("IsFlying",false);         
    }

    private void OnDestroy() 
    {
        cameraScripts.GetComponent<GoblinSpawner>().goblinKilled++;
        goblinSlayed++;
    }
}