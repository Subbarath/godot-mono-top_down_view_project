using Godot;
using System;
namespace Game.State
{
    public interface IFiniteStateMachine
    {

        /// <summary>
        ///     Change l'état du node
        /// </summary>
        void ChangeState(EnumStates newState,params object[] parameters);
        void Idle();
        void Attack();
        void Move(float dirX, float dirY);
    }
}
