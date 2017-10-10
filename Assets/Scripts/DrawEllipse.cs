using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class DrawEllipse : Editor {

    Planet planet;

    SerializedProperty mass;
    SerializedProperty minDistance;
    SerializedProperty maxDistance;
    SerializedProperty gravityForce;
    SerializedProperty planetVelocity;
    SerializedProperty systemCenter;
    SerializedProperty semiminor;
    SerializedProperty semimajor;

    // Use this for initialization
    void OnEnable () {
        planet = (Planet)target;
        mass = serializedObject.FindProperty("mass");
        minDistance = serializedObject.FindProperty("minDistance");
        maxDistance = serializedObject.FindProperty("maxDistance");
        gravityForce = serializedObject.FindProperty("gravityForce");
        planetVelocity = serializedObject.FindProperty("planetVelocity");
        systemCenter = serializedObject.FindProperty("systemCenter");
        semiminor = serializedObject.FindProperty("semiminor");
        semimajor = serializedObject.FindProperty("semimajor");
	}

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(mass);
        EditorGUILayout.PropertyField(minDistance);
        EditorGUILayout.PropertyField(maxDistance);
        EditorGUILayout.PropertyField(gravityForce);
        EditorGUILayout.PropertyField(planetVelocity);
        EditorGUILayout.PropertyField(systemCenter);
        EditorGUILayout.PropertyField(semiminor);
        EditorGUILayout.PropertyField(semimajor);
        serializedObject.ApplyModifiedProperties();
        planet.UpdateEllipse();
        planet.UpdatePlanetRenderer();
    }
}
