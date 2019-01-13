using System.Collections;
using System.Collections.Generic;
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
    private List<Transform> _explosions = new List<Transform>();

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
            if (_hitCount == MaxHits)
            {
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
