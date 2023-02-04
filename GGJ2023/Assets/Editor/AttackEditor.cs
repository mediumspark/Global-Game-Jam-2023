using UnityEditor;
using UnityEngine;
using System.Collections.Generic; 

public class PropPlacementTool : EditorWindow
{
    [MenuItem("Tools/Prop Placement")]
    public static void PlaceProps()
    {
        GetWindow<PropPlacementTool>(); 
    }

    public float Radius = 0;
    public int spawnCount = 0; 
    public List<GameObject> Prefabs;
    public bool BrushOn; 

    SerializedObject so;
    SerializedProperty SRadius;
    SerializedProperty SSpawnCount;
    SerializedProperty SPrefabs;
    SerializedProperty SBrushOn;
    
    Vector2[] randPoints; 


    private void OnGUI()
    {
        so.Update();
        EditorGUILayout.PropertyField(SRadius);
        SRadius.floatValue = Mathf.Max(1f, SRadius.floatValue);

        EditorGUILayout.PropertyField(SSpawnCount);
        SSpawnCount.intValue = Mathf.Max(1, SSpawnCount.intValue);

        EditorGUILayout.PropertyField(SPrefabs);
        EditorGUILayout.PropertyField(SBrushOn);

        if (so.ApplyModifiedProperties())
        {
            GenPoints(); 
            SceneView.RepaintAll();
        }

    }

    void GenPoints()
    {
        randPoints = new Vector2[spawnCount];
        for(int i = 0; i < spawnCount; i++)
        {
            randPoints[i] = Random.insideUnitCircle; 
        }
    }

    void CreateNewPrefabs()
    {
        if (BrushOn)
        {
            foreach (Vector2 pos in randPoints)
            {
                int rand = Random.Range(0, Prefabs.Count);
                GameObject go = Instantiate(Prefabs[rand], pos, Quaternion.identity);
            }
        }
    }

    private void OnEnable()
    {
        GenPoints(); 
        so = new SerializedObject(this);
        SRadius = so.FindProperty("Radius");
        SSpawnCount = so.FindProperty("spawnCount");
        SPrefabs = so.FindProperty("Prefabs");
        SBrushOn = so.FindProperty("BrushOn");

        SceneView.duringSceneGui += DuringSceneGUI;
    }
    private void OnDisable() => SceneView.duringSceneGui -= DuringSceneGUI;

    void drawsphere(Vector2 pos)
    {
        Handles.SphereHandleCap(-1, pos, Quaternion.identity, 0.1f, EventType.Repaint);
    }

    private void DuringSceneGUI(SceneView SV)
    {
        if (BrushOn)
        {
            Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;

            Transform cam = SV.camera.transform;

            if (Event.current.type == EventType.MouseMove)
                SV.Repaint();

            if (Event.current.type == EventType.MouseDown)
                CreateNewPrefabs();

            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {

                Vector3 HitNormal = hit.normal;
                Vector3 HitTangent = Vector3.Cross(HitNormal, cam.forward).normalized;
                Vector3 hitBitangent = Vector3.Cross(HitNormal, HitTangent);

                foreach (Vector2 p in randPoints)
                {
                    Vector3 RayOrigin = hit.point + (HitTangent * p.x + hitBitangent * p.y) * Radius;
                    RayOrigin += HitNormal * 2;
                    Vector3 RayDirection = -HitNormal;

                    Ray ptRay = new Ray(RayOrigin, RayDirection);
                    if (Physics.Raycast(ptRay, out RaycastHit ptRayHit))
                    {
                        Handles.color = Color.red; 
                        drawsphere(ptRayHit.point);
                        Handles.DrawAAPolyLine(ptRayHit.point, ptRayHit.point + ptRayHit.normal);
                    }
/*                    Handles.color = Color.blue; 
                    drawsphere(RayOrigin);*/
                }
                Handles.color = Color.black;
                Handles.DrawAAPolyLine(5, hit.point, hit.point + hit.normal);
                Handles.DrawWireDisc(hit.point, hit.normal, Radius, 5);
            }

        }

    }

}