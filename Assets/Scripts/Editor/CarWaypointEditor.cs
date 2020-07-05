using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CarWaypointEditor : EditorWindow
{
    [MenuItem("Tools/Car Waypoint Editor")]
    public static void Open()
    {
        GetWindow<CarWaypointEditor>();
    }

    public Transform waypointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if (waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root transorm must be selected. Plese assign a root transform", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();

    }


    void DrawButtons()
    {
        if (GUILayout.Button("Create CarWaypoint"))
        {

            CreateCarWaypoint();
        }
        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<CarWaypoint>())
        {
            if (GUILayout.Button("Add Branch CarWaypoint"))
            {
                CreateCarBranch();
            }
            if (GUILayout.Button("Create CarWaypoint Before"))
            {
                CreateCarWaypointBefore();
            }
            if (GUILayout.Button("Create CarWaypoint After"))
            {
                CreateCarWaypointAfter();
            }
            if (GUILayout.Button("Remove CarWaypoint"))
            {
                RemoveCarWaypoint();
            }
        }
    }


    void CreateCarWaypointBefore()
    {
        GameObject waypointObject = new GameObject("CarWaypoint " + waypointRoot.childCount, typeof(CarWaypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        CarWaypoint newWaypoint = waypointObject.GetComponent<CarWaypoint>();

        CarWaypoint selectedWaypoint = Selection.activeGameObject.GetComponent<CarWaypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        if (selectedWaypoint.previousWaypoint != null)
        {

            newWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
            selectedWaypoint.previousWaypoint.previousWaypoint = newWaypoint;
        }

        newWaypoint.nextWaypoint = selectedWaypoint;

        selectedWaypoint.previousWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void CreateCarWaypointAfter()
    {
        GameObject waypointObject = new GameObject("CarWaypoint " + waypointRoot.childCount, typeof(CarWaypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        CarWaypoint newWaypoint = waypointObject.GetComponent<CarWaypoint>();

        CarWaypoint selectedWaypoint = Selection.activeGameObject.GetComponent<CarWaypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        newWaypoint.previousWaypoint = selectedWaypoint;

        if (selectedWaypoint.nextWaypoint != null)
        {
            selectedWaypoint.nextWaypoint.previousWaypoint = newWaypoint;
            newWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
        }

        selectedWaypoint.nextWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = newWaypoint.gameObject;
    }


    void RemoveCarWaypoint()
    {
        CarWaypoint selectedWaypoint = Selection.activeGameObject.GetComponent<CarWaypoint>();

        if (selectedWaypoint.nextWaypoint != null)
        {
            selectedWaypoint.nextWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
        }
        if (selectedWaypoint.previousWaypoint != null)
        {
            selectedWaypoint.previousWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
            Selection.activeGameObject = selectedWaypoint.previousWaypoint.gameObject;
        }

        DestroyImmediate(selectedWaypoint.gameObject);
    }


    void CreateCarBranch()
    {
        GameObject waypointObject = new GameObject("CarWaypoint " + waypointRoot.childCount, typeof(CarWaypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        CarWaypoint waypoint = waypointObject.GetComponent<CarWaypoint>();

        CarWaypoint branchedFrom = Selection.activeGameObject.GetComponent<CarWaypoint>();
        branchedFrom.branches.Add(waypoint);

        waypoint.transform.position = branchedFrom.transform.position;
        waypoint.transform.forward = branchedFrom.transform.forward;

        Selection.activeGameObject = waypoint.gameObject;


    }


    void CreateCarWaypoint()
    {
        GameObject waypointObject = new GameObject("CarWaypoint " + waypointRoot.childCount, typeof(CarWaypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        CarWaypoint waypoint = waypointObject.GetComponent<CarWaypoint>();

        if (waypointRoot.childCount > 1)
        {
            waypoint.previousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<CarWaypoint>();
            waypoint.previousWaypoint.nextWaypoint = waypoint;

            waypoint.transform.position = waypoint.previousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;

        }

        Selection.activeObject = waypoint.gameObject;


    }

}


