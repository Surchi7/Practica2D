
using UnityEngine;

public class Estado_Volar : State_Base
{
    [Header("Movimiento horizontal")]
    [SerializeField] private float velocidad = 3f;

    [Header("Ondulación")]
    [SerializeField] private float amplitud = 1f;
    [SerializeField] private float frecuencia = 2f;

    private Rigidbody2D rb;
    private Transform player;

    private float alturaBase;
    private float tiempo;

    protected override void Awake()
    {
        base.Awake();
        rb = controlledObject.GetComponent<Rigidbody2D>();
    }

    public override void EnterState()
    {
        GameObject objetoPlayer =
            GameObject.FindGameObjectWithTag("Player");

        if (objetoPlayer != null)
        {
            player = objetoPlayer.transform;
        }

        // Guardamos la altura inicial del enemigo.
        alturaBase = controlledObject.transform.position.y;
        tiempo = 0f;
    }

    public override void UpdateState()
    {
        if (rb == null || player == null)
            return;

        tiempo += Time.deltaTime;

        // 1. Avanzar horizontalmente hacia el jugador.
        float direccionX = Mathf.Sign(
            player.position.x - controlledObject.transform.position.x
        );

        float nuevaX =
            rb.position.x + direccionX * velocidad * Time.deltaTime;

        // 2. Oscilar alrededor de la altura base.
        float nuevaY =
            alturaBase +
            Mathf.Sin(tiempo * frecuencia * Mathf.PI * 2f) * amplitud;

        // 3. Aplicar la nueva posición.
        rb.MovePosition(new Vector2(nuevaX, nuevaY));
    }
}

