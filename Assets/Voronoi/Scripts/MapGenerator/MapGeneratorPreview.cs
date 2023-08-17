using Delaunay;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

public partial class MapGeneratorPreview : MonoBehaviour
{
    [Header("Settings")]
    public PreviewType previewType;
    public HeightMapSettings heightMapSettings;
    public int seed = 0;
    public bool autoUpdate;

    [Header("Mesh Settings")]
    public int meshSize = 200;

    [Header("Texture Settings")]
    public int textureSize = 512;
    public bool drawNodeBoundries;
    public bool drawDelauneyTriangles;
    public bool drawNodeCenters;
    public List<MapNodeTypeColor> colours;

    [Header("Voronoi Generation")]
    public PointGeneration pointGeneration;
    public int pointSpacing = 10;
    public int relaxationIterations = 1;
    public float snapDistance = 0;

    [Header("Outputs")]
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;

    //
    [Header("By Kyuji")]
    //public Transform point;

    private List<Vector2> coloredVoronoiCells;
    private Voronoi voronoi;
    private MapGraph mapGraph;

    public void Start()
    {
        coloredVoronoiCells = new List<Vector2>();

        StartCoroutine(GenerateMapAsync());
    }

    public IEnumerator GenerateMapAsync()
    {
        yield return new WaitForSeconds(1f);
        GenerateMap();
    }

    public void GenerateMap()
    {

        var startTime = DateTime.Now;
        //var points = GetMyPoints();
        var points = GetPoints();
        //print("point numbers : " + points.Count);



        var time = DateTime.Now;
        //var voronoi = new Delaunay.Voronoi(points, null, new Rect(0, 0, meshSize, meshSize), relaxationIterations);
        voronoi = new Delaunay.Voronoi(points, null, new Rect(0, 0, meshSize, meshSize), relaxationIterations);
        //Debug.Log(string.Format("Voronoi Generated: {0:n0}ms", DateTime.Now.Subtract(time).TotalMilliseconds));



        time = DateTime.Now;
        heightMapSettings.noiseSettings.seed = seed;
        var heightMap = HeightMapGenerator.GenerateHeightMap(meshSize, meshSize, heightMapSettings, Vector2.zero);
        //Debug.Log(string.Format("Heightmap Generated: {0:n0}ms", DateTime.Now.Subtract(time).TotalMilliseconds));

        time = DateTime.Now;
        //var mapGraph = new MapGraph(voronoi, heightMap, snapDistance);
        mapGraph = new MapGraph(voronoi, heightMap, snapDistance);
        //Debug.Log(string.Format("Finished Generating Map Graph: {0:n0}ms with {1} nodes", DateTime.Now.Subtract(startTime).TotalMilliseconds, mapGraph.nodesByCenterPosition.Count));

        time = DateTime.Now;
        MapGenerator.GenerateMap(mapGraph);
        //Debug.Log(string.Format("Map Generated: {0:n0}ms", DateTime.Now.Subtract(time).TotalMilliseconds));

        if (previewType == PreviewType.HeightMap)
        {
            OnMeshDataReceived(MapMeshGenerator.GenerateMesh(mapGraph, heightMap, meshSize));
            UpdateTexture(TextureGenerator.TextureFromHeightMap(heightMap));
        }
        if (previewType == PreviewType.Map)
        {
            time = DateTime.Now;
            OnMeshDataReceived(MapMeshGenerator.GenerateMesh(mapGraph, heightMap, meshSize));
            //Debug.Log(string.Format("Mesh Generated: {0:n0}ms", DateTime.Now.Subtract(time).TotalMilliseconds));

            time = DateTime.Now;
            var texture = MapTextureGenerator.GenerateTexture(mapGraph, meshSize, textureSize, colours, drawNodeBoundries, drawDelauneyTriangles, drawNodeCenters);
            //Debug.Log(string.Format("Texture Generated: {0:n0}ms", DateTime.Now.Subtract(time).TotalMilliseconds));

            UpdateTexture(texture);
        }

        //Debug.Log(string.Format("Finished Generating World: {0:n0}ms with {1} nodes", DateTime.Now.Subtract(startTime).TotalMilliseconds, mapGraph.nodesByCenterPosition.Count));

        // Modified by Kyuji
        //Vector2 centerPoint = voronoi.NearestSitePoint(point.position.x, point.position.z).Value;
        ////print(centerPoint);
        //var t = MapTextureGenerator.GenerateTexture(mapGraph, meshSize, textureSize, colours, drawNodeBoundries, drawDelauneyTriangles, drawNodeCenters, centerPoint);
        //UpdateTexture(t);
    }

    // Modified by Kyuji
    public void ColoringVoronoiCell(Vector3 point, int playerNumber)
    {
        MapGraph.MapNodeType wonPlayer = (playerNumber == 1) ? MapGraph.MapNodeType.Player1 : MapGraph.MapNodeType.Player2;

        Vector2 centerPoint = voronoi.NearestSitePoint(point.x, point.z).Value;
        coloredVoronoiCells.Add(centerPoint);
        //var t = MapTextureGenerator.GenerateTexture(mapGraph, meshSize, textureSize, colours, drawNodeBoundries, drawDelauneyTriangles, drawNodeCenters, coloredVoronoiCells);
        var t = MapTextureGenerator.GenerateTexture(mapGraph, meshSize, textureSize, colours, drawNodeBoundries, drawDelauneyTriangles, drawNodeCenters, centerPoint, wonPlayer);

        UpdateTexture(t);
    }

    private List<Vector2> GetMyPoints()
    {
        List<Vector2> points = new List<Vector2>();

        //for(int i=0; i<5; i++)
        //{
        //    points.Add(new Vector2(UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100)));
        //}

        points.Add(new Vector2(50f, 50f));
        points.Add(new Vector2(5f, 50f));
        points.Add(new Vector2(7f, 4f));
        points.Add(new Vector2(46f, 25f));
        points.Add(new Vector2(12f, 87f));

        return points;
    }

    private List<Vector2> GetPoints()
    {
        List<Vector2> points = null;
        if (pointGeneration == PointGeneration.Random)
        {
            points = VoronoiGenerator.GetVector2Points(seed, (meshSize / pointSpacing) * (meshSize / pointSpacing), meshSize);
        }
        else if (pointGeneration == PointGeneration.PoissonDisc)
        {
            var poisson = new PoissonDiscSampler(meshSize, meshSize, pointSpacing, seed);
            points = poisson.Samples().ToList();
        }
        else if (pointGeneration == PointGeneration.Grid)
        {
            points = new List<Vector2>();
            for (int x = pointSpacing; x < meshSize; x += pointSpacing)
            {
                for (int y = pointSpacing; y < meshSize; y += pointSpacing)
                {
                    points.Add(new Vector2(x, y));
                }
            }
        }
        else if (pointGeneration == PointGeneration.OffsetGrid)
        {
            points = new List<Vector2>();
            for (int x = pointSpacing; x < meshSize; x += pointSpacing)
            {
                bool even = false;
                for (int y = pointSpacing; y < meshSize; y += pointSpacing)
                {
                    var newX = even ? x : x - (pointSpacing / 2f);
                    points.Add(new Vector2(newX, y));
                    even = !even;
                }
            }
        }

        return points;
    }

    private void OnTextureDataReceived(object result)
    {
        var textureData = result as TextureData;
        UpdateTexture(textureData);
    }

    private void OnMeshDataReceived(object result)
    {
        var meshData = result as MeshData;
        UpdateMesh(meshData);
    }

    private void UpdateTexture(TextureData data)
    {
        var texture = new Texture2D(textureSize, textureSize);
        texture.SetPixels(data.colours);
        texture.Apply();
        UpdateTexture(texture);
    }

    private void UpdateTexture(Texture2D texture)
    {
        meshRenderer.sharedMaterial.mainTexture = texture;
    }

    public void UpdateMesh(MeshData meshData)
    {
        var mesh = new Mesh
        {
            vertices = meshData.vertices.ToArray(),
            triangles = meshData.indices.ToArray(),
            uv = meshData.uvs
        };
        mesh.RecalculateNormals();
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    void OnValidate()
    {
        if (heightMapSettings != null)
        {
            heightMapSettings.OnValuesUpdated -= OnValuesUpdated;
            heightMapSettings.OnValuesUpdated += OnValuesUpdated;
        }

    }

    private void OnValuesUpdated()
    {
        GenerateMap();
    }
}