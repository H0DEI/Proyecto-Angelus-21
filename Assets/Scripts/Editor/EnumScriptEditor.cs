using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Habilidad))]
public class EnumScriptEditor : Editor
{
    private SerializedProperty propiedadNombre;
    private SerializedProperty propiedadDescripcion;
    private SerializedProperty propiedadCoste;
    private SerializedProperty propiedadVelocidad;
    private SerializedProperty propiedadFuerza;
    private SerializedProperty propiedadPenetracion;
    private SerializedProperty propiedadDaño;
    private SerializedProperty propiedadAcciones;
    private SerializedProperty propiedadObjetivos;
    private SerializedProperty propiedadTipoSeleccion;
    private SerializedProperty propiedadCantidad;

    private void OnEnable()
    {
        propiedadNombre = serializedObject.FindProperty("nombre");
        propiedadDescripcion = serializedObject.FindProperty("descripcion");
        propiedadCoste = serializedObject.FindProperty("coste");
        propiedadVelocidad = serializedObject.FindProperty("velocidad");
        propiedadFuerza = serializedObject.FindProperty("fuerza");
        propiedadPenetracion = serializedObject.FindProperty("penetracion");
        propiedadDaño = serializedObject.FindProperty("daño");
        propiedadAcciones = serializedObject.FindProperty("acciones");
        propiedadObjetivos = serializedObject.FindProperty("objetivos");
        propiedadTipoSeleccion = serializedObject.FindProperty("tipoSeleccion");
        propiedadCantidad = serializedObject.FindProperty("cantidad");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(propiedadNombre);
        EditorGUILayout.PropertyField(propiedadDescripcion);
        EditorGUILayout.PropertyField(propiedadCoste);
        EditorGUILayout.PropertyField(propiedadVelocidad);
        EditorGUILayout.PropertyField(propiedadFuerza);
        EditorGUILayout.PropertyField(propiedadPenetracion);
        EditorGUILayout.PropertyField(propiedadDaño);
        EditorGUILayout.PropertyField(propiedadAcciones);
        EditorGUILayout.PropertyField(propiedadObjetivos);
        EditorGUILayout.PropertyField(propiedadTipoSeleccion);

        TipoSeleccion tipo = (TipoSeleccion)propiedadTipoSeleccion.enumValueIndex;

        switch (tipo)
        {
            case TipoSeleccion.SoloJugador:
            case TipoSeleccion.SoloUnEnemigo:
            case TipoSeleccion.CualquierPersonaje:

                propiedadCantidad.intValue = 1;

                break;

            case TipoSeleccion.VariosEnemigos:

                EditorGUILayout.PropertyField(propiedadCantidad);

                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
