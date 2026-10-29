using UnityEngine;

public class Estado_Danio : State_Base_Enemigo
{
    [Header("Daño")]
    [SerializeField] private int daño = 1;

    [Header("Rebote")]
    [SerializeField] private float fuerzaRebote = 5f;
    [SerializeField] private float fuerzaReboteVertical = 2f;

    [Header("Tiempo")]
    [SerializeField] private float tiempoDanio = 0.2f;

    private Rigidbody2D rb;
    private float temporizador;

    protected override void Awake()
    {
        base.Awake();

        rb = controlledObject.GetComponent<Rigidbody2D>();
    }

    public override void EnterState()
    {
        temporizador = 0f;

        // 1. Hacemos daño al jugador
        GameObject objetoPlayer = GameObject.FindGameObjectWithTag("Player");

        if (objetoPlayer != null)
        {
            Player player = objetoPlayer.GetComponent<Player>();

            if (player != null)
            {
                player.RecibirDaño(daño);
            }
        }

        // 2. Aplicamos el rebote
        AplicarRebote();
    }

    public override void UpdateState()
    {
        temporizador += Time.deltaTime;

        if (temporizador >= tiempoDanio)
        {
            // Volvemos al estado que teníamos antes del daño
            State_Base_Enemigo estadoAnterior = state_machine.GetPreviousState();

            if (estadoAnterior != null)
            {
                state_machine.SetState(estadoAnterior);
            }
        }
    }

    private void AplicarRebote()
    {
        if (rb == null)
            return;

        GameObject objetoPlayer = GameObject.FindGameObjectWithTag("Player");

        if (objetoPlayer == null)
            return;

        float direccion = Mathf.Sign(
            controlledObject.transform.position.x -
            objetoPlayer.transform.position.x
        );

        rb.linearVelocity = new Vector2(
            direccion * fuerzaRebote,
            fuerzaReboteVertical
        );
    }
}
