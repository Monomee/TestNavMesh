using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("Player")]   
    public Transform player;

    [Header("Minimap")]
    public Camera miniMapCamera;
    public GameObject teleText;
    public RectTransform miniMapUI;
    public RectTransform minMiniMap;
    public RectTransform maxMiniMap;

    [Header("Teleport")]
    public GameObject telePoint;
    public LayerMask layerMask;
    private bool isFullmap = false;
    private bool isReadyForTele = false;
    private Vector3 target;

    // Update is called once per frame
    void Update()
    {
        if (isReadyForTele)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                ChangeMinimapSize();
                Debug.Log("On");
            }
        }
        if (isFullmap)
        {
            telePoint.SetActive(true);
            ChoosePointToTele();
        }
        else
        {
            telePoint.SetActive(false);
        }
    }
   
    void ChangeMinimapSize()
    {
        isFullmap = !isFullmap;

        if (isFullmap)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            miniMapUI.position = maxMiniMap.position;
            miniMapUI.localScale = maxMiniMap.localScale;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            miniMapUI.position = minMiniMap.position;
            miniMapUI.localScale = minMiniMap.localScale;
        }
    }
    void ChoosePointToTele()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(miniMapUI, Input.mousePosition, null, out localPoint);
        
        // Convert local point to normalized space [0,1]
        Vector2 normalizedPoint = new Vector2(
            (localPoint.x + miniMapUI.rect.width * 0.5f) / miniMapUI.rect.width,
            (localPoint.y + miniMapUI.rect.height * 0.5f) / miniMapUI.rect.height
        );

        // Convert normalized point to pixel coordinates of render texture
        Vector2 renderTexPoint = new Vector2(
            normalizedPoint.x * miniMapCamera.pixelWidth,
            normalizedPoint.y * miniMapCamera.pixelHeight
        );

        Ray ray = miniMapCamera.ScreenPointToRay(renderTexPoint);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            telePoint.transform.position = hit.point;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                player.position = telePoint.transform.position;
                Debug.Log("Player:" + player.position);
                Debug.Log("TelePoint: " + telePoint.transform.position);
                ChangeMinimapSize();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            teleText.gameObject.SetActive(true);
            isReadyForTele=true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            teleText.gameObject.SetActive(false);
            isReadyForTele=false;
        }
    }
    
}
