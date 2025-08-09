using Godot.Collections;
using Godot;

public static class Files
{
	public static Grid<string>LoadCsv(string path){
		Grid<string> grid = new Grid<string>();
		var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
		while(!file.EofReached()){
			var row = new Array<string>(file.GetCsvLine());
			if(row.Count > 1){
				//grid.Append(row);	// looks like csvs have a 'secret' row with crap content like ['0'] or something
				grid.AddRow(row);
			} 
							
		}
		return grid;	
	}

	public static Array<Array<string>>LoadCsv_test(string path){
		Array<Array<string>> grid = new Array<Array<string>>();
		var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
		while(!file.EofReached()){
			var row = new Array<string>(file.GetCsvLine());
			if(row.Count > 1){
				//grid.Append(row);	// looks like csvs have a 'secret' row with crap content like ['0'] or something
				grid.Add(row);
			} 
							
		}
		return grid;	
	}
}