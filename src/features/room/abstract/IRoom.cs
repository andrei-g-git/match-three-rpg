using System.Collections.Generic;

namespace Room;

public interface ModifiableRoom{
    public List<string> Modifiers{get;set;}
    public int RoomIndex{get;set;}
}