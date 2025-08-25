using Godot.Collections;
using Godot;
using System.Collections.Generic;
//using System.IO;
using System;
using System.Text.Json;
using System.Threading.Tasks;

public static class Files
{
	public static Grid<string>LoadCsv(string path){
		Grid<string> grid = new Grid<string>();
		var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
		while(!file.EofReached()){
			//var row = new Array<string>(file.GetCsvLine());
			var row = new List<string>(file.GetCsvLine());
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

	public static async Task SaveJson(object data, string path, string fileName){
		if(fileName.Contains(".json")){
			var fullPath = System.IO.Path.Join(path, fileName);
			var json = JsonSerializer.Serialize(data);
			await System.IO.File.WriteAllTextAsync(fullPath, json);			
		}else{
			throw new ArgumentException("The file name must have the .json extension");
		}
	}

	public static async Task SaveJsonIfNoneExists(object data, string path, string fileName){
		var fullPath = System.IO.Path.Join(path, fileName);
		if(!System.IO.File.Exists(fullPath)){
			
		}
	}
}