using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyCounter : MonoBehaviour {

    public int numberOfEnemies=0;
    public Text numEnemyText;
    GameObject player;

	// Use this for initialization
	void Start () {

        player = GameObject.Find("Main Character Doesn't Run(Clone)");

    }
	
	// Update is called once per frame
	void Update () {

        //gets the number of enemies and displays it
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
        
        //Get the current level and add one, if for some reason "level" doesn't exist start player at level 0
        int level;
        try {
            level = PlayerPrefs.GetInt("level");
        }
        catch {
            level = 0;
        }

        //If set level to the next level
        PlayerPrefs.SetInt("level", level+1);
    }
}
