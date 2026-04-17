using Common;

public partial class UIEventBus: EventBus
{
    public override void _Ready(){
        AddUserSignal(Events.EnergyChanged.ToString());
        AddUserSignal(Events.FireChanged.ToString());
        AddUserSignal(Events.WindChanged.ToString());
        AddUserSignal(Events.EarthChanged.ToString());
        AddUserSignal(Events.WaterChanged.ToString());
    }
}