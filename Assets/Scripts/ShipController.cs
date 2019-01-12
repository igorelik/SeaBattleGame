using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float Speed = 1f;
    public int MaxHits = 1;
    public Transform ExplosionPrefab;
    public AudioClip ExplosionEffect;

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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"ShipController is triggered and melting");
        if (collision.rigidbody.gameObject.CompareTag("Torpedo"))
        {
            var contactPoint = collision.contacts[0];
            var explosionPosition = new Vector3(contactPoint.point.x, 0, contactPoint.point.z);
            var explosion = Instantiate(ExplosionPrefab, explosionPosition, Quaternion.LookRotation(Vector3.up));
            explosion.transform.parent = this.gameObject.transform;
            AudioSource.PlayClipAtPoint(ExplosionEffect,  explosionPosition);
        }
    }

}
