using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float Speed = 1f;
    // Start is called before the first frame update
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"ShipController is triggered and melting");
        if (other.transform.gameObject.tag == "Scene")
        {
            Debug.Log($"End of the world. Turning");
            Speed *= -1;
            return;
        }
    }

}
