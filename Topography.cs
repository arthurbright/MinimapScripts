using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Topography : MonoBehaviour
{
    public  int ringDepth;
    public TerrainData ter;
    
    ////////////////////////////////// Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.SetTexture("_BaseMap", Contour(ringDepth, ter));
    }

    public static Texture2D Contour(int depth, TerrainData ter)
    {
        ///////////////////////////////////////////////////////////get heights (new method, realtime)

        //set the colors and initiate final texture variable;
        Texture2D finalMap;
        Color white = new Color(0, 0, 0, 0.85f);
        Color transparent = new Color(0, 0, 0, 0);


        //set the height and length of final texture
        int height = ter.heightmapHeight;
        int width = ter.heightmapWidth;

        //set the raw image array of shorts
        short[] rawImage = new short[height * width];

        //get the terrain data realtime
        float[,] heights = ter.GetHeights(0, 0, ter.heightmapHeight, ter.heightmapWidth);

        float max = 0;
        //map the 2d array into the 1d array
        for (int y = 0; y < ter.heightmapWidth; y ++)
        {
            for(int x = 0; x < ter.heightmapHeight; x ++)
            {
                int index = ter.heightmapWidth * x + y;
                rawImage[index] = (short) (heights[x, y] * 32768);
                if(max < rawImage[index])
                {
                    max = rawImage[index];
                }
            }
        }
        

        //create boolean array for ease of coloring later
        bool[] colored = new bool[rawImage.Length];

        //set dimensions of final map
        finalMap = new Texture2D(width, height);
        finalMap.anisoLevel = 16;

        ///////Set the lines to be white whenever they are in a certain range
        int yDepth = 0;
        
        while (yDepth < max)
        {
        

            //only color in the edge of the next layer; if a pixel is touching a pixel in a different layer, color it
            for (int x = 1, k = 0; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++, k ++)
                {
                    if (rawImage[y * width + x] >= yDepth && rawImage[y * width + x] < yDepth + depth)
                    {
                        if (rawImage[y * width + x + 1] < yDepth || 
                        rawImage[y * width + x - 1] < yDepth || 
                        rawImage[(y + 1) * width + x] < yDepth || 
                        rawImage[(y - 1) * width + x] < yDepth ||
                        rawImage[(y - 1) * width + x - 1] < yDepth || 
                        rawImage[(y - 1) * width + x + 1] < yDepth ||
                        rawImage[(y + 1) * width + x - 1] < yDepth || 
                        rawImage[(y + 1) * width + x + 1] < yDepth)
                        {
                           finalMap.SetPixel(x, y, white);
                        }
                        else
                        {
                            finalMap.SetPixel(x, y, transparent);
                        }
                    }
                }
            }
                yDepth += depth;
        }
        //apply final texture, voila!
        finalMap.Apply();
        return finalMap;
    }
}
