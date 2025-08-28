using Common;

public partial class UIEventBus: EventBus
{
    public override void _Ready(){
        AddUserSignal(Events.EnergyChanged.ToString());
    }
}