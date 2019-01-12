using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInputController : MonoBehaviour
{
    public GameObject TorpedoPrefab;
    public int TorpedoPerLevel = 10;
    private GameObject _torpedo;
    private TorpedoController _torpedoController;
    private int _torpedoAvailable;

    private Vector3 _originalTorpedoPosition;

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
           // _torpedo.transform.position = _originalTorpedoPosition;
            var torpedoRotation = new Quaternion(_torpedo.transform.rotation.x, _torpedo.transform.rotation.y, _torpedo.transform.rotation.z, _torpedo.transform.rotation.w); 

            _torpedoController.IsMoving = true;
            _torpedoController = null;
            _torpedoAvailable--;
            if (_torpedoAvailable > 0)
            {

                _torpedo = Instantiate(TorpedoPrefab);
                _torpedo.name = $"Torpedo{_torpedoAvailable}";
                _torpedo.transform.SetPositionAndRotation(_originalTorpedoPosition, torpedoRotation);
                _torpedoController = _torpedo.GetComponent<TorpedoController>();
                Debug.Log($"New torpedo created. Controller is {_torpedoController}");
                _originalTorpedoPosition = new Vector3(_torpedo.transform.position.x, _torpedo.transform.position.y, _torpedo.transform.position.z);
            }
        }
    }
}
