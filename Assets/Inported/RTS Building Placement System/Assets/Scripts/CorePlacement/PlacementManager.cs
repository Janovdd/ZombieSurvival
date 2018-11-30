using UnityEngine;


/************************************************************************************************
 * Name			: PlacementManager
 * Description 	: Manages system parameters, instantiate the building prefab upon selection
 *                and cache all the materials in every renderer found in the placeablesList
 ************************************************************************************************/
public sealed class PlacementManager : MonoBehaviour
{
    // public variables
    public bool                         gridSnap            = false;
	public float                        gridSize            = 1.0f;
	public float                        maxGradientAngle    = 0.0f;
	public Color                        highlightColor      = Color.red;
	public KeyCode                      rotationKey         = KeyCode.R;
	public LayerMask                    PlacementLayer;
    public LayerMask                    HighlightLayer;
	public GameObject[]                 placeablesList      = null;

    // private variables
    private Ray                         rayPos;
    private RaycastHit                  hitPoint;
	private bool                        selected            = false;
	private GameObject                  selectedBuilding    = null;
    private static PlacementManager     _instance           = null;


    // Singleton instance
    public static PlacementManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (PlacementManager)FindObjectOfType(typeof(PlacementManager));
            }
            return _instance;
        }
    }
    

    void Start()
    {
        CacheAllPlaceables();
    }

    
    void Update()
	{
        if (selected)
        {
            // Instantiate the building and add placeable component to control placement.
            // In the current frame the building will be instantiated at the world origin (0,0,0)
            // then in the next frame its position will be updated to current mouse position
            (Instantiate(selectedBuilding, Vector3.zero, Quaternion.identity) as GameObject).AddComponent<Placeable>();

            selected = false;
        }

    } // Update end


    // Main entry for building placement
    public void SelectBuilding(string name)
    {
        foreach (GameObject building in placeablesList)
        {
            if (building != null && building.name == name)
            {
                selected = true;
                selectedBuilding = building;

                return;
            }
        } // foreach end
        
        Debug.LogError("Building name doesn't match button name! Or building is null, assign one in the Inspector");

    } // SelectBuilding end


    // Cache all renderer(s) materials in all placeable buildings in the array including
    // their construction stages (if any).
    // Note that this access and cache the prefabs directly not the instance
    private void CacheAllPlaceables()
    {
        foreach (var go in placeablesList)
        {
            // cache the gameobject materials
            PlacementUtilities.CacheRenderers(go);

            // cache all gameobject construction stages materials
            var bInfo = GetComponent<BuildingInfo>();
            if (bInfo != null)
            {
                var stages = bInfo.ConstructionStage;
                if (stages.Count > 0)
                {
                    foreach (var stage in stages)
                    {
                        PlacementUtilities.CacheRenderers(stage);
                    }
                }
            }
        }
    }


    // Returns the position of the mouse in world coordinates
    public Vector3? GetMousePosition()
    {
        rayPos = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayPos, out hitPoint, Mathf.Infinity, 1 << LayerMask.NameToLayer("Terrain")))
        {
            return hitPoint.point;
        }

        return null;
    }
}
