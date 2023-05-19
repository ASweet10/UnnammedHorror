using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuffleSound : MonoBehaviour
{
    [SerializeField] AudioClip ambience;
    //[SerializeField] AudioClip muffledAmbience;
    AudioSource audioSource;

    [SerializeField] Transform player;
    [SerializeField] float rayDistance = 200f;
    [SerializeField] LayerMask obstacleLayer;

    [SerializeField] float muffledVolume = 0.2f;
    [SerializeField] float originalVolume = 0.4f;

    private void Awake() {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    void Update() {
        CheckPlayerLOSAndDistance();
    }

    void CheckPlayerLOSAndDistance(){
        Vector3 direction = (player.position - transform.position).normalized;

        //If raycasting & hitting an object
        if(Physics.Raycast(transform.position, player.position, rayDistance, obstacleLayer)){
            //Player inside
            Debug.Log("player inside");
            audioSource.volume = muffledVolume;
        }
        else {
            Debug.Log("player outside");
            audioSource.volume = originalVolume;
        }
    }
}
