using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class ProceduralTetrahedron : MonoBehaviour
{
    public float size = 1.0f;
    public Vector3 offset = new Vector3(0, 0, 0);

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    void OnValidate()
    {
        if (mesh == null) return;

        if (size > 0 || offset.magnitude > 0)
        {
            setMeshData(size);
            createProceduralMesh();
        }
    }

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        transform.position += Vector3.up * 0.25f;
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        //

        setMeshData(size);
        createProceduralMesh();
    }

    void setMeshData(float size)
    {
        float g = Mathf.Sqrt(3.0f) / 6.0f * size; // ���ﰢ���� ����
        float h = Mathf.Sqrt(6.0f) / 3.0f * size; // �����ü�� ����
        float c = Mathf.Sqrt(6.0f) / 12.0f * size; // �����ü�� ����

        Vector3 d0 = new Vector3(0, h - c, 0) + offset;
        Vector3 d1 = new Vector3(-0.5f * size, -c, -g) + offset;
        Vector3 d2 = new Vector3(0, -c, Mathf.Sqrt(3.0f) / 2.0f * size - g) + offset;
        Vector3 d3 = new Vector3(0.5f * size, -c, -g) + offset;

        vertices = new Vector3[] { d0, d1, d2, d0, d2, d3, d0, d3, d1, d1, d3, d2 };

        triangles = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
    }

    void createProceduralMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        // �޽� �ݶ��̴��� �ڽĿ��� �θ� �浹 üũ�� ����� �ȵǰ� �θ𿡰� �θ� Mesh renderer�� ���� ������Ʈ�� ���־ �� ����� �ȵȴ�
        // �׳� �޽� �ݶ��̴� �Ⱦ��� ����Ѱ� �������� �θ𿡴� ����� ������ �ӽù��� ó���ߴ�

        //Destroy(this.GetComponent<MeshCollider>());
        //this.transform.parent.gameObject.AddComponent<MeshCollider>();
        //gameObject.AddComponent<MeshCollider>();

        //MeshCollider collider = this.transform.parent.gameObject.GetComponent<MeshCollider>();
        //MeshCollider collider = gameObject.GetComponent<MeshCollider>();
        //collider.convex = true;
        //collider.isTrigger = true;
    }
}