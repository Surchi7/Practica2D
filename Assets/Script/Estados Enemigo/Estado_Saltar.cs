using UnityEngine;

public class Estado_Saltar : State_Base_Enemigo
{
    [Header("Salto")]
    [SerializeField] private float fuerzaSalto = 7f;
    [SerializeField] private float velocidadHorizontal = 3f;

    [Header("Espera")]
    [SerializeField] private float tiempoEspera = 0.5f;

    private Rigidbody2D rb;

    private float temporizador;

    private bool salioDelSuelo;

    private enum Fase
    {
        Esperando,
        Saltando
    }

    private Fase fase;

    protected override void Awake()
    {
        base.Awake();

        rb = controlledObject.GetComponent<Rigidbody2D>();
    }

    public override void EnterState()
    {
        temporizador = 0f;

        if (state_machine.EstaEnElSuelo)
        {
            // Está apoyado: espera antes de saltar
            fase = Fase.Esperando;
            salioDelSuelo = false;
        }
        else
        {
            // Está en el aire: probablemente viene de un rebote
            fase = Fase.Saltando;
            salioDelSuelo = true;
        }
    }

    public override void UpdateState()
    {
        Transform player = state_machine.Jugador;

        if (rb == null || player == null)
            return;

        switch (fase)
        {
            case Fase.Esperando:

                temporizador += Time.deltaTime;

                if (temporizador >= tiempoEspera)
                {
                    SaltarHaciaJugador(player);

                    fase = Fase.Saltando;

                    salioDelSuelo = false;
                }

                break;


            case Fase.Saltando:

                // Primero esperamos a que realmente abandone el suelo
                if (!salioDelSuelo)
                {
                    if (!state_machine.EstaEnElSuelo)
                    {
                        salioDelSuelo = true;
                    }

                    return;
                }

                // Ahora sí podemos detectar el aterrizaje
                if (state_machine.EstaEnElSuelo)
                {
                    temporizador = 0f;

                    fase = Fase.Esperando;
                }

                break;
        }
    }

    private void SaltarHaciaJugador(Transform player)
    {
        float direccion = Mathf.Sign(
            player.position.x -
            controlledObject.transform.position.x
        );

        rb.linearVelocity = new Vector2(
            direccion * velocidadHorizontal,
            fuerzaSalto
        );
    }
}
