using System;
using Tiles;

namespace Levels;

public class PieceOdds
{
    private string _piece;
    public string Piece{
        get => _piece;
        set{
           var pieceType = TileDict.GetEnum(value); //this can throw an unhandled exception, which is good for now I guess
           //so if it doesn't throw an exception then I'm good to go
           _piece = value; 
        }
    }
    private int _odds;
    public int Odds
    {
        get => _odds;
        set{
            if(value > 0){
                _odds = value;
            }
            else{
                throw new ArgumentOutOfRangeException("Odds must be positive integer");
            }
        }
    }
}