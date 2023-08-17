using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Delaunay;

public class VoronoiTest : MonoBehaviour
{
    public int relaxationIterations = 0;
    public int meshSize = 30;

    public int snapDistance = 1;

    Voronoi voronoi;

    private 

    void Start()
    {
        //GenerateVoronoi();
        MyFunc();
    }

    void Update()
    {
        
    }
    
    public void MyFunc()
    {
        print(voronoi.NearestSitePoint(51, 51));
    }

    public void GenerateVoronoi()
    {
        var points = GetPoints();

        voronoi = new Delaunay.Voronoi(points, null, new Rect(0, 0, meshSize, meshSize), relaxationIterations);

        //var mapGraph = new MapGraph(voronoi, heightMap, snapDistance);
        //MapGraph.create
    }

    private List<Vector2> GetPoints()
    {
        List<Vector2> points = new List<Vector2>();

        //for(int i=0; i<5; i++)
        //{
        //    points.Add(new Vector2(Random.Range(0, 100), Random.Range(0, 100)));
        //}

        points.Add(new Vector2(50f, 50f));
        points.Add(new Vector2(5f, 50f));
        points.Add(new Vector2(7f, 4f));
        points.Add(new Vector2(46f, 25f));
        points.Add(new Vector2(12f, 87f));

        return points;
    }
}
