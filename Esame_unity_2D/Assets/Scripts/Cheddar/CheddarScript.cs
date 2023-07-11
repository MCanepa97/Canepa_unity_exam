using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class CheddarScript : MonoBehaviour
{
    Rigidbody2D cheddarBody;
    Rigidbody2D goblinBody;
    public GameObject barrell;
    public Vector2 spawnPoint= new Vector3(0.56f, 1.31f,0);
    public bool newRoundStarted=false;
    public GameObject goblin;
    // Start is called before the first frame update
    void Start(){
        cheddarBody = GetComponent<Rigidbody2D>();
    }

    private async void Update(){
        if(goblinBody!=null)
            transform.position = Vector2.MoveTowards(this.transform.position, goblinBody.transform.position, 4*Time.deltaTime);
        else if (goblinBody==null)
            transform.position = Vector2.MoveTowards(this.transform.position, this.transform.position, 4*Time.deltaTime);
        
        if(newRoundStarted){
            transform.position= spawnPoint; 
            await Task.Delay(100);
            newRoundStarted=false;
            cheddarBody.GetComponent<SpriteRenderer>().enabled = false;
            //cheddarBody.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezeRotation;
        }
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other){
        if(other.collider.tag=="Goblin"){
                Debug.Log("abbiamo trovato il cheddar"+ other.transform.position);
                cheddarBody.GetComponent<Collider2D>().enabled = false;
                cheddarBody.GetComponent<SpriteRenderer>().enabled = false;
                goblinBody= other.rigidbody;   
                barrell.GetComponent<Animator>().SetBool("CheddarStolen",true);
            }
    }

    public async void GoblinGotHit(){ 
        goblinBody=null;
        cheddarBody.GetComponent<SpriteRenderer>().enabled = true;
        cheddarBody.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezeAll;
        await Task.Delay(200);
        cheddarBody.GetComponent<Collider2D>().enabled = true;
        Debug.Log("goblin Got Hit");

    }
}
