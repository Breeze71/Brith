using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : IComparable<Edge>
{
    public int Source { get; set; }
    public int Destination { get; set; }
    public float Weight { get; set; }

    public int CompareTo(Edge other)
    {
        return (this.Weight - other.Weight)>=0?1:-1;//
    }
}
public class UnionFind
{
    private int[] parent;

    public UnionFind(int size)
    {
        parent = new int[size];
        for (int i = 0; i < size; i++)
            parent[i] = i;
    }

    public int Find(int i)
    {
        if (parent[i] != i)
            parent[i] = Find(parent[i]);
        return parent[i];
    }

    public void Union(int x, int y)
    {
        int xRoot = Find(x);
        int yRoot = Find(y);
        if (xRoot != yRoot)
            parent[xRoot] = yRoot;
    }
}
public class Graph
{
    public List<Edge> Edges { get; set; }
    public int Vertices { get; set; }

    public Graph(int vertices)
    {
        Vertices = vertices;
        Edges = new List<Edge>();
    }

    public void AddEdge(int source, int destination, float weight)
    {
        Edges.Add(new Edge { Source = source, Destination = destination, Weight = weight });
    }

    public List<Edge> KruskalMST()
    {
        List<Edge> result = new List<Edge>();
        UnionFind uf = new UnionFind(Vertices);

        Edges.Sort();

        foreach (var edge in Edges)
        {
            int x = uf.Find(edge.Source);
            int y = uf.Find(edge.Destination);

            if (x != y)
            {
                result.Add(edge);
                uf.Union(x, y);
            }
        }

        return result;
    }
}

