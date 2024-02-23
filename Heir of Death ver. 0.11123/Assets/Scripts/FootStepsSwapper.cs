using UnityEngine;

public class FootStepsSwapper : MonoBehaviour
{
    private TerrainChecker checker;
    private PlayerMovement pMovement;
    public FootStepCollection[] terrainFootstepCollection;

    private string currentLayer;

    void Start()
    {
        checker = new TerrainChecker();
        pMovement = GetComponent<PlayerMovement>();
    }

    public void CheckLayer()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position,Vector3.down, out hit,3))
        {
            if (hit.transform.GetComponent<Terrain>() != null)
            {
                Terrain t = hit.transform.GetComponent<Terrain>();

                if(currentLayer!= checker.GetLayerName(transform.position, t))
                {
                    currentLayer = checker.GetLayerName(transform.position, t);
                    foreach(FootStepCollection collection in terrainFootstepCollection)
                    {
                        if (currentLayer == collection.name)
                        {
                            pMovement.SwapFootsteps(collection);
                        }
                    }
                }
            }
            if(hit.transform.GetComponent<SurfaceType>() != null)
            {
                FootStepCollection collection = hit.transform.GetComponent<SurfaceType>().footStepCollection;
                currentLayer = collection.name;
                pMovement.SwapFootsteps(collection);
            }
        }
    }
}
