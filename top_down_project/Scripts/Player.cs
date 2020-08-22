using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;
using static Extensions;
namespace Game.State
{
    public class Player : KinematicBody2D, IFiniteStateMachine
    {

        private Vector2 _velocity;
        private Sprite sprite ;
        
        // Exports Variables
        [Export]
        private int _speed;
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _velocity = Vector2.Zero;
            sprite = GetNode<Sprite>("Sprite");
            
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(float delta)
        {
            
            var dirX = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
            var dirY = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
            if (dirX != 0 || dirY  != 0){
                ChangeState(EnumStates.Move, dirX, dirY);
            }
            else{
                ChangeState(EnumStates.Idle);
            }
            MoveAndSlide(_velocity);
        }

        public void ChangeState(EnumStates newState,params object[] parameters)
        {
            switch (newState)
            {
                case EnumStates.Move:
                    if (parameters != null && parameters.Length == 2)
                    {
                        Move((float)parameters[0],(float)parameters[1]);
                    }else
                    {
                        throw new ArgumentException("MoveState nécessite l'utilisation de deux paramètres DirX et DirY");
                    }
                    break;
                case EnumStates.Attack:
                    Attack();
                    break;
                
                default:
                    Idle(); 
                    break;
                
            }
        }

        public void Idle(){
            _velocity.y  = Mathf.Lerp(_velocity.y,0,0.5f);
            _velocity.x  = Mathf.Lerp(_velocity.x,0,0.5f);        
        }
        public void Attack(){

        }
        public void Move(float dirX, float dirY){

            if(dirX !=  0) {
                _velocity.x = _speed * dirX;
                sprite.FlipH = !(dirX>0);
            }

            if(dirY !=  0) {
                _velocity.y = _speed * dirY;
                sprite.FlipH = !(dirY>0);
            }
        }
    }
}