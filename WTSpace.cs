using robotManager.Helpful;
using System.Collections.Generic;
using System.Linq;

namespace Wholesome_Toolbox
{
    /// <summary>
    /// Space and vector related methods
    /// </summary>
    public class WTSpace
    {
        /// <summary>
        /// Mostly used for Aoe ground spells.
        /// Returns the center of aggregated positions.
        /// radius is the max radius, ex 15 for Circle of Healing.
        /// minCount is the minimum wanted amount of positions aggregated together.
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="radius"></param>
        /// <param name="minCount"></param>
        /// <param name="depth"></param>
        /// <returns>The aggregated center or null if it wasn't found</returns>
        public static Vector3 FindAggregatedCenter(List<Vector3> positions, int radius, int minCount, int depth = 5)
        {
            Vector3 centerVector = GetCenterVector(positions);
            for (int i = 0; i < depth; i++)
            {
                if (i >= depth - 1 || positions.Count < minCount)
                {
                    return null;
                }

                List<Vector3> positionsInRadius = positions
                    .FindAll(pos => pos.DistanceTo(centerVector) < radius)
                    .OrderBy(pos => pos.DistanceTo(centerVector))
                    .ToList();

                if (positionsInRadius.Count < minCount)
                {
                    positions.RemoveAt(positions.Count - 1);
                    centerVector = GetCenterVector(positions);
                }
                else
                {
                    return GetCenterVector(positionsInRadius);
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the center of several vectors
        /// </summary>
        /// <param name="positions"></param>
        /// <returns>Center vector</returns>
        public static Vector3 GetCenterVector(List<Vector3> positions)
        {
            Vector3 centerVector = new Vector3(0, 0, 0);
            foreach (Vector3 position in positions)
            {
                centerVector += position;
            }
            return centerVector /= positions.Count;
        }
    }
}
