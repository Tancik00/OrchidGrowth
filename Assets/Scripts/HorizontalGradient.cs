using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/HorizontalGradient")]
public class HorizontalGradient : BaseMeshEffect
{
    [SerializeField]
    private Color32 topColor = Color.white;
    [SerializeField]
    private Color32 bottomColor = Color.black;
    public override void ModifyMesh(VertexHelper vh)
    {
        if (!this.IsActive())
            return;
        List<UIVertex> vertexList = new List<UIVertex>();
        vh.GetUIVertexStream(vertexList);
        ModifyVertices(vertexList);
        vh.Clear();
        vh.AddUIVertexTriangleStream(vertexList);
    }
    public void ModifyVertices(List<UIVertex> vertexList)
    {
        int count = vertexList.Count;
        float bottomY = vertexList[0].position.x;
        float topY = vertexList[0].position.x;
        for (int i = 1; i < count; i++)
        {
            float y = vertexList[i].position.x;
            if (y > topY)
            {
                topY = y;
            }
            else if (y < bottomY)
            {
                bottomY = y;
            }
        }
        float uiElementHeight = topY - bottomY;
        for (int i = 0; i < count; i++)
        {
            UIVertex uiVertex = vertexList[i];
            uiVertex.color = Color32.Lerp(bottomColor, topColor, (uiVertex.position.x - bottomY) / uiElementHeight);
            vertexList[i] = uiVertex;
        }
    }
}