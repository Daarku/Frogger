using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    public Camera Camera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _Distancejoint;
    public Rigidbody2D rig;
    public float Strenght = 2000;
    private Vector3 MouseDir;
    public Transform LineThing;
    public bool isGrappling;
    public Transform lookToHook;

    [SerializeField] private float maxRopeLength = 10f;

    // Start is called before the first frame update
    void Start()
    {
        isGrappling = true;
        _Distancejoint.autoConfigureDistance = true;
        _Distancejoint.enabled = false;
        _lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MouseDir = Camera.ScreenToWorldPoint(Input.mousePosition);

        if (isGrappling)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector2 mousepos = (Vector2)Camera.ScreenToWorldPoint(Input.mousePosition);
                _lineRenderer.SetPosition(0, mousepos);
                _lineRenderer.SetPosition(1, transform.position);
                _Distancejoint.connectedAnchor = mousepos;
                _Distancejoint.enabled = true;
                LineThing.position = mousepos;
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                _lineRenderer.SetPosition(1, transform.position);
                _lineRenderer.enabled = true;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _Distancejoint.enabled = false;
                _lineRenderer.enabled = false;
            }

            if (_Distancejoint.enabled)
            {
                Vector2 playerToAnchor = _Distancejoint.connectedAnchor - (Vector2)transform.position;
                if (playerToAnchor.magnitude > maxRopeLength)
                {
                    playerToAnchor = playerToAnchor.normalized * maxRopeLength;
                    _Distancejoint.connectedAnchor = (Vector2)transform.position + playerToAnchor;
                }
                _lineRenderer.SetPosition(0, _Distancejoint.connectedAnchor);
                _lineRenderer.SetPosition(1, transform.position);
            }

            if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Mouse0))
            {
                Vector3 Direction = LineThing.position - transform.position;
                rig.velocity = new Vector2(Direction.x * Strenght, Direction.y * Strenght).normalized * Strenght * Time.deltaTime;
                rig.AddForce(Direction.normalized * Strenght * Time.deltaTime);
                _Distancejoint.enabled = false;
            }

            if (Input.GetKeyUp(KeyCode.E) && Input.GetKey(KeyCode.Mouse0))
            {
                _Distancejoint.enabled = true;
            }
        }
    }
}
