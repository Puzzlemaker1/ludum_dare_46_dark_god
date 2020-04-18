using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int xSize, ySize;
    public float xScale, yScale;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < xSize; x++)
        {
            for(int y = 0; y < ySize; y++)
            {
                Instantiate(prefab, new Vector3(xScale * x, yScale * y), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
