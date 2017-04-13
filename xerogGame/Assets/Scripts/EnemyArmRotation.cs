using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmRotation : MonoBehaviour {

    public GameObject character;

    public int rotationOffset = 0;

    void Start()
    {
        character = GameObject.Find("Main Character Doesn't Run(Clone)");
    }

    public void rotateArm()
    {
        Vector3 difference = character.transform.position - transform.position;
        difference.Normalize();
        float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotz + rotationOffset);
    }
}
