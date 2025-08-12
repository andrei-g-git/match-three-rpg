using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Tiles;
namespace Util;

public static class Debugging
{
    public static Action<List<List<Control>>, List<Vector2I>> CurryPrintItemsInRowInitials(int howManyLetters, int spacing) => 
        (List<List<Control>> grid, List<Vector2I> cellRow) => {
            var row = new List<Control>();
            foreach(var cell in cellRow){
                row.Add(grid[cell.X][cell.Y]);
            }

            var extraSpace = new String(' ', spacing);
            var computedNewLines = howManyLetters % 2;
            var initialsRow = row.Select(item => {
                var name = (item as Tile).Type.ToString().ToLower();
                if(name.Length > howManyLetters){
                    return name[..howManyLetters] + extraSpace;
                }else{
                    return name + extraSpace;
                }
            });
            var finalRow = initialsRow.ToArray(); //+ (new String('\n', computedNewLines));
            GD.Print(finalRow/* , "\n" */);
            GD.Print(new String('\n', computedNewLines));
            // for (int a = 0; a < computedNewLines; a++){
            //     GD.Print("\n");
            // }            
        };

    public static void PrintItemsInitials(List<List<Control>> grid, int howManyLetters, int spacing, string header){
        GD.Print("\n", header);
        Hex.IterateOverRowsNorthEast(
            grid,
            CurryPrintItemsInRowInitials(2, 2)
        );
        GD.Print("--------------------------");
    }

    public static void PrintStackedGridInitialsTilted(List<List<Control>> grid, int howManyLetters, int spacing, string header){
        foreach(var row in grid){
            var extraSpace = new String(' ', spacing);
            var computedNewLines = howManyLetters % 2;
            var initialsRow = row.Select(item => {
                if(item is Tile tile){
                    var name = (item as Tile).Type.ToString();//.ToLower();
                    if(name.Length > howManyLetters){
                        return name[..howManyLetters] + extraSpace;
                    }else{
                        return name + extraSpace;
                    }                    
                }else{
                    return "Nu" + extraSpace;
                }

            });
            var finalRow = initialsRow.ToArray(); 
            GD.Print(finalRow);
            GD.Print(new String('\n', computedNewLines));            
        }
    }

    public static void PrintStackedGridInitials(List<List<Control>> grid, int howManyLetters, int spacing, string header){
        var extraSpace = new String(' ', spacing);
        var computedNewLines = howManyLetters % 2;        
        for(int x=0;x<grid.Count;x++){
            var row = new List<string>();
            for(int y=0;y<grid.Count;y++){
                var item = grid[y][x]; //REVERSED
                if(item is Tile tile){
                    row.Add(tile.Type.ToString().Substring(0, howManyLetters) + extraSpace);
                }else{
                    row.Add("Nu" + extraSpace);
                }        
            }   
            GD.Print([.. row]);
            GD.Print(new String('\n', computedNewLines));                       
        }
    }

    public static void PrintChildrenTileInitials(List<Node> children, int howManyLetters, string header){
        GD.Print(header);
        var nodeList = new List<string>();
        foreach(var node in children){
            if(node is Tile tile){
                nodeList.Add(tile.Type.ToString().Substring(0, howManyLetters));
            }else{
                nodeList.Add("Nu");
            }
        }
        GD.Print([..nodeList]);
    }
}