using UnityEngine;

public class TorpedoController : MonoBehaviour
{
    public float Speed = 0.1f;
    public float MaxRange = 10000;
    public bool IsMoving = false;

    private float MaxRangeSquared => MaxRange * MaxRange;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"New torpedo {this.gameObject.name}. Position {this.gameObject.transform.position}. Distance from 0 - {this.gameObject.transform.position.magnitude}");
        
    }

    private void OnEnable()
    {

    }



    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.sqrMagnitude > MaxRangeSquared)
        {
            Object.Destroy(this.gameObject);
            //IsMoving = false;
            return;
        }
        if (IsMoving)
        {
            this.gameObject.transform.position += this.gameObject.transform.right * Speed;
        }
        
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnCollisionEnter(Collision other)
//    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Target"))
        {
            Debug.Log($"{this.gameObject.name} collided with {other.gameObject.name} - BOOM");
            Object.Destroy(this.gameObject);
            //IsMoving = false;
        }
    }
}
