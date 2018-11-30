using UnityEngine;
using System.Collections.Generic;


/************************************************************************************************
 * Name 		: BuildingInfo
 * Description	: Stores building construction stages prefab and construction time
 ************************************************************************************************/
class BuildingInfo : MonoBehaviour
{
    // public variables
    public int constructionTime = 1;
    public bool enableStages = false;
    public List<GameObject> ConstructionStage = null;
    
}
