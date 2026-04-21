using System;
using Godot;
using Godot.Collections;
using Tiles;


public partial class DragAndDrop : Control
{
	[Export] private Control _tileNode;
    [Export] private Container _container;
    [Export] private Sprite2D _sprite;

	[Signal] public delegate void EngagedByEventHandler(Control sourceNode);
    Vector2[] _hexPoints;
    private float _w, _h = 0f;
    public override void _Ready(){
        _w = _container.Size.X;
        _h = _container.Size.Y;

        _hexPoints = [
            new Vector2(_w * 0.25f, 0.0f),      // clockwise from top left
            new Vector2(_w * 0.75f, 0.0f),     
            new Vector2(_w, _h * 0.5f), 
            new Vector2(_w * 0.75f, _h),       
            new Vector2(_w * 0.25f, _h),      
            new Vector2(0.0f, _h * 0.5f)  
        ];
    }

    public override void _Draw(){
        var x = Position.X;
        var y = Position.Y;

        // for(int i=0; i<_hexPoints.Length; i++){
        //     if (i < _hexPoints.Length - 1){
        //         var src = _hexPoints[i];
        //         var dst = _hexPoints[i+1];
        //         DrawLine(src, dst, new Color(1, 0, 0, 1), 2f);                
        //     }
        // }
        //DrawCircle(new Vector2(32f, 27.5f), )

        DrawPolygon(_hexPoints, [new Color(1, 0.5f, 0.5f, 0.4f)]);
    }

    //TODO: sometimes some pieces's drag surface becomes mostly un-dragable. Check if this is still the case when there are no enemies in the level.
    //FIX the sizing transforms of pieces, there is almost certainly something wrong with them.
    public override Variant _GetDragData(Vector2 atPosition){
        var texture = _sprite.Texture;
        var dragPreview = new TextureRect();
        dragPreview.CustomMinimumSize = new Vector2(_w, _h);
        //dragPreview.Position = Vector2.Zero - /* Position; */atPosition; //-0.5f * dragPreview.CustomMinimumSize; //apparently this is being overidden by the engine so it's pointless...
        dragPreview.Texture = texture;
        var previewNode = new Control();
        previewNode.AddChild(dragPreview);
        dragPreview.Position = -0.5f * dragPreview.CustomMinimumSize;
        SetDragPreview(/* dragPreview */previewNode); //well none of this works...
        GD.Print((_tileNode as Tile).Type.ToString());
		return _tileNode;        
    }

	public override bool _CanDropData(Vector2 atPosition, Variant data){ //this can drop data on it's self since the root tile is a separate control node with offset boundaries
        var withinHexagon = Geometry2D.IsPointInPolygon(atPosition, _hexPoints);    
		if((Control) data != null){ //SWAPPING
            if(withinHexagon){
			    return true;                
            }else{ 
                //throw new Exception("out of hexagon bounds"); //this just craps up my debugger output when dragging through an invalid area
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