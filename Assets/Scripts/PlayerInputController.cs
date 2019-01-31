using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInputController : MonoBehaviour
{
    public GameObject TorpedoPrefab;
    public int TorpedoPerLevel = 10;
    public float TimeDelayBetweenShots = 1;
    private GameObject _torpedo;
    private TorpedoController _torpedoController;
    private int _torpedoAvailable;
    private Vector3 _originalTorpedoPosition;

    public bool CanFire => _torpedoAvailable > 1;

    void Start()
    {
        _torpedo = Instantiate(TorpedoPrefab);
        _torpedoController = _torpedo.GetComponent<TorpedoController>();
        _originalTorpedoPosition = new Vector3(_torpedo.transform.position.x, _torpedo.transform.position.y, _torpedo.transform.position.z);
        var vb = new CrossPlatformInputManager.VirtualButton("Fire1");
        CrossPlatformInputManager.RegisterVirtualButton(vb);
        ResetTorpedos();
    }

    private void ResetTorpedos()
    {
        _torpedoAvailable = TorpedoPerLevel;
    }

    void Update()
    {
        var horRotation = Input.GetAxis("Horizontal");
        this.gameObject.transform.Rotate(new Vector3(0, 1, 0), horRotation);
        if (_torpedoController != null && !_torpedoController.IsMoving)
        {
            _torpedo.transform.Rotate(new Vector3(0, 1, 0), horRotation);
        }

        var isFireTrigger = CrossPlatformInputManager.GetButtonUp("Fire1");

        if (_torpedoController != null && !_torpedoController.IsMoving && isFireTrigger)
        {
            Debug.Log($"FIRE!!! {_torpedoAvailable} left");
            _torpedoController.IsMoving = true;
            _torpedoController = null;
            _torpedoAvailable--;
            if (_torpedoAvailable > 1)
            {
                StartCoroutine(CreateNewTorpedo());
            }
        }
    }

    private IEnumerator CreateNewTorpedo()
    {
        yield return new WaitForSeconds(TimeDelayBetweenShots);
        _torpedo = Instantiate(TorpedoPrefab);
        _torpedo.name = $"Torpedo{_torpedoAvailable}";
        _torpedo.transform.SetPositionAndRotation(_originalTorpedoPosition, this.gameObject.transform.rotation);
        _torpedo.transform.Rotate(Vector3.up, -90);
        _torpedoController = _torpedo.GetComponent<TorpedoController>();
        Debug.Log($"New torpedo created. Controller is {_torpedoController}");
        _originalTorpedoPosition = new Vector3(_torpedo.transform.position.x, _torpedo.transform.position.y,_torpedo.transform.position.z);
    }
}
