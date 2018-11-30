using UnityEngine;


/************************************************************************************************
 * Name			: Placeable 
 * Description	: Updates building position and handle mouse and keyboard control.
 *                This class acts as a placement control, it communicates with the collision 
 *                and slope detector components and ensure that a placement position is legal
 *                before placing. Also handles building rotation and cancel.
 *                
 * Note         : This script is used by pc platforms only              
 ************************************************************************************************/
class Placeable : MonoBehaviour
{
    // private variables
    private Vector3 hitPoint;
	private PlaceableSlopeDetector slopeDetector;
	private PlaceableCollisionDetector collisionDetector;


	void Awake()
	{
		slopeDetector = GetComponent <PlaceableSlopeDetector>();
		collisionDetector = GetComponent <PlaceableCollisionDetector>();
        PlacementUtilities.SetLayer(gameObject, "Highlight");
    } // Awake end


    // Update is called once per frame
    void Update () 
	{
        // Update building position
        // ---------------------------------------------------------------------------------------------
        // Update position according to mouse position while ignoring terrain collision
        hitPoint = (Vector3) PlacementManager.Instance.GetMousePosition();

        // Snapping to grid is enabled
        if (PlacementManager.Instance.gridSnap)
        {
            float gridSize = PlacementManager.Instance.gridSize;

            transform.position = new Vector3(Mathf.Round(hitPoint.x / gridSize) * gridSize, hitPoint.y,
                Mathf.Round(hitPoint.z / gridSize) * gridSize);
        }
        // No grid snapping
        else
        {
            transform.position = hitPoint;
        }

        
        // Keyboard and mouse controls
        // ---------------------------------------------------------------------------------------------
        // Rotate selected building 45 degree
        if (Input.GetKeyDown(PlacementManager.Instance.rotationKey))
        {
			transform.Rotate (transform.up, 45.0f);
		}

        
        // Place the building
        if (Input.GetMouseButtonDown (0)) {

			// Position is legal, place the building
			if (collisionDetector.legalPlace && slopeDetector.legalSlope) {

				// Start building construction
                // Remove this if you don't want construction animation
                gameObject.AddComponent<BuildingConstruction>();

                // Destroy the components that we don't need anymore
                Destroy(collisionDetector);
                Destroy(slopeDetector);

                // Place the building.
                // We remove this script which stops placement control
                // leaving the building in its current position
                Destroy (this);
			}
		}

		// Cancel placement
		if (Input.GetMouseButtonDown (1)) {

			// Destroy the selected building (PlaceableHighlighter handles the consequences!).
			Destroy (gameObject);
		}
        // ----------------------------------------------------------------------------------------------

    } // Update end
    
}
