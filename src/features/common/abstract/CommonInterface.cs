using Board;
using Godot;

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
}