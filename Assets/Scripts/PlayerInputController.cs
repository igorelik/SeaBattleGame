using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var horRotation = Input.GetAxis("Horizontal");
        //Debug.Log($"Hor rotation = {horRotation}");
        this.gameObject.transform.Rotate(new Vector3(0, 1, 0), horRotation);
    }
}
