using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private PlayerInputController _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindObjectOfType<PlayerInputController>();
        Debug.Log($"Player Controller {_player}");
    }

    // Update is called once per frame
    void Update()
    {
        var targets = this.gameObject.GetComponentsInChildrenWithTag<Transform>("Target");
        if (!targets.Any())
        {
            Debug.Log("Game over - you won");
            return;
        }

        var torpedos = GameObject.FindGameObjectsWithTag("Torpedo");

    }
}
