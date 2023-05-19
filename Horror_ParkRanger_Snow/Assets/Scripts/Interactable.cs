using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameController gameController;

    [SerializeField] enum InteractType {Journal, B}
    [SerializeField] InteractType interactType;

    void Start() {
        if (gameController == null) {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
        if(audioSource == null) {
            audioSource = gameObject.GetComponent<AudioSource>();
        }
    }

    public void InteractWithAudioOnly() {
        if(!audioSource.isPlaying) {
            audioSource.Play();
        }
    }
    /*
    public void InteractWithDrawer() {
        lockedDrawerAnim.Play("DrawerOpen", 0, 0.0f);
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        underDrawerCollider.SetActive(true);
        drawerBoxCollider.enabled = false;
    }

    public void EnableJournalNote() {
        if(!audioSource.isPlaying) {
            audioSource.Play();
        }
        journalUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void DisableJournalNote() {
        journalUI.SetActive(false);
    }
    */

}

