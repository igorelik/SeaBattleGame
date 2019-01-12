using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipController : MonoBehaviour
{
    public float Speed = 1f;
    public int MaxHits = 1;

    // Start is called before the first frame update
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    private int _hitCount = 0;
    void Start()
    {
        _originalPosition = this.gameObject.transform.position;
        _originalRotation = this.gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(Speed, 0, 0);
    }

    // OnControllerColliderHit is called when the controller hits a collider while performing a Move
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log($"ShipController OnControllerColliderHit");
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"ShipController is triggered and melting");
        //if (other.transform.gameObject.tag == "Scene")
        //{
        //    Debug.Log($"End of the world. Turning");
        //    Speed *= -1;
        //    return;
        //}

        //other.ClosestPointOnBounds()
    }

}
