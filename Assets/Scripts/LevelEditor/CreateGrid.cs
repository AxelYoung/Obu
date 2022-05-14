using UnityEngine;
using System.Collections.Generic;

public class CreateGrid : MonoBehaviour {

    public int gridSize;

    void Awake() {
        CreateMesh();
        CenterObject();
    }

    void CreateMesh() {
        MeshFilter filter = gameObject.GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        List<Vector3> verticies = new List<Vector3>();

        List<int> indicies = new List<int>();
        for (int i = 0; i <= gridSize; i++) {
            verticies.Add(new Vector3(i, 0, 0));
            verticies.Add(new Vector3(i, gridSize, 0));

            indicies.Add(4 * i + 0);
            indicies.Add(4 * i + 1);

            verticies.Add(new Vector3(0, i, 0));
            verticies.Add(new Vector3(gridSize, i, 0));

            indicies.Add(4 * i + 2);
            indicies.Add(4 * i + 3);
        }

        mesh.vertices = verticies.ToArray();
        mesh.SetIndices(indicies.ToArray(), MeshTopology.Lines, 0);
        filter.mesh = mesh;
    }

    void CenterObject() {
        float center = -(gridSize / 2f);
        transform.position = new Vector3(center, center, 1);
    }
}