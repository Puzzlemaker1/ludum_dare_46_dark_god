using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVec = new Vector3();
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveVec.y += moveSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveVec.x -= moveSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveVec.y -= moveSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveVec.x += moveSpeed;
        }
        this.transform.position += moveVec;

        //Handle clicking on game objects
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = this.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("Click!");
            if (Physics.Raycast(ray, out hit))
            {
                // the object identified by hit.transform was clicked
                // do whatever you want
                Debug.Log(hit.transform.gameObject);
                Tile clickedTile = hit.transform.gameObject.GetComponent<Tile>();
                if(clickedTile != null)
                {
                    Debug.Log("Tile clicked using raycast");
                }
            }
        }
    }
}
