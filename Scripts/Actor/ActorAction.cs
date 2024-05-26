using Godot;

public partial class ActorAction : Node2D
{
    protected Actor TheActor;
    
    public override void _Ready()
    {
        this.SearchParent(out TheActor);
        TheActor.RegisterAction(this);
    }

    public virtual void InitiateAction()
    {
        TheActor.Visible = true;
        TheActor.ProcessMode = ProcessModeEnum.Inherit;
    }

    public virtual void ActionDone()
    {
        
    }
}