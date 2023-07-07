using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class CheddarScript : MonoBehaviour
{
    Rigidbody2D cheddarBody;
    Rigidbody2D goblinBody;
    
    public GameObject goblin;
    // Start is called before the first frame update
    void Start()
    {
        cheddarBody = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        if(goblinBody!=null)
        transform.position = Vector2.MoveTowards(this.transform.position, goblinBody.transform.position, 4*Time.deltaTime);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag=="Goblin")
            {
                Debug.Log("abbiamo trovato il cheddar"+ other.transform.position);
                cheddarBody.GetComponent<Collider2D>().enabled = false;
                cheddarBody.GetComponent<SpriteRenderer>().enabled = false;
                goblinBody= other.rigidbody;       
            }
    }

    public async void GoblinGotHit()
    {
        goblinBody=null;
        cheddarBody.GetComponent<SpriteRenderer>().enabled = true;
        cheddarBody.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezeAll;
        await Task.Delay(200);
        cheddarBody.GetComponent<Collider2D>().enabled = true;
        Debug.Log("goblin Got Hit");

    }
}
