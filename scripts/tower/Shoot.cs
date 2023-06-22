using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

	public AudioClip shootSounds;
	public AudioSource soundSource;

	public Transform spawnPoint;
	public GameObject projectile;
	public float projectileVelocity;

	void Awake() {

		soundSource = (GameObject.Find("AudioSourceSFX")).GetComponent<AudioSource>();

	}

	public void shoot() {

		// Play a shooting sound and initiate a prefab which spawns at a predefined point on the tower
		// prefab (somewhere at the tip of the gun) and send it forward
		soundSource.PlayOneShot(shootSounds, 1.0f);
		var projectilePrefab = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
		projectilePrefab.GetComponent<Rigidbody2D>().velocity = spawnPoint.up * projectileVelocity;
		
	}

}
