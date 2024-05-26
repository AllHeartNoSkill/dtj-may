namespace DTJMay.Scripts.Actor;

public partial class HideAction : ActorAction
{
    public override void InitiateAction()
    {
        base.InitiateAction();
        TheActor.Visible = false;
        TheActor.ProcessMode = ProcessModeEnum.Disabled;
    }

    public override void ActionDone()
    {
        
    }

    public override void _Process(double delta)
    {
        
    }
}