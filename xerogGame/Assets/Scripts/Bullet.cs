using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public CharacterControllerScripts character;

    public GameObject bulletPrefab;

    public float bulletSpeed = 10;

    // Update is called once per frame
    void Update () {
		this.transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed);
	}
}
