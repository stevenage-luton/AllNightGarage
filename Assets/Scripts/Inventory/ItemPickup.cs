using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemDataSO m_currentItem;
    [SerializeField] private GameObject m_heldItem;
    public TMP_Text itemText;
    public TMP_Text itemPrice;
    public TMP_Text basketTotal;

    public Transform destination;

    private Vector3 m_oldPosition; 
    private Quaternion m_oldRotation;

    public float sensitivity = 400f;

    public PlayerMovement playerMovement;
    public EnableDisableDOF enableDisableDOF;
    public UIFadeScript UIFadeScript;

    private Camera m_cam;

    private void Start()
    {
        m_cam = Camera.main;
    }

    public void PickupItem(ItemDataSO itemData)
    {
        m_currentItem = itemData;

        itemText.text = m_currentItem.itemName;

    }

    public void DropCurrentItem()
    {
        m_currentItem = null;
        m_heldItem.transform.position = m_oldPosition;
        m_heldItem.transform.rotation = m_oldRotation;

        m_heldItem.GetComponent<Collider>().enabled = true;

        m_heldItem.layer = 7;

        playerMovement.examining = false;
        enableDisableDOF.EnableDisableDepthOfField(false);
        UIFadeScript.UIFade(false);
        UIScript.ChangeUI(UIEnum.CROSSHAIR);

        m_heldItem = null;

        gameObject.GetComponent<MouseLook>().examiningObject = false;
    }

    public void AddItemToInventory(ItemDataSO itemData)
    {

        Inventory.Instance.AddItem(itemData);

        m_heldItem.layer = 7;

        playerMovement.examining = false;
        enableDisableDOF.EnableDisableDepthOfField(false);
        UIFadeScript.UIFade(false);
        UIScript.ChangeUI(UIEnum.CROSSHAIR);

        m_currentItem = null;
        m_heldItem = null;

        gameObject.GetComponent<MouseLook>().examiningObject = false;
    }

    public void ExamineHeldItem(GameObject itemToHold)
    {
        gameObject.GetComponent<MouseLook>().examiningObject = true;

        m_heldItem = itemToHold;
        m_oldPosition = m_heldItem.transform.position;
        m_oldRotation = m_heldItem.transform.rotation;

        itemPrice.text = m_heldItem.GetComponent<ItemScript>().PriceString();
        basketTotal.text = Inventory.Instance.PriceTotal();

        m_heldItem.GetComponent<Collider>().enabled = false;

        m_heldItem.transform.position = destination.position;
        m_heldItem.transform.LookAt(Camera.main.transform.position, Vector3.up);
        m_heldItem.transform.position += m_heldItem.transform.forward * m_currentItem.zoomDistance;
        m_heldItem.transform.Rotate(0f, m_currentItem.rotateAmount, 0f);

        m_heldItem.layer = 8;

        playerMovement.examining = true;
        enableDisableDOF.EnableDisableDepthOfField(true);
        UIFadeScript.UIFade(true);
        UIScript.ChangeUI(UIEnum.INTERACTING);
    }


    void Update()
    {

        if (m_heldItem != null)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            Vector3 right = Vector3.Cross(m_cam.transform.up, m_heldItem.transform.position - m_cam.transform.position);
            Vector3 up = Vector3.Cross(m_heldItem.transform.position - m_cam.transform.position, right);

            m_heldItem.transform.rotation = Quaternion.AngleAxis(-mouseX, up) * m_heldItem.transform.rotation;
            m_heldItem.transform.rotation = Quaternion.AngleAxis(mouseY, right) * m_heldItem.transform.rotation;
            //m_heldItem.transform.Rotate(Vector3.up, mouseX);
            //m_heldItem.transform.Rotate(Vector3.right, mouseY, Space.World);
        }
    }
}
