using System;
using UnityEngine;
using EzySlice;

public class SliceTest : MonoBehaviour
{
    public GameObject sourceGo;//切割的物体
    public GameObject slicerGo;//切片物体
    public Material sectionMat;//切面材质


    [Header("mesh复制")] public GameObject OriginGameObject;
    [Header("mesh粘贴")] public GameObject TargetGameObject;

    private void Start()
    {
        Debug.Log("");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SlicedHull hull = sourceGo.Slice(slicerGo.transform.position, slicerGo.transform.right);
            GameObject upper = hull.CreateUpperHull(sourceGo, sectionMat);
            GameObject lower = hull.CreateLowerHull(sourceGo, sectionMat);
            sourceGo.SetActive(false);
            upper.transform.SetParent(sourceGo.transform.parent);
            upper.transform.localPosition=Vector3.zero;
            upper.transform.localScale=Vector3.one;

            lower.transform.SetParent(sourceGo.transform.parent);
            lower.transform.localPosition=Vector3.zero;
            lower.transform.localScale=Vector3.one;

            int lowerLength= lower.GetComponent<MeshFilter>().sharedMesh.vertices.Length;
            int upperLength = upper.GetComponent<MeshFilter>().sharedMesh.vertices.Length;

            GameObject go =lowerLength>upperLength? upper:lower;

            Vector3 dir= go.transform.TransformDirection(slicerGo.transform.right);
            dir = Vector3.Dot(Vector3.up, dir) > 0 ? dir : -dir;
            go.transform.localPosition= go.transform.localPosition + dir*0.2f;
            Debug.DrawRay(slicerGo.transform.position,dir*100,Color.red,3);
            
            //
            
            sourceGo= lowerLength > upperLength ? lower : upper;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            MeshFilter meshFilter=TargetGameObject.GetComponent<MeshFilter>();
            //
            meshFilter.sharedMesh = OriginGameObject.GetComponent<MeshFilter>().sharedMesh;
        }
    }

}