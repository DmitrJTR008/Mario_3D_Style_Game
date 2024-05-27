using UnityEngine;

public class PlayerBonusBox : ITimerUser
{
    private Player _player;
    private Material _playerMaterial;
    private Color _originalColor;
    private TimerHandle _timerHandleImmortal;

    private bool _isImmortal;
    private float _immortalTime;

    public PlayerBonusBox(Player player)
    {
        _player = player;
        _timerHandleImmortal = new TimerHandle();
    }
    public bool IsImmortal() => _isImmortal;

    public void EnableImmortal(Material material, float time)
    {
        if (_isImmortal) return;

        _isImmortal = true;
        _immortalTime = time;
        _playerMaterial = material;
        _timerHandleImmortal.maxTime = time;
        TimerGlobalManager.StaticClass.SetTimer(_timerHandleImmortal, this);
        MaterialEffect();
    }
    private async void MaterialEffect()
    {
        if(_playerMaterial == null) return; 

        _originalColor = _playerMaterial.color;
        float elapsedTime = 0f;

        while (_isImmortal && elapsedTime < _immortalTime)
        {
            float t = Mathf.PingPong(Time.time, 1f);
            _playerMaterial.color = Color.Lerp(_originalColor, Color.black, t);

            elapsedTime += Time.deltaTime;
            await System.Threading.Tasks.Task.Yield();
        }

        _playerMaterial.color = _originalColor; // Вернуть исходный цвет
        _isImmortal = false;
    }

    public void AcceptBonus(IBonus bonus)
    {
        bonus.Consume(_player);
    }
}
