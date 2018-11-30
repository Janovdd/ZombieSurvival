using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/************************************************************************************************
 * Name 		: BuildingConstruction
 * Description	: Handles UI slider and consruction prefabs animation.
 *                The animation here is configured for four stages.
 ************************************************************************************************/
sealed class BuildingConstruction : MonoBehaviour
{
    // private variables    
    private float           passedTime              = 0.0f;
    private float           quarterTime             = 1.0f;
    private float           maxYbounds              = 0.0f;
    private BuildingInfo    buildingInfo;
    private GameObject      currentPrefab           = null;
    private Canvas          canvas                  = null;
    private Slider          constructionSlider      = null;
    private Collider        goCollider              = null;
    private Vector3         sliderWorldPosition;
    private Vector3         sliderScreenPosition;


    void Awake()
    {
        buildingInfo = GetComponent<BuildingInfo>();

        // Reference to construction canvas
        canvas = GetComponentInChildren<Canvas>(true);

    } // Awake end


    void Start()
    {
        canvas.enabled = true;
        quarterTime = (float)buildingInfo.constructionTime / 4;
        goCollider = GetComponent<Collider>();
        maxYbounds = goCollider.bounds.max.y;

        // Slider configurations
        //---------------------------------------------------------------------------------------
        // Enable construction canvas gameObject
        //canvasObject.SetActive(true);

        // Setup canvas and slider references
        //canvas = canvasObject.GetComponentInChildren<Canvas>();
        constructionSlider = canvas.GetComponentInChildren<Slider>();

        // Disable slider interactability
        constructionSlider.interactable = false;

        // Configure slider to be float(smooth) or integer
        constructionSlider.wholeNumbers = false;

        // Set slider starting and end values
        constructionSlider.minValue = passedTime;
        constructionSlider.maxValue = buildingInfo.constructionTime;
        //---------------------------------------------------------------------------------------

        // Animate slider
        StartCoroutine(AnimateSlider());

        // Animate construction prefabs
        if (buildingInfo.enableStages)
        {
            StartCoroutine(AnimateConstruction());
        }
        
    } // Start end


    void Update()
    {
        passedTime += Time.deltaTime;

    } // Update end


    private IEnumerator AnimateSlider()
    {
        while (true)
        {
            constructionSlider.value = passedTime;

            // Preserve slider position
            sliderWorldPosition = new Vector3(transform.position.x, maxYbounds, transform.position.z);
            sliderScreenPosition = Camera.main.WorldToScreenPoint(sliderWorldPosition);
            constructionSlider.transform.position = sliderScreenPosition;

            if (passedTime > buildingInfo.constructionTime)
            {
                Destroy(buildingInfo);
                canvas.enabled = false;
                Destroy(this);
            }

            yield return null;
            
        } // while end
        
    } // AnimateSlider end


    private IEnumerator AnimateConstruction()
    {
        //--------
        //Stage 0%
        //--------
        gameObject.AddComponent<OtherHighlighter>();
        // Hide original building
        PlacementUtilities.ActivateRenderers(gameObject, false);
        // Disabling box collider disables highlighting and any undesired effects
        goCollider.enabled = false;

        // Instantiate prefab
        if (buildingInfo.ConstructionStage[0] != null)
        {
            currentPrefab = (GameObject)Instantiate(buildingInfo.ConstructionStage[0],
                transform.position, transform.rotation);
        }
        
        while (true)
        {
            //---------
            //Stage 25%
            //---------
            if (Mathf.Approximately(Mathf.Round(passedTime * 4) / 4f, quarterTime))
            {
                // Destroy previous stage (0%) prefab
                Destroy(currentPrefab);
                if (buildingInfo.ConstructionStage[1] != null)
                {
                    currentPrefab = (GameObject)Instantiate(buildingInfo.ConstructionStage[1],
                        transform.position, transform.rotation);
                }
            }

            //---------
            //Stage 50%
            //---------
            else if (Mathf.Approximately(Mathf.Round(passedTime * 4) / 4, quarterTime * 2f))
            {
                // Destroy previous stage (25%) prefab
                Destroy(currentPrefab);
                if (buildingInfo.ConstructionStage[2] != null)
                {
                    currentPrefab = (GameObject)Instantiate(buildingInfo.ConstructionStage[2],
                        transform.position, transform.rotation);
                }
            }

            //---------
            //Stage 75%
            //---------
            else if(Mathf.Approximately(Mathf.Round(passedTime * 4) / 4, quarterTime * 3f))
            {
                // Destroy previous stage (50%) prefab
                Destroy(currentPrefab);
                if (buildingInfo.ConstructionStage[3] != null)
                {
                    currentPrefab = (GameObject)Instantiate(buildingInfo.ConstructionStage[3],
                        transform.position, transform.rotation);
                }
            }

            //----------
            //Stage 100%
            //----------
            if (passedTime > buildingInfo.constructionTime)
            {
                // Show original building
                //--------------------------------------------------------------
                // Destroy previous stage prefab
                Destroy(currentPrefab);
                // Enable gameObject renderer/s
                PlacementUtilities.ActivateRenderers(gameObject, true);
                // Activate the collider
                goCollider.enabled = true;
            }
            
            yield return null;
            
        } // While end

    } // AnimateConstruction end
}
