using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlights : MonoBehaviour
{
    GameObject lastHighlightedObject;

    [SerializeField] Image cursorUI;
    [SerializeField] Sprite normalCursor;
    [SerializeField] Sprite interactCursor;
    [SerializeField] Image lightProgressImage;
    Camera mainCamera;
    [SerializeField] GameController gameController;
    string noKeyFoundString = "It's locked but I see a key hole...";

    void Start() {
        mainCamera = Camera.main;
    }
    void Update() {
        HighlightObjectInCenterOfCam();
    }

    void HighlightObject(GameObject gameObject, bool uiEnabled) {
        if (lastHighlightedObject != gameObject)
        {
            ClearHighlighted();

            lastHighlightedObject = gameObject;
            if(uiEnabled)
            {
                cursorUI.sprite = interactCursor;
            }
        }
    } 

    void ClearHighlighted()
    {
        if (lastHighlightedObject != null)
        {
            //lastHighlightedObject.GetComponent<MeshRenderer>().material = originalMat;
            
            lastHighlightedObject = null;
            cursorUI.sprite = normalCursor;
        }
    } 
    
    void HighlightObjectInCenterOfCam()
    {
        float rayDistance = 50f;
        // Ray from the center of the viewport.
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit rayHit;
        // Check if we hit something.
        if (Physics.Raycast(ray, out rayHit, rayDistance))
        {
            // Get the object that was hit.
            GameObject hitObj = rayHit.collider.gameObject;

            switch(hitObj.GetComponent<Collider>().gameObject.tag)
            {
                case "LockedDoor":
                    if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                    {
                        HighlightObject(hitObj, true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            hitObj.GetComponent<Collider>().gameObject.GetComponent<Interactable>().InteractWithAudioOnly();
                        }
                    }
                    else
                    {
                        ClearHighlighted();
                    }
                    break;
                case "OpenableDoor":
                    if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                    {
                        HighlightObject(hitObj, true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            var doorScript = hitObj.GetComponent<Collider>().gameObject.GetComponent<DoorController>();
                            bool doorClosed = doorScript.ReturnDoorStatus();
                            if(doorClosed)
                            {
                                doorScript.OpenDoor();
                            }
                            else
                            {
                                doorScript.CloseDoor();
                            }
                        }
                    }
                    else
                    {
                        ClearHighlighted();
                    }
                    break;
                case "KnockOnDoor":
                    if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                    {
                        HighlightObject(hitObj, true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            hitObj.GetComponent<Collider>().gameObject.GetComponent<Interactable>().InteractWithAudioOnly();
                        }
                    }
                    else
                    {
                        ClearHighlighted();
                    }
                    break;
                case "Escape":
                    if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 6f)
                    {
                        HighlightObject(hitObj, true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            hitObj.GetComponent<Collider>().gameObject.GetComponent<MainEvents>().EscapeAndWin();
                        }
                    }
                    else
                    {
                        ClearHighlighted();
                    }
                    break;
                case "ExpositionNote":
                    if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                    {
                        HighlightObject(hitObj, true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            //hitObj.GetComponent<Collider>().gameObject.GetComponent<Interactable>().EnableJournalNote();
                        }
                    }
                    else
                    {
                        ClearHighlighted();
                    }
                    break;
                case "HiddenKey":
                    if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                    {
                        HighlightObject(hitObj, true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            hitObj.GetComponent<Collider>().gameObject.GetComponent<MainEvents>().InteractWithHiddenKey();
                        }
                    }
                    else
                    {
                        ClearHighlighted();
                    }
                    break;
                case "LockedDrawer":
                    if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                    {
                        HighlightObject(hitObj, true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            if(!gameController.playerNeedsKey)
                            {
                                //hitObj.GetComponent<Collider>().gameObject.GetComponent<Interactable>().InteractWithDrawer();
                            }
                            else
                            {
                                gameController.ShowPopupMessage(noKeyFoundString, 2);
                            }
                        }
                    }
                    else
                    {
                        ClearHighlighted();
                    }
                    break;                 

                case "StepLadder":
                    if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                    {
                        if(gameController.currentCheckpoint == 4)
                        {
                            HighlightObject(hitObj, true);
                            if(Input.GetKey(KeyCode.E))
                            {
                                hitObj.GetComponent<Collider>().gameObject.GetComponent<MainEvents>().PickUpStepLadder();
                            }
                        }
                    }
                    break;

                default:
                    ClearHighlighted();
                    break;
            }
        }
    }
}
