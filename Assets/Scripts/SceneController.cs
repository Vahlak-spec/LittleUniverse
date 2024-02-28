using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance => instance;

    public FollowController FollowController => _planet.FollowController;

    [SerializeField] private Planet _planet;
    [SerializeField] private PlayerController _player;
    [SerializeField] private Joystick _joystick;

    private static SceneController instance;

    private void Start()
    {
        instance = this;
        _player.Init(_planet);
    }

    private void Update()
    {
        _player.SetDirection(_joystick.Direction);
    }
}
