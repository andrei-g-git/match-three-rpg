using System;
using Board;
using Godot;
using Godot.Collections;

namespace Common{
    public interface Mapable{
        Tileable Map{set;}
    }

    public interface WithSceneManager{
        public ChangeableScenes SceneManager{set;}
    }

    public interface ChangeableScenes{
        public void ChangeScene(Node instantiatedScene);
    }

    public interface RemoteSignaling{
        //public void Publish(Events event_, ParameterDict[] parameters);
        public void Publish(Events event_, Array<Variant> parameters);
        public void Publish(Events event_);
        //public void Subscribe(/* Action callback */Callable callable, Events event_);
        public void Subscribe<[MustBeVariant]T1, [MustBeVariant]T2>(Action<T1, T2> callback, Events event_);
    }

    public interface RelayableUIEvents{
        public RemoteSignaling UIEventBus{set;}
    }

    public interface Modelable{
        public void Notify();
    }
}