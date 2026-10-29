
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private int vidaMaxima = 3;

    private int vidaActual;

    private void Awake()
    {
        vidaActual = vidaMaxima;

        Debug.Log(
            gameObject.name + " aparece con " +
            vidaActual + " puntos de vida."
        );
    }

    public void RecibirDaño(int cantidad)
    {
        if (vidaActual <= 0)
        {
            return;
        }

        vidaActual -= cantidad;

        Debug.Log(
            gameObject.name + " recibió " + cantidad +
            " de daño. Vida restante: " + vidaActual
        );

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Debug.Log(gameObject.name + " ha muerto.");

        Destroy(gameObject);
    }
}
