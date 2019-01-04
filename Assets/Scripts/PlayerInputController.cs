using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInputController : MonoBehaviour
{
    private GameObject _torpedo;
    private TorpedoController _torpedoController;

    private Vector3 _originalTorpedoPosition;

    void Start()
    {
        _torpedo = GameObject.FindGameObjectWithTag("Torpedo");
        _torpedoController = _torpedo.GetComponent<TorpedoController>();
        _originalTorpedoPosition = _torpedo.transform.position;
        //_torpedo.SetActive(false);
        var vb = new CrossPlatformInputManager.VirtualButton("Fire1");
        CrossPlatformInputManager.RegisterVirtualButton(vb);
    }
    
    // Update is called once per frame
    void Update()
    {
        var horRotation = Input.GetAxis("Horizontal");
        //Debug.Log($"Hor rotation = {horRotation}");
        this.gameObject.transform.Rotate(new Vector3(0, 1, 0), horRotation);
        if (!_torpedoController.IsMoving)
        {
            _torpedo.transform.Rotate(new Vector3(0, 1, 0), horRotation);
        }

        if (!_torpedoController.IsMoving && CrossPlatformInputManager.GetButton("Fire1"))
        {
            Debug.Log("FIRE");
            //_torpedo.SetActive(true);
            _torpedo.transform.position = _originalTorpedoPosition;
           // _torpedo.transform.rotation = this.gameObject.transform.rotation;
            _torpedoController.IsMoving = true;
        }
    }
}
