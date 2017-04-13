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

        //gets the number of enemies and displays it
        Transform canvas = player.transform.Find("Canvas");

        Transform nOET = canvas.transform.Find("numberOfEnemiesText");
        numEnemyText = nOET.GetComponent<Text>();

        Transform level = canvas.transform.Find("Level");
        levelText = level.GetComponent<Text>();

        Transform numEnemyBackground = canvas.transform.Find("numEnemiesTextBackground");
        numEnemyBackgroundImage = numEnemyBackground.GetComponent<Image>(); 
        

    }
	
	// Update is called once per frame
	void Update () {

        numEnemyText.text = "NUMBER OF ENEMIES: " + numberOfEnemies;
        levelText.text = "LEVEL: " + PlayerPrefs.GetInt("level");

        if (numberOfEnemies == 0) {
            //Instructs the player to go to the door and removes text background image
            numEnemyBackgroundImage.enabled = false;
            numEnemyText.text = "GO BACK TO THE DOOR TO PROCEEED TO NEXT LEVEL";
            //loadNextLevel();
        }
	}

    public void decreaseEnemies() {
        numberOfEnemies = numberOfEnemies - 1;
        //numEnemyText.text = "NUMBER OF ENEMIES: " + numberOfEnemies;
    }

    void loadNextLevel() {

        if (nextLevelLoaded == false) {
            //Get the current level and add one, if for some reason "level" doesn't exist start player at level 0
            int level;
            try {
                level = PlayerPrefs.GetInt("level");
            }
            catch {
                level = 0;
            }

            //If set level to the next level
            PlayerPrefs.SetInt("level", level + 1);
            nextLevelLoaded = true;
        }

    }
}
