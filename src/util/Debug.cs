using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Tiles;
namespace Util;

public static class Debug
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
        GD.Print(header);
        Hex.IterateOverRowsNorthEast(
            grid,
            CurryPrintItemsInRowInitials(2, 2)
        );
        GD.Print("--------------------------");
    }


}