using System;
using Godot;
using Godot.Collections;


public partial class DragAndDrop : Control
{
	[Export] private Control _tileNode;
    [Export] private Container _container;

	[Signal] public delegate void EngagedByEventHandler(Control sourceNode);
    Vector2[] _hexPoints;
    public override void _Ready(){
        var w = _container.Size.X;
        var h = _container.Size.Y;

        _hexPoints = [
            new Vector2(w * 0.25f, 0.0f),      // clockwise from top left
            new Vector2(w * 0.75f, 0.0f),     
            new Vector2(w, h * 0.5f), 
            new Vector2(w * 0.75f, h),       
            new Vector2(w * 0.25f, h),      
            new Vector2(0.0f, h * 0.5f)  
        ];
    }

    public override Variant _GetDragData(Vector2 atPosition){
		return _tileNode;        
    }

	public override bool _CanDropData(Vector2 atPosition, Variant data){ //this can drop data on it's self since the root tile is a separate control node with offset boundaries
        var withinHexagon = Geometry2D.IsPointInPolygon(atPosition, _hexPoints);    
		if((Control) data != null){ //SWAPPING
            if(withinHexagon){
			    return true;                
            }else{ 
                throw new Exception("out of hexagon bounds");
            }
		}else{ 
            throw new Exception("not dragging a tile"); //ig if it throws an exception it count as false?...
		}

		return false; //unreachable code??? how tf...
	}

    public override void _DropData(Vector2 atPosition, Variant data){
		if((Control) data != null){
			GD.Print("got data in tile");
			var sourceNode = (Control) data;

			EmitSignal(SignalName.EngagedBy, sourceNode);
		}
    }	
}