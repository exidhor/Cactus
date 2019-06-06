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

        public static bool LineIntersectRect(out Vector2 intersection, Vector2 origin, Vector2 endPoint, Rect rect)
        {
            Vector2 botLeft = new Vector2(rect.xMin, rect.yMin);
            Vector2 topLeft = new Vector2(rect.xMin, rect.yMax);
            Vector2 topRight = new Vector2(rect.xMax, rect.yMax);
            Vector2 botRight = new Vector2(rect.xMax, rect.yMin);

            Segment line = new Segment(origin, endPoint);

            Segment left = new Segment(botLeft, topLeft);
            Segment top = new Segment(topLeft, topRight);
            Segment right = new Segment(topRight, botRight);
            Segment bot = new Segment(botRight, botLeft);

            bool isInsideRect = rect.Contains(origin);

            bool toTheRight = (origin.x < endPoint.x) ^ isInsideRect;
            bool toTheTop = (origin.y < endPoint.y) ^ isInsideRect;

            if(toTheRight)
            {
                if (IntersectSegments(out intersection, line, left))
                {
                    return true;
                }
            }
            else
            {
                if (IntersectSegments(out intersection, line, right))
                {
                    return true;
                }
            }

            if(toTheTop)
            {
                if (IntersectSegments(out intersection, line, bot))
                {
                    return true;
                }

            }
            else
            {
                if (IntersectSegments(out intersection, line, top))
                {
                    return true;
                }
            }

            intersection = Vector2.zero;
            return false;
        }

        public static float Cross2D(Vector2 v, Vector2 w)
        {
            return v.x * w.y - v.y * w.x;
        }

        // source : https://stackoverflow.com/questions/563198/how-do-you-detect-where-two-line-segments-intersect
        public static bool IntersectSegments(out Vector2 intersection, Segment first, Segment second)
        {
            Vector2 p = first.A;
            Vector2 r = first.delta;

            Vector2 q = second.A;
            Vector2 s = second.delta;

            Vector2 o = (q - p); // offset

            float RxS = Cross2D(r, s);
            float OxS = Cross2D(o, s);
            float OxR = Cross2D(o, r);

            if (RxS == 0f)
            {
                if (OxR == 0f)
                {
                    // two segments are colinear
                    float RdotR = Vector2.Dot(r, r);

                    // The crossing point of the second segment expressed in terms of the equation of the first segment
                    float t0 = Vector2.Dot(o, r) / RdotR;

                    // The pointLine point of the second segment expressed in terms of the equation of the first segment
                    float t1 = t0 + Vector2.Dot(s, r) / RdotR;

                    bool t0Intersects = (0 <= t0 && t0 <= 1);
                    bool t1Intersects = (0 <= t1 && t1 <= 1);

                    if (t0Intersects)
                    {
                        // they are overlapping
                        intersection = q;
                        return true;
                    }

                    if (t1Intersects)
                    {
                        // they are overlapping
                        intersection = s;
                        return true;
                    }
                    else
                    {
                        // they are disjoint
                        intersection = new Vector2();
                        return false;
                    }
                }
                else
                {
                    // line are parallel
                    intersection = new Vector2();
                    return false;
                }
            }

            float t = OxS / RxS;
            float u = OxR / RxS;

            if ((0 <= t && t <= 1) && (0 <= u && u <= 1))
            {
                intersection = p + t * r;
                return true;
            }
            else
            {
                // lines are not parallel but segments do not intersect
                intersection = new Vector2();
                return false;
            }
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
