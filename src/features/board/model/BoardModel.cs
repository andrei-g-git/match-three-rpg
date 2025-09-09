using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Godot;
using Tiles;

namespace Board;

public partial class BoardModel : Node, Organizable, MatchableBoard, WithTiles, Queriable//, Mapable
{
    [Export] private Node _tileOrganizer;
    [Export] private Node _tileMatcher;
    [Export] private Node _tileQuery;
    [Export] private Node _tileFactory;
    [Export] private Node _tileContainer;
    [Export] private Node _environment;
	[Export] private Node _selectedSkillsModel;     
    [Export] private Node _turns;

    public Grid<Control> Tiles { 
        get => (_tileOrganizer as WithTiles).Tiles; 
        set {
            (_tileOrganizer as WithTiles).Tiles = value;
            (_turns as WithTiles).Tiles = value;          
        } 
    }


    public override void _Ready(){
        (_tileQuery as Mapable).Map = _environment as Tileable;

    }

    public void Initialize(Grid<TileTypes> tileTypes){
        (_tileOrganizer as Organizable).Initialize(tileTypes);
        //Tiles = (_tileOrganizer as WithTiles).Tiles;
        (_tileMatcher as WithTiles).Tiles = (_tileOrganizer as WithTiles).Tiles;

        (_tileQuery as WithTiles).Tiles = (_tileOrganizer as WithTiles).Tiles;
        (_turns as WithTiles).Tiles = (_tileOrganizer as WithTiles).Tiles;
        //(_turns as Initializable).Initialize(); //happens before player stats are loaded, need speed stat...
    }


    public void InitializeTurnQueue(){ //NOT INTERFACE METHOD
        (_turns as Initializable).Initialize();
    }


    public bool TryMatching(Control sourceTile, Control targetTile){
        return (_tileMatcher as MatchableBoard).TryMatching(sourceTile, targetTile);
    }

    public async Task TransferTileToTile(Control sourceTile, Control targetTile){
        await (_tileOrganizer as Organizable).TransferTileToTile(sourceTile, targetTile);
    }

    public /* async Task */ void TransferTileTo(Control tile, Vector2I target){
        /* await  */(_tileOrganizer as Organizable).TransferTileTo(tile, target);
    }    


    public void RelocateTile(Control tile, Vector2I target){
        (_tileOrganizer as Organizable).RelocateTile(tile, target);
    }


    public void MatchWithoutSwapping(){
        (_tileMatcher as MatchableBoard).MatchWithoutSwapping();
    }

    public void CollapseGridAndCheckNewMatches(){
        (_tileMatcher as MatchableBoard).CollapseGridAndCheckNewMatches();
    }

    public List<Control> GetNeighboringTiles(Vector2I center){
        return (_tileQuery as Queriable).GetNeighboringTiles(center);
    }

    public List<Control> GetAllActors(){
        return (_tileQuery as Queriable).GetAllActors();
    }

    public Control FindNextTileInLine(List<Vector2I> line){
        return (_tileQuery as Queriable).FindNextTileInLine(line);
    }

    public bool IsCellAdjacentToLine(Vector2I cell, List<Vector2I> line){
        return (_tileQuery as Queriable).IsCellAdjacentToLine(cell, line);
    }


}