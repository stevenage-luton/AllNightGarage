using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemDataSO _currentItem;
    [SerializeField] private GameObject _heldItem;
    [SerializeField] private AudioSource _source;

    public TMP_Text itemText;
    public TMP_Text itemPrice;
    public TMP_Text basketTotal;
    public Transform destination;

    private Vector3 _oldPosition;
    private Quaternion _oldRotation;

    public float sensitivity = 400f;

    public PlayerMovement playerMovement;
    public EnableDisableDOF enableDisableDOF;
    public UIFadeScript UIFadeScript;

    private Camera _cam;

    Collider[] _helditemColliders;

    private void Start()
    {
        _cam = Camera.main;
    }

    public void PickupItem(ItemDataSO itemData)
    {
        _currentItem = itemData;

        itemText.text = _currentItem.itemName;

        _source.PlayOneShot(_currentItem.pickupSound);
    }

    public void DropCurrentItem()
    {
        _source.PlayOneShot(_currentItem.dropSound);

        _currentItem = null;
        _heldItem.transform.position = _oldPosition;
        _heldItem.transform.rotation = _oldRotation;

        foreach (Collider collider in _helditemColliders)
        {
            collider.enabled = true;
        }

        _heldItem.layer = 7;

        playerMovement.remainStationary = false;
        enableDisableDOF.EnableDisableDepthOfField(false);
        UIFadeScript.UIFade(false);
        UIScript.ChangeUI(UIEnum.CROSSHAIR);

        _heldItem = null;

        gameObject.GetComponent<MouseLook>().state = MouseLook.State.Movement;
    }

    public void AddItemToInventory(ItemDataSO itemData)
    {

        Inventory.Instance.AddItem(itemData);

        _heldItem.layer = 7;

        playerMovement.remainStationary = false;
        enableDisableDOF.EnableDisableDepthOfField(false);
        UIFadeScript.UIFade(false);
        UIScript.ChangeUI(UIEnum.CROSSHAIR);

        _currentItem = null;
        _heldItem = null;

        gameObject.GetComponent<MouseLook>().state = MouseLook.State.Movement;
    }

    public void ExamineHeldItem(GameObject itemToHold)
    {
        gameObject.GetComponent<MouseLook>().state = MouseLook.State.Examining;

        _heldItem = itemToHold;
        _oldPosition = _heldItem.transform.position;
        _oldRotation = _heldItem.transform.rotation;

        itemPrice.text = _heldItem.GetComponent<ItemScript>().PriceString();
        basketTotal.text = Inventory.Instance.PriceTotal();

        _helditemColliders = _heldItem.transform.GetComponentsInChildren<Collider>();

        foreach (Collider collider in _helditemColliders)
        {
            collider.enabled = false;
        }

        //_heldItem.GetComponent<Collider>().enabled = false;

        _heldItem.transform.position = destination.position;
        _heldItem.transform.LookAt(Camera.main.transform.position, Vector3.up);
        _heldItem.transform.position += _heldItem.transform.forward * _currentItem.zoomDistance;
        _heldItem.transform.Rotate(0f, _currentItem.rotateAmount, 0f);

        _heldItem.layer = 8;

        playerMovement.remainStationary = true;
        enableDisableDOF.EnableDisableDepthOfField(true);
        UIFadeScript.UIFade(true);
        UIScript.ChangeUI(UIEnum.INTERACTING);
    }


    void Update()
    {

        if (_heldItem != null)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            Vector3 right = Vector3.Cross(_cam.transform.up, _heldItem.transform.position - _cam.transform.position);
            Vector3 up = Vector3.Cross(_heldItem.transform.position - _cam.transform.position, right);

            _heldItem.transform.rotation = Quaternion.AngleAxis(-mouseX, up) * _heldItem.transform.rotation;
            _heldItem.transform.rotation = Quaternion.AngleAxis(mouseY, right) * _heldItem.transform.rotation;
            //_heldItem.transform.Rotate(Vector3.up, mouseX);
            //_heldItem.transform.Rotate(Vector3.right, mouseY, Space.World);
        }
    }
}
