using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : IStateSwitcher
{
    private List<IState> _states;
    private IState _currentState;

    public CharacterStateMachine(Player player, IStateSwitcher switcher)
    {
        _states = new List<IState>()
        {
            new MoveState(player, this),
            new AirState(player, this),
            new DeadState(player, this),
            new WinState(player, this) 
        };
        _currentState = _states[1];
        _currentState?.Enter();
        
    }

    public void SwitchState<State>() where State : IState
    {
        IState state = _states.FirstOrDefault(state => state is State);
        
        if (state == null) return;

        _currentState?.Exit();
        _currentState = state;
        _currentState?.Enter();
    }

    public void HandleInput() => _currentState?.InputHandle();
    public void Update() => _currentState?.Update();
}
