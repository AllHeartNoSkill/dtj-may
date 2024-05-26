using Godot;

namespace DTJMay.Scripts.Actor;

public partial class MoveAction : ActorAction
{
    [Export] private Vector2 _startPos = new Vector2();
    [Export] private Vector2 _endPos = new Vector2();
    [Export] private float _moveSpeed = 3f;

    private bool _isMoving = false;
    private Vector2 _moveDirection;

    public override void InitiateAction()
    {
        _isMoving = true;
        TheActor.GlobalPosition = _startPos;
        _moveDirection = (_endPos - _startPos).Normalized();
    }

    public override void ActionDone()
    {
        _isMoving = false;
        TheActor.Velocity = Vector2.Zero;
    }

    public override void _PhysicsProcess(double delta)
    {
        if(!_isMoving) return;
        TheActor.Velocity = _moveDirection * _moveSpeed;
        if ((TheActor.GlobalPosition - _endPos).LengthSquared() <= 0.01f)
        {
            ActionDone();
        }
    }
}