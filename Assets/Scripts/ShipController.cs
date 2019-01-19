using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float Speed = 1f;
    public int MaxHits = 1;
    public Transform ExplosionPrefab;
    public AudioClip ExplosionEffect;
    public CharacterController CharacterController;
    public Vector3[] WayPoints;
    public float TurnDistance = 10;

    // Start is called before the first frame update
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    private int _hitCount = 0;
    private List<Transform> _explosions = new List<Transform>();
    private int _nextWayPointIdx = 1;

    void Start()
    {
        _originalPosition = this.gameObject.transform.position;
        _originalRotation = this.gameObject.transform.rotation;
        var rotation = Quaternion.LookRotation(WayPoints[1]- WayPoints[0]);
        this.gameObject.transform.SetPositionAndRotation(WayPoints[0], rotation);
        this.gameObject.transform.Rotate(0, -90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        var diff = WayPoints[_nextWayPointIdx] - this.gameObject.transform.position;
        if (diff.sqrMagnitude > TurnDistance * TurnDistance)
        {
            this.gameObject.transform.position += this.gameObject.transform.right * Speed;
        }
        else
        {
            var nextIdx = (_nextWayPointIdx + 1) % WayPoints.Length;
            var rotation = Quaternion.LookRotation(WayPoints[nextIdx] - WayPoints[_nextWayPointIdx]);
            this.gameObject.transform.SetPositionAndRotation(this.gameObject.transform.position, rotation);
            this.gameObject.transform.Rotate(0, -90, 0);
            _nextWayPointIdx = nextIdx;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{this.gameObject.name} is triggered and melting");
        if (collision.rigidbody.gameObject.CompareTag("Torpedo"))
        {
            var contactPoint = collision.contacts[0];
            var explosionPosition = new Vector3(contactPoint.point.x, 0, contactPoint.point.z);
            var explosion = Instantiate(ExplosionPrefab, explosionPosition, Quaternion.LookRotation(Vector3.up));
            _explosions.Add(explosion);
            explosion.transform.parent = this.gameObject.transform;
            AudioSource.PlayClipAtPoint(ExplosionEffect,  explosionPosition);
            _hitCount++;
            Speed *= 0.7f;
            var rotation = Quaternion.LookRotation( WayPoints[_nextWayPointIdx] - this.gameObject.transform.position);
            this.gameObject.transform.SetPositionAndRotation(this.gameObject.transform.position, rotation);
            this.gameObject.transform.Rotate(0, -90, 0);
            if (_hitCount == MaxHits)
            {
                Speed *= 0.1f;
                var sinkAnimator = this.GetComponentInChildren<Animator>();
                if (sinkAnimator != null)
                {
                    var length = sinkAnimator.runtimeAnimatorController.animationClips[0].length;
                    Debug.Log($"{this.gameObject.name} sinking animation is {length} sec");
                    sinkAnimator.enabled = true;
                    StartCoroutine(DestroySunkenShip(length - 0.5f));
                }
                else
                {
                    StartCoroutine(DestroySunkenShip(0.5f));
                }
            }
        }
    }

    private IEnumerator DestroySunkenShip(float delay)
    {
        yield return new WaitForSeconds(delay/2);
        foreach (var explosion in _explosions)
        {
            Object.Destroy(explosion.gameObject);
        }
        yield return new WaitForSeconds(delay/2);
        Object.Destroy(this.gameObject);
    }

}
