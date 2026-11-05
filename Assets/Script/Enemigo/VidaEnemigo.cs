using UnityEngine;

[DisallowMultipleComponent]
public class VidaEnemigo : MonoBehaviour, IDamageable
{
    [SerializeField] private int vidaMaxima = 3;

    private int vidaActual;

    public int VidaActual => vidaActual;
    public int VidaMaxima => vidaMaxima;
    public bool EstaVivo => vidaActual > 0;

    private void Awake()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDaño(int cantidad)
    {
        if (cantidad <= 0 || vidaActual <= 0)
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
