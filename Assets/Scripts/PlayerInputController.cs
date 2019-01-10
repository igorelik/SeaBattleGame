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
        _originalTorpedoPosition = _torpedo.transform.position;
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

        if (_torpedoController != null && !_torpedoController.IsMoving && CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            Debug.Log("FIRE");
            _torpedo.transform.position = _originalTorpedoPosition;
            var torpedoRotation = _torpedo.transform.rotation;

            _torpedoController.IsMoving = true;
            _torpedoController = null;
            _torpedoAvailable--;
            if (_torpedoAvailable > 0)
            {

                _torpedo = Instantiate(TorpedoPrefab);
                _torpedo.transform.SetPositionAndRotation(_torpedo.transform.position, torpedoRotation);
                _torpedoController = _torpedo.GetComponent<TorpedoController>();
                _originalTorpedoPosition = _torpedo.transform.position;
            }
        }
    }
}
