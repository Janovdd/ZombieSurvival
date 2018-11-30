using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;


/************************************************************************************************
    Name        : DragDropPlacement
    Description : Same functionality in Placeable except that it is for drag and drop placement.
                  This script supports both mobile and pc platforms
*************************************************************************************************/
sealed class DragDropPlacement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // public variables
    public CoinManager coin;
    public TMP_Text costText;
    public int cost = 10;
    public GameObject                   buildingPrefab          = null;

    // private variables
    private bool                        cancel                  = false;
    private Ray                         ray;
    private Transform                   buildingTransform       = null;
    private List<RaycastResult>         rayResults              = new List<RaycastResult>();
    private PointerEventData            pointerEvData           = new PointerEventData(null);
    private GraphicRaycaster            g_raycaster             = null;

    private PlaceableCollisionDetector  collisionDetector       = null;
    private PlaceableSlopeDetector      slopeDetector           = null;



    void Start()
    {
        g_raycaster = GetComponentInParent<GraphicRaycaster>();
        costText.text = "" + cost;

    } // Start end


    void Update()
    {
        pointerEvData.position = Input.mousePosition;
        g_raycaster.Raycast(pointerEvData, rayResults);
        if (rayResults.Count > 0) cancel = true;
        else cancel = false;

        rayResults.Clear();

        if (cost <= coin.coins)
        {
            GetComponent<Image>().color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = Color.gray;
        }

        // Rotate selected building 45 degree
        // PC platforms only


        if (Input.touchCount > 1)
        {
            if (buildingTransform)
            buildingTransform.Rotate(transform.up, 90.0f);
        }



    } // Update end


    // Instantiate the building
    public void OnBeginDrag(PointerEventData ev)
    {
        ray = Camera.main.ScreenPointToRay(ev.position);
        RaycastHit rayHit = new RaycastHit();

        if (cost <= coin.coins)
        {
            buildingTransform = (Instantiate(buildingPrefab) as GameObject).transform;
            buildingTransform.position = ev.position;
            PlacementUtilities.SetLayer(buildingTransform.gameObject, "Highlight");
            // Set components reference
            slopeDetector = buildingTransform.GetComponent<PlaceableSlopeDetector>();
            collisionDetector = buildingTransform.GetComponent<PlaceableCollisionDetector>();
        }
  



    } // OnBeginDrag end


    // Update position
    public void OnDrag(PointerEventData ev)
    {
        Vector3 hitPoint = (Vector3)PlacementManager.Instance.GetMousePosition();

        if (PlacementManager.Instance.gridSnap)
        {
            buildingTransform.position = new Vector3(Mathf.Round(hitPoint.x / PlacementManager.Instance.gridSize) * PlacementManager.Instance.gridSize,
                hitPoint.y, Mathf.Round(hitPoint.z / PlacementManager.Instance.gridSize) * PlacementManager.Instance.gridSize);
        }
        // No grid snapping
        else
        {
            buildingTransform.position = hitPoint;
        }
    } // OnDrag end


    // Final placement
    public void OnEndDrag(PointerEventData ev)
    {
        // Cancel placement
        // this happens when the user drops the building back to the button
        // or when the building is dropped on illegal place
        if (!collisionDetector.legalPlace || !slopeDetector.legalSlope || cancel)
        {
            Destroy(buildingTransform.gameObject);
        }
        else
        {
            if (cost <= coin.coins)
            {
                coin.coins -= cost;
            }
            // Start building construction.
            // Remove this if you don't want construction animation
            buildingTransform.gameObject.AddComponent<BuildingConstruction>();
            // Destroy the components that we don't need anymore
            buildingTransform = null;
            Destroy(collisionDetector);
            Destroy(slopeDetector);
        }
    } // OnEndDrag end
}
