using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GoblinSpawner : MonoBehaviour
{

    public GameObject goblin;
    //public Vector2[] possibleSpawns = new Vector2[4];
    static Vector2 spawn1 = new Vector2(-3.6f,-3.93f);
    static Vector2 spawn2 = new Vector2(-3.6f,3.93f);
    static Vector2 spawn3 = new Vector2(-8.5f,0.16f);
    static Vector2 spawn4 = new Vector2(-6f,2.5f);
    public Vector2[] possibleSpawns = new Vector2[4]{spawn1,spawn2,spawn3,spawn4};
    
    public 
    // Start is called before the first frame update
    async void Start()
    {
        await Task.Delay(2000);
        spawnGoblin();
    }

    private void spawnGoblin()
    {
        GameObject spawnedGoblin = Instantiate(goblin) as GameObject;
        spawnedGoblin.transform.position= possibleSpawns[Random.Range(0,3)];
        spawnedGoblin.SetActive(true);
    }

    // Update is called once per frame

}
