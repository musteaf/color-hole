using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.BoardNS
{
    public static class BoardMath
    {
        public static Limits CalculateLimits(List<GameObject> objs)
        {
            var limits = new Limits
            {
                maxX = objs[0].transform.position.x,
                minX = objs[0].transform.position.x,
                maxZ = objs[0].transform.position.z,
                minZ = objs[0].transform.position.z
            };
            for (var i = 1; i < objs.Count; i++)
            {
                limits.minX = Math.Min(limits.minX, objs[i].transform.position.x);
                limits.maxX = Math.Max(limits.maxX, objs[i].transform.position.x);
                limits.minZ = Math.Min(limits.minZ, objs[i].transform.position.z);
                limits.maxZ = Math.Max(limits.maxZ, objs[i].transform.position.z);
            }
            //should calculate hole size dynamically :(
            limits.minX += 2f;
            limits.maxX -= 2f;
            limits.maxZ -= 2f;
            limits.minZ += 2f;
            return limits;
        }
        
        public static Vector3 FindMiddlePoint(List<GameObject> objs)
        {
            var limits = CalculateLimits(objs);
            return new Vector3(limits.minX + ((limits.maxX-limits.minX) / 2f), 0f, limits.minZ + ((limits.maxZ-limits.minZ) / 2f));
        }
    }
}