//using System.Collections.Generic;
using Godot.Collections;
using Godot;


namespace UI;

public interface ProgressableBar{
    public void Update(int value, int maxValue);    
}

public interface DisplayableElements{
    public void Update<[MustBeVariant]T>(Array<T> elementsData);
}

public interface DisplayableNodeListFromStrings{
    public void Update(Array<string> elementsData);
}