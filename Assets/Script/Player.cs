using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int vida = 10;

    public void RecibirDaño(int daño)
    {
        vida -= daño;

        Debug.Log("El jugador recibió " + daño + " de daño. Vida restante: " + vida);

        if (vida <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Debug.Log("El jugador murió");
    }
}
