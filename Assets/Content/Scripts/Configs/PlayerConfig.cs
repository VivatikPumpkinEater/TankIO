using System;
using UnityEngine;

public class PlayerConfig : BaseConfig<PlayerConfig>
{
    [SerializeField] private PlayerModel _playerModel;
    
    public static PlayerView PlayerView => Instance._playerModel.View;
    public static PlayerSettings PlayerSettings => Instance._playerModel.Settings;

    [Serializable]
    public struct PlayerModel
    {
        public PlayerView View;
        public PlayerSettings Settings;
    }
}