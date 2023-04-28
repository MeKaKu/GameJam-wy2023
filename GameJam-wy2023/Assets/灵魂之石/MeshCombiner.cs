using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    public GameObject[] objectsToCombine;

    void Start()
    {
        CombineMeshes();
    }

    void CombineMeshes()
    {
        // Create a new empty game object to hold the combined mesh
        GameObject combinedObject = new GameObject("CombinedMesh");

        // Initialize the mesh filter and renderer components for the combined object
        MeshFilter meshFilter = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = combinedObject.AddComponent<MeshRenderer>();

        // Initialize a new empty mesh for the combined object
        Mesh combinedMesh = new Mesh();

        // Initialize arrays to hold the individual meshes and their transforms
        Mesh[] meshes = new Mesh[objectsToCombine.Length];
        Matrix4x4[] matrices = new Matrix4x4[objectsToCombine.Length];

        // Loop through each object and add its mesh and transform to the arrays
        for (int i = 0; i < objectsToCombine.Length; i++)
        {
            meshes[i] = objectsToCombine[i].GetComponent<MeshFilter>().mesh;
            matrices[i] = objectsToCombine[i].transform.localToWorldMatrix;
        }

        CombineInstance[] combineInstances = new CombineInstance[meshes.Length];
        for(int i=0;i<meshes.Length;i++){
            combineInstances[i] = new CombineInstance(){
                mesh = meshes[i], transform = matrices[i]
            };
        }
        // Combine all meshes into the new combined mesh
        combinedMesh.CombineMeshes(combineInstances);

        // Set the combined mesh as the mesh for the combined object
        meshFilter.sharedMesh = combinedMesh;

        // Set the materials of the combined object to be the materials of the first object in the list
        meshRenderer.materials = objectsToCombine[0].GetComponent<MeshRenderer>().sharedMaterials;

        // Set the transform of the combined object to be the identity matrix
        combinedObject.transform.position = Vector3.zero;
        combinedObject.transform.rotation = Quaternion.identity;
        combinedObject.transform.localScale = Vector3.one;

        // Loop through each object in the list and deactivate it
        for (int i = 0; i < objectsToCombine.Length; i++)
        {
            objectsToCombine[i].SetActive(false);
        }
    }
}
