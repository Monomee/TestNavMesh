using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;

public class RandomPointNavMesh : MonoBehaviour
{
    const float RANGE1 =4;
    const float RANGE2 = 10f;
    const float RANGE3 = 30f;
    const float RANGE4 = 100f;

    static readonly float[] rangeList = new float[] { RANGE1, RANGE2, RANGE3, RANGE4 };
    public NavMeshAgent agent;

    public static RandomPointNavMesh instance;
    private void OnEnable()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }
    public float range = 15000.0f;
    public GameObject centerGO;


    public static Vector3 RandomPointInArea(Vector3 pos)
    {
        Vector2 random = Random.insideUnitCircle * Random.Range(0, RANGE3);
        Vector3 randomVector3 = new Vector3(random.x, 0, random.y);

        return pos + randomVector3;
    }
    public bool RandomPoint(Vector3 center, out Vector3 result)
    {
        for (int i = 0; i < rangeList.Length; i++)
        {
            //Vector3 randomPoint = center + Random.insideUnitSphere * range;
            Vector3 randomPoint = center;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, rangeList[i], agent.areaMask))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
