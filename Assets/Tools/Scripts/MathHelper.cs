using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public static class MathHelper
    {
        public static Rect ConstructRect(Vector2 p0, Vector2 p1)
        {
            float minX, maxX;

            if (p0.x < p1.x)
            {
                minX = p0.x;
                maxX = p1.x;
            }
            else
            {
                minX = p1.x;
                maxX = p0.x;
            }

            float minY, maxY;

            if (p0.y < p1.y)
            {
                minY = p0.y;
                maxY = p1.y;
            }
            else
            {
                minY = p1.y;
                maxY = p0.y;
            }

            Vector2 min = new Vector2(minX, minY);
            Vector2 max = new Vector2(maxX, maxY);

            return new Rect(min, max - min);
        }

        public static bool LineIntersectRect(out Vector2 intersection, Vector2 p0, Vector2 p1, Rect rect)
        {
            Vector2 botLeft = new Vector2(rect.xMin, rect.yMin);
            Vector2 topLeft = new Vector2(rect.xMin, rect.yMax);
            Vector2 topRight = new Vector2(rect.xMax, rect.yMax);
            Vector2 botRight = new Vector2(rect.xMax, rect.yMin);

            // left
            if(IntersectLines(out intersection, p0, p1, botLeft, topLeft))
            {
                return true;
            }
            // top
            else if(IntersectLines(out intersection, p0, p1, topLeft, topRight))
            {
                return true;
            }
            // right
            else if(IntersectLines(out intersection, p0, p1, topRight, botRight))
            {
                return true;
            }
            // bot
            else if(IntersectLines(out intersection, p0, p1, botRight, botLeft))
            {
                return true;
            }

            intersection = Vector2.zero;
            return false;
        }

        // source : https://stackoverflow.com/questions/4543506/algorithm-for-intersection-of-2-lines
        public static bool IntersectLines(out Vector2 intersection, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            // we want equations in form of : Ax + By = C
            float a0 = p1.y - p0.y;
            float b0 = p1.x - p0.x;
            float c0 = a0 * p0.x + b0 * p0.y;

            float a1 = p3.y - p2.y;
            float b1 = p3.x - p2.x;
            float c1 = a1 * p1.x + b1 * p1.y;

            float det = a0 * b1 - a1 * b0;

            if (-float.Epsilon < det && det < float.Epsilon)
            {
                // Line are parallel
                intersection = new Vector2();
                return false;
            }

            float x = (b1 * c0 - b0 * c1) / det;
            float y = (a0 * c1 - a1 * c0) / det;

            intersection = new Vector2(x, y);

            return true;
        }
    }
}
