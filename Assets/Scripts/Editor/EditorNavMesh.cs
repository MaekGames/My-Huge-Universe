using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine.AI;
#endif
public class EditorNavMesh
{

    //----- HERE ARE YOUR BAKED NAVMESH CHUNKS
    [SerializeField]
    private List<NavMeshChunk> _navMeshChunks;

    [SerializeField]
    private List<NavMeshDataInstance> _instances = new List<NavMeshDataInstance>();

    [SerializeField]
    private Transform _pivot;
#if UNITY_EDITOR
    [MenuItem("GameObject/LoadCustomMesh")]
    static void LoadCustomMesh()
    {
        //LoadNavmeshData();

    }
    public void LoadNavmeshData()
    {
        foreach (var chunk in _navMeshChunks)
        {
            if (chunk.Enabled)
            {
                _instances.Add(
                    UnityEngine.AI.NavMesh.AddNavMeshData(
                        chunk.Data,
                        _pivot.transform.position,
                        Quaternion.Euler(chunk.EulerRotation)));
            }
        }
    }
#endif

}
