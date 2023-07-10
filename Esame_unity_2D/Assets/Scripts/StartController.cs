using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartController : MonoBehaviour
{
    // Start is called before the first frame update
     public void Open() {
        
        Debug.Log("toccato");
        SceneManager.LoadScene(1);
        

    }

    
}
