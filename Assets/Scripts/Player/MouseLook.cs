using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float sensitivity = 200f;
    public Transform playerBody;
    public float playerRotation = 0f;

    public Canvas canvas;

    public Transform destination;
    Vector3 oldPosition;
    Quaternion oldRotation;

    public Transform dialogueTarget;
    public float movementTime = 1;
    public float rotationSpeed = 0.1f;


    public bool examiningObject = false;
    float pickupDistance = 3f;

    public GameObject selectedObject;
    public Transform selectedTransform;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private string pickupTag = "Interactable";

    public EnableDisableDOF enableDisableDOF;

    public enum State
    {
        Movement,
        Examining,
        Dialogue,
        Menu,
    }

    public State state;

    void Start()
    {
        state = State.Movement;

    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        switch (state)
        {
            case State.Movement:
                Cursor.lockState = CursorLockMode.Locked;

                playerRotation -= mouseY;
                playerRotation = Mathf.Clamp(playerRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(playerRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);

                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.forward, out hit, pickupDistance, layerMask))
                {
                    selectedTransform = hit.transform;
                    selectedObject = selectedTransform.gameObject;
                    if (selectedObject.GetComponent<Interactable>() != null)
                    {
                        UIScript.DisplayHoverText(selectedObject.GetComponent<Interactable>().OnHover());
                    }                  
                    //selectedObject.GetComponent<ItemDataSO>().itemName = "test";
                    if (Input.GetMouseButtonDown(0))
                    {
                        //RaycastHit hit;
                        if (selectedObject.CompareTag(pickupTag))
                        {
                            selectedObject.GetComponent<Interactable>().OnClick();
                        }
                    }
                }
                else
                {
                    UIScript.hideHoverText();
                }

               
                break;

            case State.Examining:
                Cursor.lockState = CursorLockMode.Locked;
                if (Input.GetMouseButtonDown(1))
                {
                    selectedObject.GetComponent<ItemScript>().OnDrop();

                }
                else if (Input.GetMouseButtonDown(0) && Inventory.Instance.hasBasket)
                {
                    selectedObject.GetComponent<ItemScript>().AddToInventory();


                }
                break;

            case State.Dialogue:
                Cursor.lockState = CursorLockMode.Confined;


                if (!dialogueTarget)
                    return;
                //Interpolate Rotation
                transform.localRotation = Quaternion.Slerp(transform.localRotation, dialogueTarget.rotation, rotationSpeed * Time.deltaTime);

                
                break;

            case State.Menu:
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }

        //if (examiningObject == false)
        //{

        //    //playerRotation -= mouseY;
        //    //playerRotation = Mathf.Clamp(playerRotation, -90f, 90f);

        //    //transform.localRotation = Quaternion.Euler(playerRotation, 0f, 0f);
        //    //playerBody.Rotate(Vector3.up * mouseX);

        //    //if (Input.GetMouseButtonDown(0)) {
        //    //    RaycastHit hit;

        //    //    if (Physics.Raycast(transform.position, transform.forward, out hit, pickupDistance))
        //    //    {
        //    //        selectedTransform = hit.transform;
        //    //        selectedObject = selectedTransform.gameObject;
        //    //        //selectedObject.GetComponent<ItemDataSO>().itemName = "test";
        //    //        if (selectedObject.CompareTag(pickupTag))
        //    //        {
        //    //            print(selectedObject.name);
        //    //            //examiningObject = true;
        //    //            //GameObject.Find("Player").GetComponent<PlayerMovement>().examining = true;
        //    //            //oldPosition = selectedObject.transform.position;
        //    //            //oldRotation = selectedObject.transform.rotation;
        //    //            //selectedObject.transform.position = destination.position;

        //    //            //selectedObject.transform.LookAt(Camera.main.transform.position,Vector3.up);
        //    //            //selectedObject.transform.Rotate(0f, 90f, 0f);

        //    //            //enableDisableDOF.EnableDisableDepthOfField(true);

        //    //            //GameObject.Find("UI Canvas").GetComponent<UIFadeScript>().UIFade(true);

        //    //            selectedObject.GetComponent<Interactable>().OnClick();

        //    //            //selectedObject.layer = 8;

        //    //            //UIScript.ChangeUI(UIEnum.INTERACTING);
        //    //        }

        //    //    }

        //    //}

        //}
        //else
        //{
        //    //selectedObject.transform.Rotate(Vector3.up, mouseX);
        //    //selectedObject.transform.Rotate(Vector3.right, mouseY, Space.World);

        //    if (Input.GetMouseButtonDown(1))
        //    {

        //        //examiningObject = false;
        //        //GameObject.Find("Player").GetComponent<PlayerMovement>().examining = false;
        //        //selectedObject.transform.position = oldPosition;
        //        //selectedObject.transform.rotation = oldRotation;
        //        //enableDisableDOF.EnableDisableDepthOfField(false);

        //        //GameObject.Find("UI Canvas").GetComponent<UIFadeScript>().UIFade(false);
        //        //UIScript.ChangeUI(UIEnum.CROSSHAIR);

        //        //selectedObject.layer = 7;

        //        selectedObject.GetComponent<ItemScript>().OnDrop();
        //    }
        //    else if (Input.GetMouseButtonDown(0) && Inventory.Instance.hasBasket)
        //    {
        //        //examiningObject = false;
        //        //GameObject.Find("Player").GetComponent<PlayerMovement>().examining = false;
        //        selectedObject.GetComponent<ItemScript>().AddToInventory();
        //        //enableDisableDOF.EnableDisableDepthOfField(false);

        //        //GameObject.Find("UI Canvas").GetComponent<UIFadeScript>().UIFade(false);
        //        //UIScript.ChangeUI(UIEnum.CROSSHAIR);

        //        //selectedObject.layer = 7;

        //    }
        //}
        

    }


}
