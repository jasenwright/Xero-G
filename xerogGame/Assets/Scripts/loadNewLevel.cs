using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadNewLevel : MonoBehaviour {

    public GameObject eCounter;
    public enemyCounter numberOfEnemies;
    public GameObject mController;
    public MenuControl menuController;
    public bool haveEntered;

    // Use this for initialization
    void Start() {
        bool haveEntered = false;
        //Get the menu controller and it's script
        mController = GameObject.Find("MenuControlObject");
        menuController = mController.GetComponent<MenuControl>();

        //Get the enemyCounter and it's script
        eCounter = GameObject.Find("enemyCounter");
        numberOfEnemies = eCounter.GetComponent<enemyCounter>();
    }

    void OnTriggerEnter2D(Collider2D col) {

        //Try needed as .numberOfEnemies might have been instantiated yet
        try {
            if (col.tag == "Player" && numberOfEnemies.numberOfEnemies == 0 && haveEntered == false) {
                int level;
                try {
                    level = PlayerPrefs.GetInt("level");
                }
                catch {
                    level = 0;
                }

                //If set level to the next level
                PlayerPrefs.SetInt("level", level + 1);
                haveEntered = true;
                menuController.SceneChanger("Level1");

            }
        }
        catch { }

    }
}
