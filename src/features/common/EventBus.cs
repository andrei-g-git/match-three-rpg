using System;
using System.Linq;
using Godot;
using Godot.Collections;
using Common;

public partial class EventBus: Node, RemoteSignaling{

    public override void _Ready(){
        //AddUserSignal(Events.Attacking.ToString());        
    }
    // public void Publish(Events event_, ParameterDict[] parameters){
    //     var actualParameters = new Array<Dictionary<string, Variant>>(
    //         parameters.Select(parameterDict => parameterDict.GetDict())
    //     );
    //     EmitSignal(event_.ToString(), actualParameters);
    // }
    // public void Publish<[MustBeVariant]T>(Events event_, Array<T> parameters){
    //     EmitSignal(event_.ToString(), parameters);
    // }
    public void Publish(Events event_, Array<Variant> parameters){
        EmitSignal(event_.ToString(), [.. parameters]);
    }    
    public void Publish(Events event_){
        EmitSignal(event_.ToString());
    }
    // public void Subscribe(Callable callable, Events event_){
    //     Connect(event_.ToString(), callable);
    // }

    public void Subscribe<[MustBeVariant]T1, [MustBeVariant]T2>(Action<T1, T2> callback, Events event_){
        Connect(event_.ToString(), Callable.From(callback));
    }
}