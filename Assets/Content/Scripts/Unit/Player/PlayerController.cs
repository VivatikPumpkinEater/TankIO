using Player;
using UnityEngine;

public class PlayerController : Unit<PlayerView>
{
    private readonly BodyController _bodyController;
    
    public PlayerController(PlayerView view, PlayerSettings playerSettings, Vector2 worldSize) : base(view, playerSettings)
    {
        _bodyController = new BodyController(view.Rigidbody2D, playerSettings, worldSize);
    }

    public override void ManualFixedUpdate(float fixedDeltaTime)
    {
        if (!IsAlive)
            return;
        
        _bodyController.ManualFixedUpdate(fixedDeltaTime);
    }
}