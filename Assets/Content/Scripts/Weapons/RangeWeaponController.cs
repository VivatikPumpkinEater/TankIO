public class RangeWeaponController
{
    private readonly RangeWeaponView _view;
    private readonly BulletPool _pool;
    private readonly WeaponSettings _settings;

    public RangeWeaponController(RangeWeaponView view, BulletPool pool, WeaponSettings settings)
    {
        _view = view;
        _pool = pool;
        _settings = settings;
    }
}