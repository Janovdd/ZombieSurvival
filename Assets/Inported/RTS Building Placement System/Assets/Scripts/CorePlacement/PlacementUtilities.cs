using UnityEngine;


/************************************************************************************************
 * Name 		: PlacementUtilities
 * Description	: Provides helper functions for building placement
 ************************************************************************************************/
public static class PlacementUtilities
{
    // find a gameObject in parent childrens tagged with tag and return it
    public static GameObject FindWithTagInChild(GameObject parent, string tag)
    {
        if (parent != null)
        {
            Transform[] tArray = parent.GetComponentsInChildren<Transform>(true);

            for (int i = 0; i < tArray.Length; i++)
            {
                if (tArray[i].tag == tag)
                {
                    return tArray[i].gameObject;
                }
            }
        }

        // not found
        Debug.LogError("No GameObject with tag '" + tag + "' found!");
        return null;
    }


    /// enable/disable renderers in a gameObject
    public static void ActivateRenderers(GameObject building, bool _setActive)
    {
        if (building != null)
        {
            foreach (var rend in building.GetComponentsInChildren<Renderer>())
            {
                if (rend != null)
                {
                    rend.enabled = _setActive;
                }
            }
        }
    }


	/// cache each renderer materials in gameObject
	public static void CacheRenderers(GameObject building)
	{
        if (building != null)
        {
            foreach (var rend in building.GetComponentsInChildren<Renderer>())
            {
                if (rend != null)
                {
                    // avoid duplication of component
                    if (rend.GetComponent<CachedRenderer>() == null)
                    {
                        // add the component and cache the materials
                        rend.gameObject.AddComponent<CachedRenderer>().CacheMaterials();
                    }
                    else    // the component exists, cache the materials
                    {
                        rend.GetComponent<CachedRenderer>().CacheMaterials();
                    }
                }
            }
        }
	}


    // set a gameObject and its childrens(Renderer) layer to _layer
    public static void SetLayer(GameObject go, string _layer)
    {
        go.layer = LayerMask.NameToLayer(_layer);

        foreach (var rend in go.GetComponentsInChildren<Renderer>())
        {
            rend.gameObject.layer = LayerMask.NameToLayer(_layer);
        }
    }


    /// Highlight the gameObject with the specified color
    public static void Highlight(GameObject building, Color color)
	{
        if (building != null)
        {
            // foreach renderer copy original materials, change the color and assign back
            foreach (var rend in building.GetComponentsInChildren<Renderer>())
            {
                if (rend != null)
                {
                    var c = rend.GetComponent<CachedRenderer>();

                    rend.materials = c ? c.highlightMaterials : null;
                }
            }
        }
	}


	/// Unhighlight the gameObject
	public static void Unhighlight(GameObject building)
	{
        if (building != null)
        {
            foreach (var rend in building.GetComponentsInChildren<Renderer>())
            {
                if (rend != null)
                {
                    var c = rend.GetComponent<CachedRenderer>();

                    // Replace highlight materials with original materials
                    rend.materials = c ? c.originalMaterials : null;
                }
            }
        }
	}

}

