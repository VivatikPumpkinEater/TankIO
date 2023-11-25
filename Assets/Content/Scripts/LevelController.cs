using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private HUD _hud;
    
    private PlayerController _player;

    private UpdateSystem _updateSystem;
    private EnemiesSystem _enemiesSystem;
    private WeaponSystem _weaponSystem;
    private PlayerInput _playerInput;
    
    private Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
        var screenResolution = _camera.pixelRect.size;
        var worldSize = _camera.ScreenToWorldPoint(screenResolution);

        _playerInput = new PlayerInput();
        _playerInput.Enable();

        var playerView = Instantiate(PlayerConfig.PlayerView, Vector3.zero, Quaternion.identity);
        var (healthBar, protectionBar) = _hud.Bars;
        playerView.SetBars(healthBar, protectionBar);
        _player = new PlayerController(playerView, PlayerConfig.PlayerSettings, worldSize, _playerInput);
        _player.Die += OnPlayerDie;

        _updateSystem = new GameObject("UpdateSystem").AddComponent<UpdateSystem>();
        _enemiesSystem = new EnemiesSystem(worldSize, playerView.transform, _updateSystem);

        _weaponSystem = new WeaponSystem(playerView.WeaponHolder, playerView.TargetType, _hud, _playerInput);
        
        _updateSystem.Register(_player);
    }
    
    //Заглушка
    private void OnPlayerDie(IUnit _)
    {
        _player.Die -= OnPlayerDie;
        
        Dispose();
        SceneManager.LoadScene(0);
    }

    private void Dispose()
    {
        _playerInput.Disable();
        _weaponSystem.Dispose();
    }
}
