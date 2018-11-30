using UnityEngine;


/************************************************************************************************
 * Name 		: OtherCollisionDetector
 * Description	: Controls highlighting on other buildings.
 *                This script should be attached to the buildings in your scene that you want to
 *                highlight when the selected building collides with them and don't forget to set
 *                the building layer to 'Highlight'
 ************************************************************************************************/
sealed class OtherHighlighter : MonoBehaviour
{
    // private variables
    private int otherLayer;
    private LayerMask buildingsLayer;

    
    void Start()
    {
        buildingsLayer = PlacementManager.Instance.HighlightLayer;
        PlacementUtilities.CacheRenderers(gameObject);
    }

    
    void OnTriggerEnter(Collider other)
    {
        otherLayer = other.gameObject.layer;

        // highlight only the collided building with 'Highlight' layer set
        if ((buildingsLayer.value & 1 << otherLayer) == (1 << otherLayer))
        {
            PlacementUtilities.Highlight(gameObject, PlacementManager.Instance.highlightColor);
        }
    }


    void OnTriggerExit(Collider other)
    {
        otherLayer = other.gameObject.layer;

        if ((buildingsLayer.value & 1 << otherLayer) == (1 << otherLayer))
        {
            PlacementUtilities.Unhighlight(gameObject);
        }
    }
}
