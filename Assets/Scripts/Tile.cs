using NUnit.Framework;
using System.Collections.Generic;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using UnityEngine;
using System.Collections;
using System.Linq;


public enum TileType
{
    MUD
}
public class Tile : MonoBehaviour
{
    public TileType type;
    public MeshFilter mf;
    public PolygonCollider2D polygonCollider;
    public int2 size;
    public int2 squareSize;
    [UnityEngine.Range(2, 10)]
    public int shapeSize;

    private bool?[] shape;
    private MeshData meshData;
    private int coroutines;
    private int tiles;

    public void create(Vector2 position)
    {
        Instantiate(this, position, quaternion.identity);
    }

    private void Awake()
    {
        StartCoroutine(CreateShape());
    }

    private IEnumerator CreateShape()
    {
        meshData = new(size.x, size.y);
        shape = new bool?[(size.x - 1) * (size.y - 1)];

        float topLeftX = (size.x-1) / -2f * squareSize.x;
        float topLeftY = (size.y-1) / 2f * squareSize.y;

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                meshData.vertices[i + (j * size.x)] = new(topLeftX + (i * squareSize.x), topLeftY - (j * squareSize.y), 0);
            }
        }

        coroutines = 0;
        tiles = 0;
        StartCoroutine(CreateTile(new((size.x - 1) / 2, (size.y - 1) / 2)));
        yield return new WaitUntil(() => coroutines == 0);


        string d = "Shape: \n";
        for (int i = 0; i < size.y-1; i++)
        {
            for (int j = 0; j < size.x-1; j++)
            {
                if(shape[i + (j * (size.x - 1))] == null)
                {
                    d += " ";
                }
                else
                {
                    d += shape[i + (j * (size.x - 1))] == true ? "X" : "O";
                }
            }
            d += "\n";
        }
        Debug.Log(d);
        mf.sharedMesh = meshData.createMesh();
    }

    private IEnumerator CreateTile(int2 position)
    {
        coroutines++;
        if (position.x < 0 || position.y < 0) { coroutines--; yield break; }
        if (position.x >= size.x - 1 || position.y >= size.y - 1) { coroutines--; yield break; }

        int index = position.x + (position.y * (size.y-1));
        if (shape[index] != null) { coroutines--; yield break; }
        if (Random.Range(0, shapeSize) == 0 && tiles > shapeSize)
        {
            shape[index] = false;
            coroutines--;
            yield break;
        }
        tiles++;
        meshData.AddSquare(position.x + (position.y * size.y));
        AddSquareToCollider(position.x + (position.y * size.y));
        shape[index] = true;

        yield return null;
        StartCoroutine(CreateTile(new(position.x, position.y+1)));
        StartCoroutine(CreateTile(new(position.x, position.y-1)));
        StartCoroutine(CreateTile(new(position.x+1, position.y)));
        StartCoroutine(CreateTile(new(position.x-1, position.y)));
        coroutines--;
    }

    private void AddSquareToCollider(int startPosition)
    {
        polygonCollider.pathCount = tiles;
        Vector2[] square = new Vector2[4] { meshData.vertices[startPosition], meshData.vertices[startPosition + 1], meshData.vertices[startPosition + 1 + size.x], meshData.vertices[startPosition + size.x] };
        polygonCollider.SetPath(tiles-1, square);
    }

    
}



public class MeshData
{
    public Vector3[] vertices { get; private set; }
    List<int> triangles = new();
    List<Vector2> edges = new();
    int width;

    public MeshData(int width, int height)
    {
        vertices = new Vector3[width * height];
        this.width = width;
    }

    public void PrintTriangles()
    {
        string d = "[";
        foreach (int triangle in triangles)
        {
            d += $"{triangle}, ";
        }
        d += "]";
        Debug.Log(d);
    }

    public void PrintVertices()
    {
        string d = "[";
        foreach (Vector3 vertice in vertices)
        {
            d += $"({vertice.x}, {vertice.y}, {vertice.z}), ";
        }
        d += "]";
        Debug.Log(d);
    }


    public void AddSquare(int startPosition)
    {
        triangles.Add(startPosition);
        triangles.Add(startPosition + width + 1);
        triangles.Add(startPosition + width);

        triangles.Add(startPosition + width + 1);
        triangles.Add(startPosition);
        triangles.Add(startPosition + 1);

    }

    public void RemoveSquare(int startPosition)
    {
        triangles.Remove(startPosition);
        triangles.Remove(startPosition + width + 1);
        triangles.Remove(startPosition + width);

        triangles.Remove(startPosition + width + 1);
        triangles.Remove(startPosition);
        triangles.Remove(startPosition + 1);
    }

    public Mesh createMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        return mesh;
    }
}


//int width = maxWidth - 1;
//List<int2>[] Shape = new List<int2>[maxHeight - 1];
//for (int i = 0; i < maxHeight - 1; i++)
//{
//    Shape[i] = new List<int2>();

//    int startPosition = Random.Range(0, (int)(maxWidth / 2.5f));
//    int endPosition = Random.Range((int)(maxWidth / 1.5f), width);

//    string d = $"Height: {i}, Start/End: {startPosition}/{endPosition}\n";
//    for (int j = startPosition; j < endPosition; j++)
//    {
//        d += $"Height: {i}; width: {j}; SqaurePlaced: {j + (i * maxWidth)}; Start/End: {startPosition}/{endPosition}\n";
//        Shape[i].Add(new(j, i));
//    }
//    Debug.Log(d);
//}

//List<int2> previouseRemoved = new();

//for (int i = 0; i < maxHeight - 2; i++)
//{
//    List<int2> tempRemoved = previouseRemoved.ToList();
//    previouseRemoved = new();
//    for (int j = 0; j < Shape[i].Count; j++)
//    {
//        if (Random.Range(0, 2) == 0 && tempRemoved.Where(x => x.x == Shape[i][j].x).Count() != 0 || i == 0)
//        {
//            previouseRemoved.Add(Shape[i][j]);
//            Shape[i].RemoveAt(j);
//        }
//    }
//}

//for (int i = 0; i < Shape.Length; i++)
//{
//    foreach (var position in Shape[i])
//    {
//        meshData.AddSquare(position.x + (position.y * maxWidth));
//    }
//}

//meshData.PrintTriangles();
//meshData.PrintVertices();

//return meshData.createMesh();
