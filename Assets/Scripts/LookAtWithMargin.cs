using UnityEngine;

public class LookAtWithMargin : MonoBehaviour
{
    [SerializeField] private Transform target; // El objeto al que apuntar
    [SerializeField][Range(0, 1)] private float errorMargin = 0.25f; // Margen de error (25% por defecto)

    private void Start()
    {
        target = GameManager.instance.objetoJugador.transform;

        // Excluir que el target sea el mismo objeto que lleva el script
        if (target == transform)
        {
            return;
        }

        // Dirección hacia el objetivo
        Vector3 directionToTarget = target.position - transform.position;

        // Introducir margen de error
        Vector3 randomError = new Vector3(
            Random.Range(-errorMargin, errorMargin),
            Random.Range(-errorMargin, errorMargin),
            Random.Range(-errorMargin, errorMargin)
        );

        // Ajustar dirección con el margen de error
        Vector3 adjustedDirection = directionToTarget + randomError;

        // Orientar el objeto hacia la nueva dirección
        transform.rotation = Quaternion.LookRotation(adjustedDirection);
    }
}