﻿using System.Runtime.CompilerServices;
using UnityEngine;

namespace Common.Mathematics
{
    public static class Hexagons
    {
        public enum Direction
        {
            NE, E, SE, SW, W, NW, Count
        }

        public const int VERTEX_COUNT = 6;

        public const float INNER_TO_OUTER_RADIUS = 2.0f / Mathx.ROOT_3;
        public const float OUTER_TO_INNER_RADIUS = Mathx.ROOT_3 / 2.0f;

        private const float INNER_RADIUS = 0.5f;
        private const float OUTER_RADIUS = INNER_RADIUS * INNER_TO_OUTER_RADIUS;

        public static readonly Vector2[] Vertices = new Vector2[]
        {
            new Vector2(0.0f, OUTER_RADIUS),
            new Vector2(INNER_RADIUS, OUTER_RADIUS * 0.5f),
            new Vector2(INNER_RADIUS, -OUTER_RADIUS * 0.5f),
            new Vector2(0.0f, -OUTER_RADIUS),
            new Vector2(-INNER_RADIUS, -OUTER_RADIUS * 0.5f),
            new Vector2(-INNER_RADIUS, OUTER_RADIUS * 0.5f),
        };

        /// <summary> Triangles of a hexagon </summary>
        public static readonly int[] Triangles = new int[]
        {
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            0, 4, 5,
            -1
        };

        public static readonly Vector2Int[] Translations = new Vector2Int[]
        {
            new Vector2Int() { x =  0, y =  1 },
            new Vector2Int() { x =  1, y =  0 },
            new Vector2Int() { x =  1, y = -1 },
            new Vector2Int() { x =  0, y = -1 },
            new Vector2Int() { x = -1, y =  0 },
            new Vector2Int() { x = -1, y =  1 }
        };

        /// <summary> Calculates vertices of a hexagon defined by centre 'c' and circumradius 'r' into 'target' array </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetVertices(Vector2[] target, Vector2 c, float r)
        {
            for (int i = 0; i < target.Length; ++i)
                target[i] = c + Vertices[i] * r;
        }

        /// <summary> Calculates vertices of a hexagon defined by centre 'c' and circumradius 'r' </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2[] GetVertices(Vector2 c, float r)
        {
            var vs = new Vector2[VERTEX_COUNT];
            GetVertices(vs, c, r);
            return vs;
        }
        
        /// <summary> Converts position defined in hex coordinates 'v' of a hexagon defined by circumradius 'r' to world position </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Convert(Vector2Int v, float r)
        {
            float x = (v.x + v.y * 0.5f) * r * 2.0f * OUTER_TO_INNER_RADIUS;
            float y = v.y * r * 1.5f;
            return new Vector2(x, y);
        }

        /// <summary> Converts position defined as world position 'v' of a hexagon defined by circumradius 'r' to hex coordinates </summary>
        public static Vector2Int Convert(Vector2 v, float r)
        {
            float x = v.x / (r * OUTER_TO_INNER_RADIUS * 2.0f);
            float y = -x;

            float offset = v.y / (r * 3.0f);
            x -= offset;
            y -= offset;

            int ix = Mathf.RoundToInt(x);
            int iy = Mathf.RoundToInt(y);
            int iz = Mathf.RoundToInt(-x - y);

            if (ix + iy + iz != 0)
            {
                float dx = Mathf.Abs(x - ix);
                float dy = Mathf.Abs(y - iy);
                float dz = Mathf.Abs(-x - y - iz);

                if (dx > dy && dx > dz)
                    ix = -iy - iz;
                else if (dz > dy)
                    iz = -ix - iy;
            }

            return new Vector2Int(ix, iz);
        }
    }
}
