using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class Grid : MonoBehaviour {

    public enum Face{
        xy,
        zx,
        yz,
    };

    public float gridSize=1f;
    public Color color = Color.white;
    public Face face = Face.xy;
    public bool back = true;
    public int horizontalGrids = 1;
    public int verticalGrids = 1;

    Mesh mesh;

    void Start () {

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh = ReGrid(mesh);

    }

    Mesh ReGrid (Mesh mesh) {
        if (back) {
            GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        } else {
            GetComponent<MeshRenderer>().material = new Material(Shader.Find("GUI/Text Shader"));
        }

        mesh.Clear();

        float width;
        float height;
        int resolution;

        Vector3[] vertices;
        Vector2[] uvs;
        int[] lines;
        Color[] colors;

        width = gridSize * (float)horizontalGrids;
        height = gridSize * (float)verticalGrids;
        Vector2 startPosition = new Vector2 (-width/2, -height/2);
        Vector2 endPosition = new Vector2 (width/2, height/2);
        resolution = (horizontalGrids+verticalGrids+2)*2;
        //最後の２辺を追加している

        vertices = new Vector3[resolution];
        uvs = new Vector2[resolution];
        lines = new int[resolution];
        colors = new Color[resolution];

        for (int i = 0; i < horizontalGrids+1; i++) {
            vertices[2*i] = new Vector3(startPosition.x + (gridSize * (float)i), startPosition.y, 0);
            vertices[2*i+1] = new Vector3(startPosition.x + (gridSize * (float)i), endPosition.y, 0);
        }
        for (int i = 0; i < verticalGrids+1; i++) {
            vertices[2*(horizontalGrids+1) + 2*i] = new Vector3(startPosition.x, endPosition.y - (gridSize * (float)i), 0);
            vertices[2*(horizontalGrids+1) + 2*i+1] = new Vector3(endPosition.x, endPosition.y - (gridSize * (float)i), 0);
        }

        for (int i = 0; i < resolution; i++) {
            uvs [i] = Vector2.zero;
            lines [i] = i;
            colors [i] = color;
        }

        Vector3 rotDirection;
        switch (face) {
            case Face.xy:
                rotDirection = Vector3.forward;
                break;
            case Face.zx:
                rotDirection = Vector3.up;
                break;
            case Face.yz:
                rotDirection = Vector3.right;
                break;
            default:
                rotDirection = Vector3.forward;
                break;
        }

        mesh.vertices = RotateVertices(vertices,rotDirection);
        mesh.uv = uvs;
        mesh.colors = colors;
        mesh.SetIndices (lines, MeshTopology.Lines, 0);

        return mesh;
    }

    //頂点配列データーをすべて指定の方向へ回転移動させる
    Vector3[] RotateVertices(Vector3[] vertices,Vector3 rotDirection){
        Vector3[] ret= new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++) {
            ret[i] = Quaternion.LookRotation(rotDirection) * vertices[i];
        }
        return ret;
    }

}