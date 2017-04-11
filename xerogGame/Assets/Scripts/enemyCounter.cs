using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyCounter : MonoBehaviour {

    public int numberOfEnemies=0;
    public Text numEnemyText;

	// Use this for initialization
	void Start () {

        
        

        
    }
	
	// Update is called once per frame
	void Update () {
        
        //gets the number of enemies and displays it
        GameObject player = GameObject.Find("Main Character Doesn't Run(Clone)");
        Transform canvas = player.transform.Find("Canvas");
        Transform nOET = canvas.transform.Find("numberOfEnemiesText");
        Text numEnemyText = nOET.GetComponent<Text>();
        numEnemyText.text = "Number of Enemies: " + numberOfEnemies;

        if (numberOfEnemies == 0) {
            loadNextLevel();
        }
	}

    public void decreaseEnemies() {
        numberOfEnemies = numberOfEnemies - 1;
        numEnemyText.text = "Number of Enemies: " + numberOfEnemies;
    }

    void loadNextLevel() {

    }
}
