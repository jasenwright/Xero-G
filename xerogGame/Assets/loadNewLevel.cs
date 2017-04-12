using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadNewLevel : MonoBehaviour {

    public GameObject eCounter;
    public enemyCounter numberOfEnemies;
    public GameObject mController;
    public MenuControl menuController;

    // Use this for initialization
    void Start() {
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
            if (col.tag == "Player" && numberOfEnemies.numberOfEnemies == 0) {
                menuController.SceneChanger("Level1");
            }
        }
        catch { }

    }
}
