using UnityEngine;
using System.Collections.Generic;


/************************************************************************************************
 * Name			: PlaceableCollisionDetection
 * Description 	: Detects collision with other buildings or objects.
 *                Highlight/unhighlight selected building 
 ************************************************************************************************/
sealed class PlaceableCollisionDetector : MonoBehaviour
{
    // private variables
    private int                         otherLayer      = 0;
    private bool                        _legalPlace     = false;
    private LayerMask                   terrainLayer;
    private PlaceableSlopeDetector      slopeDetector   = null;
    private List<Collider>              collidersList   = new List<Collider>();

    public bool legalPlace
    {
        get { return _legalPlace; }
    }


    void Start ()
    {
        terrainLayer = PlacementManager.Instance.PlacementLayer;
        slopeDetector = GetComponent<PlaceableSlopeDetector>();

    } // Start end


    void OnTriggerEnter(Collider other)
	{
		otherLayer = other.gameObject.layer;

        // Detect collision, ignoring terrain collision
        if ((terrainLayer.value & 1 << otherLayer) != (1 << otherLayer))
        {
            collidersList.Add(other);
        }

    } // OnTrigger end


    void OnTriggerExit(Collider other)
	{
		otherLayer = other.gameObject.layer;

        // ignore terrain collision
        if ((terrainLayer.value & 1 << otherLayer) != (1 << otherLayer))
        {
            collidersList.Remove(other);
        }

        // clean up any missing reference so we don't stuck highlighting forever (collidersList.count > 0)
        // this is typically caused by the construction prefabs being destroyed and instantiated
        collidersList.RemoveAll(Collider => Collider == null);

	} // OnTrigger end


    bool highlight = false;

    // Update is called once per frame
    void Update()
	{
        // this is the code for highlighting the placeable building currently selected
        if (collidersList.Count > 0 || !slopeDetector.legalSlope)
        {
            _legalPlace = false;

            if (!highlight)
            {
                PlacementUtilities.Highlight(gameObject, PlacementManager.Instance.highlightColor);

                highlight = true;
            }
        }
        else
        {
            _legalPlace = true;
            if (highlight)
            {
                PlacementUtilities.Unhighlight(gameObject);
                highlight = false;
            }
        }
        
    } // Update end


    // handles unhighlighting when placement is cancelled
    void OnDestroy()
    {
        if (!_legalPlace)
        {
            foreach (var col in collidersList)
            {
                // Unhighlight
                if (col != null)
                    if (col.GetComponent<OtherHighlighter>() == true)
                    {
                        PlacementUtilities.Unhighlight(col.gameObject);
                    }
            }
        }
    }
}