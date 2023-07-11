using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextManager : MonoBehaviour
{
    public TMP_Text score;

    public int scoreValue;
    public TMP_Text round;
    public int roundValue;
    // Start is called before the first frame update
    void Start()
    {
        scoreValue= GoblinAI.goblinSlayed;
        roundValue= GoblinSpawner.currentRound;
        score.text = "Goblin menati: "+scoreValue;
        round.text = "Round superati: "+roundValue;
    }

    private void Update() {
        scoreValue= GoblinAI.goblinSlayed;

        score.text = "Goblin menati: "+scoreValue;
    }

    
    
}
