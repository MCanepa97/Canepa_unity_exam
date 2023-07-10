using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrellScript : MonoBehaviour
{
    private Rigidbody2D _barrellBody;
    public Animator animator;
    private void Awake() {
      _barrellBody= GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        WallSplat(other);            
    }
    /*private void OnCollisionStay2D(Collision2D other) 
    {
        WallSplat(other);            
    }*/
    
    
    void Update()
    {
       
    }
  void WallSplat(Collision2D other)
  {
    if(other.collider.tag=="Wall")
      _barrellBody.constraints= RigidbodyConstraints2D.FreezeAll;
      
    
  }
  
}
