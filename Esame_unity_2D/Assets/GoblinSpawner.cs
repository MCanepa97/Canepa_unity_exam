using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.SceneManagement;

public class GoblinSpawner : MonoBehaviour
{
    public static int currentRound = 1;
    public int requiredGoblins = 1;
    public int goblinKilled = 0;
    public GameObject barrell;
    public GameObject cheddar;
    public int currentGoblinNum=0;
    public TMP_Text roundCounter;
    public TMP_Text gameOver;

    public bool cheddarGotStolen = false;
    public GameObject goblin;
    //public Vector2[] possibleSpawns = new Vector2[4];
    static Vector2 spawn1 = new Vector2(-3.6f,-3.93f);
    static Vector2 spawn2 = new Vector2(-3.6f,3.93f);
    static Vector2 spawn3 = new Vector2(-8.6f,0.16f);
    static Vector2 spawn4 = new Vector2(-6f,2.5f);
    public Vector2[] possibleSpawns = new Vector2[4]{spawn1,spawn2,spawn3,spawn4};
    
    public 
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnGoblin",0,1);
        
        InvokeRepeating("KeepTrackOfGoblinSpawnAndRound",0,1);
    }

    private async void Update() 
    {
        roundCounter.text="ROUND "+currentRound;
        if (cheddarGotStolen)
        {
            
            gameOver.gameObject.SetActive(true);
            cheddarGotStolen = false;
            await Task.Delay(1000);
            SceneManager.LoadScene(2);
        } 
        
        
    }
    
    private void spawnGoblin()
    {
        
        if (currentGoblinNum<requiredGoblins)
        {
            GameObject spawnedGoblin = Instantiate(goblin) as GameObject;
            spawnedGoblin.transform.position = possibleSpawns[Random.Range(0,3)];
            spawnedGoblin.SetActive(true);   
            currentGoblinNum++;  
        } 
               
    }
    void KeepTrackOfGoblinSpawnAndRound()
    {
        if(goblinKilled==currentGoblinNum&&goblinKilled!=0)
        {
            Debug.Log("aumento round");
            currentRound++;
            requiredGoblins++;
            currentGoblinNum=0;
            goblinKilled=0;
            barrell.GetComponent<Animator>().SetBool("CheddarStolen",false);
            cheddar.GetComponent<CheddarScript>().newRoundStarted=true;
        }          
    }

    

    // Update is called once per frame

}
