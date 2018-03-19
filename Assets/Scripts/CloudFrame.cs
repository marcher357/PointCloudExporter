using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CloudFrame
{
    [SerializeField]
    private float t;
    [SerializeField]
    private float[] p;//public SimpleVector[] p;
    [SerializeField]
    public int height;
    [SerializeField]
    public int width;
    [SerializeField]
    public float psize;


    /// <summary>
    /// Receives a vector3 array, a color array and a timestamp
    /// Converts this into a serializable cloudframe and initializes the object
    /// </summary>
    public CloudFrame(Vector3[] somepc, Color32[] somecc, float timestamp, int height, int width)
    {
        this.height = height;
        this.width = width;
        p = new float[somepc.Length];//new SimpleVector[somepc.Length];
        t = timestamp;

        for (int i = 0; i < somepc.Length; i++)
            p[i] = somepc[i].z;//new SimpleVector(somepc[i], somecc[i]);
    }

    public int getVertexCount()
    {
        return p.Length;
    }

    public void reScale(float pA, float pB)
    {
        List<float> rescaledPs = new List<float>();

        for (int i = 0; i < p.Length; i++)
        {
            if (pA <= p[i] && pB >= p[i])
                rescaledPs.Add(p[i]);
        }

        float[] Ps = rescaledPs.ToArray();
        Ps = normalize(Ps);

        
    }

    private float[] normalize(float[] Ps)
    {
        float max = 0f;
        foreach (float point in Ps)
        {
            if (point > max)
                max = point;
        }

        for(int i = 0; i<Ps.Length; i++)
            Ps[i] = Ps[i] / max;

        return Ps;
    }

    /// <summary>
    /// Returns a unity vector3 array associated with simplevector array stored
    /// with the object.
    /// </summary>1
    public Vector3[] GetpointCloud(float scale)
    {
        Vector3[] pointCloud = new Vector3[p.Length];


            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int i = x + y * width;
                    float xval = (float)x * scale;
                    float yval = (float)y * scale;
                    Vector3 test = new Vector3(xval, yval, p[i]);
                    if (test.x != 0 && test.y != 0)
                    {
                        //   Debug.Log("Yay!");
                    }
                    pointCloud[i] = test;
                    //p[i].toVector3();
                }
            }

        return pointCloud;
    }

    /// <summary>
    /// Returns a unity color array associated with the color array associated 
    /// with the simpleVector stored in the object. 
    /// </summary>
    public Color[] GetColorCloud()
    {
        Color[] colorCloud = new Color[p.Length];

        for (int i = 0; i < p.Length; i++)
        {
            Color aColor = new Color(p[i], p[i], p[i]);
            colorCloud[i] = aColor;
        }

        return colorCloud;

    }
}
