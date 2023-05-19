using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Text popupText;
    [SerializeField] Text objectiveText;    
    [SerializeField] Text endingUIText;
    [SerializeField] Text taskListText;

    AudioSource audioSource;
    public int currentCheckpoint;
    public bool canLeaveByCar = false;
    public bool playerNeedsKey = true;
    public bool playerHasFuse = false;
    
    public bool fuseLightsOff = false;

    public bool fuseBoxFixed = false;

    public bool engagedInTask = false;

    public bool holdingTrash = false;
    public bool sinkCompleted = false;
    public bool lightCompleted = false;
    public bool trashCompleted = false;

    [SerializeField] private string[] gameObjectives;
    void Start() {
        currentCheckpoint = 0;
    }

    public void ShowPopupMessage(string message, float delay) {
        StartCoroutine(DisplayMessage(message, delay));
    }
    IEnumerator DisplayMessage(string message, float delay) {
        popupText.text = message;
        popupText.enabled = true;
        yield return new WaitForSeconds(delay);
        popupText.enabled = false;
    }

    public void LoadLevel(int levelNumber) {
        SceneManager.LoadScene(levelNumber);
    }
}
