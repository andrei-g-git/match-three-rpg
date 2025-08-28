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
        public void Subscribe(Action callback, Events event_);
    }
}