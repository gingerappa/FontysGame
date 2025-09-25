using System.Collections;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WorldCreation : MonoBehaviour
{
    public Vector2 worldSize;
    public Tile[] tiles;

    private Vector2[] world;

    public void CreateWorld()
    {
        int2 tileSize = new(0,0);
        for (int i = 0; i < worldSize.y; i += tileSize.y)
        {
            
        }
    }

    //private IEnumerator StartCreateWorld()
    //{
        
    //}

    //private IEnumerator CreateTile(Vector2 position)
    //{

    //}
}
