using UnityEngine;

public class TerrainChecker : MonoBehaviour
{
    private float[] GetTextureMix(Vector3 playerPos, Terrain t)
    {
        Vector3 tPosition = t.transform.position;
        TerrainData tData = t.terrainData;

        int mapX = Mathf.RoundToInt((playerPos.x - tPosition.x) / tData.size.x * tData.alphamapWidth);
        int mapZ = Mathf.RoundToInt((playerPos.z - tPosition.z) / tData.size.z * tData.alphamapHeight);
        float[,,] splatMapData = tData.GetAlphamaps(mapX, mapZ, 1, 1);

        float[] cellmix = new float[splatMapData.GetUpperBound(2) + 1];
        for (int i = 0; i < cellmix.Length; i++)
        {
            cellmix[i] = splatMapData[0, 0, i];
        }
        return cellmix;
    }

    public string GetLayerName(Vector3 playerPosition, Terrain terrain)
    {
        float[] cellMix = GetTextureMix(playerPosition, terrain);
        float strongest = 0;
        int maxIndex = 0;

        for (int i = 0; i < cellMix.Length; i++)
        {
            if (cellMix[i] > strongest)
            {
                maxIndex = i;
                strongest = cellMix[i];
            }
        }
        
        return terrain.terrainData.terrainLayers[maxIndex].name;
    }
}
