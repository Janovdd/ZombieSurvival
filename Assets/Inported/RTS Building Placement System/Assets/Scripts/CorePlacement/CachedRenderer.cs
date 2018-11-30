using UnityEngine;


/************************************************************************************************
 * Name 		: CachedRenderer
 * Description	: This script is attached to every renderer in a gameobject you want to highlight.
 *                It stores the renderer original materials and highlighting materials.
 *                The materials are cached before the game run so that its fast when we 
 *                highlight/unhighlight during play, this makes performance gains in execution
 *                time and garbage collection (GC).
 *                You can call the CacheMaterials() function during play if the materials
 *                are changed to update (cache again) the materials arrays.
 ************************************************************************************************/
sealed class CachedRenderer : MonoBehaviour
{
    // public variables
    public Material[]   originalMaterials     = null;
    public Material[]   highlightMaterials    = null;


    // Cache the renderer and highlight materials.
    // This function is useful if your object materials has changed during runtime
    // where you might need to cache again by accessing this component and calling this function
    public void CacheMaterials()
    {
        originalMaterials = GetComponent<Renderer>().sharedMaterials;

        // cache the highlighting materials
        highlightMaterials = new Material[originalMaterials.Length];
        for (int i = 0; i < highlightMaterials.Length; i++)
        {
            highlightMaterials[i]       = new Material(originalMaterials[i]);       // new temporary material
            highlightMaterials[i].color = PlacementManager.Instance.highlightColor;
        }
    }
}
