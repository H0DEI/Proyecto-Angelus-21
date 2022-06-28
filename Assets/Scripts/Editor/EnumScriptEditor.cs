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
    private SerializedProperty propiedadDa�o;
    private SerializedProperty propiedadAcciones;
    private SerializedProperty propiedadObjetivos;
    private SerializedProperty propiedadTipoSeleccion;
    private SerializedProperty propiedadCantidad;
    private SerializedProperty propiedadUsosLimitados;
    private SerializedProperty propiedadNumeroDeUsos;
    private SerializedProperty propiedadTier;

    private void OnEnable()
    {
        propiedadNombre = serializedObject.FindProperty("nombre");
        propiedadDescripcion = serializedObject.FindProperty("descripcion");
        propiedadCoste = serializedObject.FindProperty("coste");
        propiedadVelocidad = serializedObject.FindProperty("velocidad");
        propiedadFuerza = serializedObject.FindProperty("fuerza");
        propiedadPenetracion = serializedObject.FindProperty("penetracion");
        propiedadDa�o = serializedObject.FindProperty("da�o");
        propiedadAcciones = serializedObject.FindProperty("acciones");
        propiedadObjetivos = serializedObject.FindProperty("objetivos");
        propiedadTipoSeleccion = serializedObject.FindProperty("tipoSeleccion");
        propiedadCantidad = serializedObject.FindProperty("cantidad");
        propiedadUsosLimitados = serializedObject.FindProperty("usosLimitados");
        propiedadNumeroDeUsos = serializedObject.FindProperty("numeroDeUsos");
        propiedadTier = serializedObject.FindProperty("tier");
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
        EditorGUILayout.PropertyField(propiedadDa�o);
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

                propiedadCantidad.intValue = 2;

                EditorGUILayout.PropertyField(propiedadCantidad);

                break;
        }

        EditorGUILayout.PropertyField(propiedadUsosLimitados);

        if (propiedadUsosLimitados.boolValue == true)
        {
            propiedadNumeroDeUsos.intValue = 1;

            EditorGUILayout.PropertyField(propiedadNumeroDeUsos);
        }
        else
        {
            propiedadNumeroDeUsos.intValue = 727;
        }

        EditorGUILayout.PropertyField(propiedadTier);

        serializedObject.ApplyModifiedProperties();
    }
}
