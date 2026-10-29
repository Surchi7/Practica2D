using UnityEngine;

public class Estado_Volar : State_Base
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 3f;

    [Header("Ondulación")]
    [SerializeField] private float amplitud = 1f;
    [SerializeField] private float frecuencia = 2f;

    private Rigidbody2D rb;
    private Transform player;

    private float tiempo;

    protected override void Awake()
    {
        base.Awake();

        rb = controlledObject.GetComponent<Rigidbody2D>();
    }

    public override void EnterState()
    {
        GameObject objetoPlayer = GameObject.FindGameObjectWithTag("Player");

        if (objetoPlayer != null)
        {
            player = objetoPlayer.transform;
        }

        tiempo = 0f;
    }

    public override void UpdateState()
    {
        if (rb == null || player == null)
            return;

        tiempo += Time.deltaTime;

        // Dirección horizontal hacia el jugador
        float direccionX = Mathf.Sign(
            player.position.x -
            controlledObject.transform.position.x
        );

        // Altura deseada respecto al jugador
        float alturaObjetivo =
            player.position.y +
            Mathf.Sin(tiempo * frecuencia) * amplitud;

        // Diferencia entre nuestra altura y la altura deseada
        float diferenciaY =
            alturaObjetivo -
            controlledObject.transform.position.y;

        // Limitamos la velocidad vertical
        float velocidadY = Mathf.Clamp(
            diferenciaY * velocidad,
            -velocidad,
            velocidad
        );

        rb.linearVelocity = new Vector2(
            direccionX * velocidad,
            velocidadY
        );
    }
}
