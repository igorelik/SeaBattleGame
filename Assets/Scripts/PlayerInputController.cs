using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInputController : MonoBehaviour
{
    public GameObject TorpedoPrefab;
    private GameObject _torpedo;
    private TorpedoController _torpedoController;

    private Vector3 _originalTorpedoPosition;

    void Start()
    {
        _torpedo = Instantiate(TorpedoPrefab);
        _torpedoController = _torpedo.GetComponent<TorpedoController>();
        _originalTorpedoPosition = _torpedo.transform.position;
        var vb = new CrossPlatformInputManager.VirtualButton("Fire1");
        CrossPlatformInputManager.RegisterVirtualButton(vb);
    }
    
    // Update is called once per frame
    void Update()
    {
        var horRotation = Input.GetAxis("Horizontal");
        //Debug.Log($"Hor rotation = {horRotation}");
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

            _torpedo = Instantiate(TorpedoPrefab);
            _torpedo.transform.SetPositionAndRotation(_torpedo.transform.position, torpedoRotation);
            _torpedoController = _torpedo.GetComponent<TorpedoController>();
            _originalTorpedoPosition = _torpedo.transform.position;

        }
    }
}
