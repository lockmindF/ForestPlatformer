using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 position = this.transform.position;
        position.y = player.transform.position.y + 1.2f;
        position.x = player.transform.position.x;

        this.transform.position = position;
    }
}
