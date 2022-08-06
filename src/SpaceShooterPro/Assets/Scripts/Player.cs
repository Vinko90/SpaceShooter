using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed = 3.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        //Start by positioning player to (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Get movement value from key manager
        float horizontalInput = Input.GetAxis("Horizontal");
        
        //Move Player: Left/Right * HorizontalInput * PlayerSpeed * Time
        transform.Translate(Vector3.right * horizontalInput * playerSpeed * Time.deltaTime);
    }
}
