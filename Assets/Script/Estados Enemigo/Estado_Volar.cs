
using UnityEngine;

public class Estado_Volar : State_Base_Enemigo
{
    [Header("Persecución")]
    [SerializeField] private float velocidad = 3f;

    [Header("Ondulación")]
    [SerializeField] private float amplitud = 0.5f;
    [SerializeField] private float frecuencia = 2f;

    private Rigidbody2D rb;
    private float tiempo;

    protected override void Awake()
    {
        base.Awake();
        rb = controlledObject.GetComponent<Rigidbody2D>();
    }

    public override void EnterState()
    {
        tiempo = 0f;
    }

    public override void UpdateState()
    {
        Transform player = state_machine.Jugador;

        if (rb == null || player == null)
            return;

        tiempo += Time.deltaTime;

        // Dirección hacia el jugador en ambos ejes.
        Vector2 direccion = (
            (Vector2)player.position - rb.position
        ).normalized;

        // Movimiento base hacia el jugador.
        Vector2 movimiento =
            direccion * velocidad * Time.deltaTime;

        // Ondulación vertical agregada al movimiento.
        float ondulacion =
            Mathf.Sin(tiempo * frecuencia * Mathf.PI * 2f)
            * amplitud * Time.deltaTime;

        movimiento.y += ondulacion;

        rb.MovePosition(rb.position + movimiento);
    }
}
