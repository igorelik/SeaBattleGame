using UnityEngine;

public class TorpedoController : MonoBehaviour
{
    public float Speed = 1f;
    public float MaxRange = 10000;
    public bool IsMoving = false;

    private float MaxRangeSquared => MaxRange * MaxRange;

    // Start is called before the first frame update
    void Start()
    {
        
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
            return;
        }
        if (IsMoving)
        {
            this.gameObject.transform.position += this.gameObject.transform.right * Speed;
        }
        
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Debug.Log("Torpedo collided - BOOM");
            Object.Destroy(this.gameObject);
        }
    }
}
