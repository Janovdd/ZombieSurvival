using UnityEngine;


/************************************************************************************************
 * Name			: PlaceableSlopeDetector
 * Description 	: Detects terrain slope by calculating the gradient angle underneath the building.
 ************************************************************************************************/
sealed class PlaceableSlopeDetector : MonoBehaviour
{
    // private variables
    private float   ToDegree    = 0.0f;
    private bool    _legalSlope = true;

    private Ray         r1;
    private Ray         r2;
    private Ray         r3;
    private Ray         r4;
    private Ray         r5;
    private Ray         r6;
    private Ray         r7;
    private Ray         r8;

    private Vector3     p1;
    private Vector3     p2;
    private Vector3     p3;
    private Vector3     p4;
    private Vector3     p5;
    private Vector3     p6;
    private Vector3     p7;
    private Vector3     p8;

    private float       angle1;
    private float       angle2;
    private float       angle3;
    private float       angle4;
    private float       angle5;
    private float       angle6;
    private float       angle7;
    private float       angle8;
    
    private RaycastHit  hit1;
    private RaycastHit  hit2;
    private RaycastHit  hit3;
    private RaycastHit  hit4;
    private RaycastHit  hit5;
    private RaycastHit  hit6;
    private RaycastHit  hit7;
    private RaycastHit  hit8;

    private Collider    goCollider      = null;
    private float       maxGradient     = 0.0f;

    public bool legalSlope
    {
        get { return _legalSlope; }
    }


    void Start()
    {
        ToDegree    = 180.0f / Mathf.PI;
        goCollider  = GetComponent<BoxCollider>();
        maxGradient = PlacementManager.Instance.maxGradientAngle;

    }// Start end


    // Update is called once per frame
    void Update()
    {
        Bounds bounds = goCollider.bounds;
        
        /******************************************************************************************* 
         *  Eight rays are used to detect gradient angle.
         *  the rays depend on the bounding box of the building box collider
         *  four on the corners of the bound and another four on the middle between each two corners
         *******************************************************************************************/

        // bounds corners
        p1.x = bounds.min.x - 1f;
        p1.y = bounds.max.y;
        p1.z = bounds.min.z - 1f;

        p2.x = bounds.max.x + 1f;
        p2.y = bounds.max.y;
        p2.z = bounds.min.z - 1f;

        p3.x = bounds.min.x - 1f;
        p3.y = bounds.max.y;
        p3.z = bounds.max.z + 1f;

        p4.x = bounds.max.x + 1f;
        p4.y = bounds.max.y;
        p4.z = bounds.max.z + 1;
            
        // middle corners
        p5.x = transform.position.x;
        p5.y = bounds.max.y;
        p5.z = bounds.min.z - 1f;

        p6.x = transform.position.x;
        p6.y = bounds.max.y;
        p6.z = bounds.max.z + 1f;

        p7.x = bounds.min.x - 1f;
        p7.y = bounds.max.y;
        p7.z = transform.position.z;

        p8.x = bounds.max.x + 1f;
        p8.y = bounds.max.y;
        p8.z = transform.position.z;

        // Rays positions
        r1.origin = p1;
        r1.direction = Vector3.down;

        r2.origin = p2;
        r2.direction = Vector3.down;

        r3.origin = p3;
        r3.direction = Vector3.down;

        r4.origin = p4;
        r4.direction = Vector3.down;
        
        r5.origin = p5;
        r5.direction = Vector3.down;

        r6.origin = p6;
        r6.direction = Vector3.down;

        r7.origin = p7;
        r7.direction = Vector3.down;

        r8.origin = p8;
        r8.direction = Vector3.down;


        /*******************************************************************************************
         * Dot product of two unit vectors yield the cosine of the angle between them
         * use the inverse function to get the angle and convert to degree
         *******************************************************************************************/
         
        if (Physics.Raycast(r1, out hit1, 100.0f))
            angle1 = Mathf.Acos(Vector3.Dot(hit1.normal, Vector3.up)) * ToDegree;

        if (Physics.Raycast(r2, out hit2, 100.0f))
            angle2 = Mathf.Acos(Vector3.Dot(hit2.normal, Vector3.up)) * ToDegree;

        if (Physics.Raycast(r3, out hit3, 100.0f))
            angle3 = Mathf.Acos(Vector3.Dot(hit3.normal, Vector3.up)) * ToDegree;

        if (Physics.Raycast(r4, out hit4, 100.0f))
            angle4 = Mathf.Acos(Vector3.Dot(hit4.normal, Vector3.up)) * ToDegree;

        if (Physics.Raycast(r5, out hit5, 100.0f))
            angle5 = Mathf.Acos(Vector3.Dot(hit5.normal, Vector3.up)) * ToDegree;

        if (Physics.Raycast(r6, out hit6, 100.0f))
            angle6 = Mathf.Acos(Vector3.Dot(hit6.normal, Vector3.up)) * ToDegree;

        if (Physics.Raycast(r7, out hit7, 100.0f))
            angle7 = Mathf.Acos(Vector3.Dot(hit7.normal, Vector3.up)) * ToDegree;

        if (Physics.Raycast(r8, out hit8, 100.0f))
            angle8 = Mathf.Acos(Vector3.Dot(hit8.normal, Vector3.up)) * ToDegree;


        if (angle1 > maxGradient ||
            angle2 > maxGradient ||
            angle3 > maxGradient ||
            angle4 > maxGradient ||
            angle5 > maxGradient ||
            angle6 > maxGradient ||
            angle7 > maxGradient ||
            angle8 > maxGradient)
        {
            _legalSlope = false;
        }
        else
        {
            _legalSlope = true;
        }
        
    } // Update end

}
