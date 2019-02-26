using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexComb : MonoBehaviour {

    public GameObject hexTile;
    public Vector2 centerOfComb;
    public int layersOfComb;
    public float tileSide;
    public bool generate;

    public List<GameObject> HexTiles { get; set; }
    private float sqr3 = Mathf.Sqrt(3);


    private void Awake()
    {
        if (generate)
        {
            Generate();
        }
    }

    public void Generate()
    {
        //gameObject.transform.position = new Vector3(centerOfComb.x, 0, centerOfComb.y);
        gameObject.transform.position = new Vector3(centerOfComb.x, centerOfComb.y,0);

        HexTiles = new List<GameObject>();
        int widtOfComb = layersOfComb * 2 + 1;

        for (int i = layersOfComb * -1; i < layersOfComb + 1; i++)
        {
            if (i < 0)
            {
                if (i % 2 == 0)
                {
                    for (int j = (widtOfComb + i) / -2; j < (widtOfComb + i) / 2 + 1; j++)
                    {

                        GameObject tile = Instantiate(hexTile, new Vector2(j * sqr3 * tileSide - (tileSide * sqr3 / 2) + centerOfComb.x, 1.5f * tileSide * Mathf.Abs(i) + centerOfComb.y), Quaternion.identity, gameObject.transform);
                        HexTiles.Add(tile);
                        
                    }
                }
                else
                {
                    for (int j = (widtOfComb + i) / -2; j < (widtOfComb + i) / 2; j++)
                    {
                        GameObject tile = Instantiate(hexTile, new Vector2(j * sqr3 * tileSide + centerOfComb.x, 1.5f * tileSide * Mathf.Abs(i) + centerOfComb.y), Quaternion.identity, gameObject.transform);
                        HexTiles.Add(tile);
                    }
                }
            }
            else
            {
                if (i % 2 == 0)
                {
                    for (int j = (widtOfComb - i) / -2; j < (widtOfComb - i) / 2 + 1; j++)
                    {

                        GameObject tile = Instantiate(hexTile, new Vector2(j * sqr3 * tileSide - (tileSide * sqr3 / 2) + centerOfComb.x, 1.5f * tileSide * Mathf.Abs(i) * -1 + centerOfComb.y), Quaternion.identity, gameObject.transform);
                        HexTiles.Add(tile);
                    }
                }
                else
                {
                    for (int j = (widtOfComb - i) / -2; j < (widtOfComb - i) / 2; j++)
                    {
                        GameObject tile = Instantiate(hexTile, new Vector2(j * sqr3 * tileSide + centerOfComb.x, 1.5f * tileSide * Mathf.Abs(i) * -1 + centerOfComb.y), Quaternion.identity, gameObject.transform);
                        HexTiles.Add(tile);
                    }
                }
            }
        }

        //gameObject.transform.Rotate(new Vector3(90, 0, 0));
    }
}
