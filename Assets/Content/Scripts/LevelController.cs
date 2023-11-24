using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private HUD _hud;
    [SerializeField] private float _borderOffset;
    
    private PlayerController _player;

    private UpdateSystem _updateSystem;
    private EnemiesSystem _enemiesSystem;

    private Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
        var borderOffset = Vector2.one * _borderOffset;
        var screenResolution = _camera.pixelRect.size;
        var worldSize = _camera.ScreenToWorldPoint(screenResolution) + (Vector3)borderOffset;

        var playerView = Instantiate(PlayerConfig.PlayerView, Vector3.zero, Quaternion.identity);
        var (healthBar, protectionBar) = _hud.Bars;
        playerView.SetBars(healthBar, protectionBar);
        _player = new PlayerController(playerView, PlayerConfig.PlayerSettings, worldSize);

        _updateSystem = new GameObject("UpdateSystem").AddComponent<UpdateSystem>();
        _enemiesSystem = new EnemiesSystem(worldSize, playerView.transform, _updateSystem);
        
        _updateSystem.Register(_player);
    }
}
