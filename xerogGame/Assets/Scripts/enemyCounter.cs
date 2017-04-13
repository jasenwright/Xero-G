using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyCounter : MonoBehaviour {

    public int numberOfEnemies=0;
    public Text numEnemyText;
    public Text levelText;
    public Image numEnemyBackgroundImage;
    GameObject player;
    public bool nextLevelLoaded = false;

	// Use this for initialization
	void Start () {

        nextLevelLoaded = false;
        player = GameObject.Find("Main Character Doesn't Run(Clone)");

        Transform canvas = player.transform.Find("Canvas");

        //gets the number of enemies and displays it
        Transform nOET = canvas.transform.Find("numberOfEnemiesText");
        numEnemyText = nOET.GetComponent<Text>();

        //Display the level
        Transform level = canvas.transform.Find("Level");
        levelText = level.GetComponent<Text>();

        //Needed so can get rid of background image
        Transform numEnemyBackground = canvas.transform.Find("numEnemiesTextBackground");
        numEnemyBackgroundImage = numEnemyBackground.GetComponent<Image>();

        levelText.text = "LEVEL: " + PlayerPrefs.GetInt("level");


    }

	void Update () {

        numEnemyText.text = "NUMBER OF ENEMIES: " + numberOfEnemies;
        

        if (numberOfEnemies == 0) {
            //Instructs the player to go to the door and removes text background image
            numEnemyBackgroundImage.enabled = false;
            numEnemyText.text = "GO BACK TO THE DOOR TO PROCEEED TO NEXT LEVEL";

        }
	}

    public void decreaseEnemies() {
        numberOfEnemies = numberOfEnemies - 1;
    }

}
