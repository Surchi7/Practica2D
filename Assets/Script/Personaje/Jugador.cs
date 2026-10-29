using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField] public int vida;
    [SerializeField] public int velocidad;
    [SerializeField] public int daño;

    private CapsuleCollider2D capsuleCollider2D;
    private Transform transform;
    private Rigidbody2D rigidbody2D;
    public float fuerzaDeSalto = 7f; // Ajusta este valor en el Inspector si salta mucho o poco
    public int direccion = 1;

    private void Awake()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        transform = GetComponent<Transform>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            RecibirDaño(1);
        }
    }

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
