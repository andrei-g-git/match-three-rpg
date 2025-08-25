using Godot.Collections;
using Godot;
using System.Collections.Generic;
//using System.IO;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;

public static class Files
{
	public static string LevelEnvironmentsPath{get;} = "content/levels/environment/";
	public static string LevelPiecesPath{get;} = "content/levels/pieces/";
	public static string LevelsPath{get;} = "content/levels/";	
	public static string SavesPath{get;} = "saves/";
	public static string ManualSavesPath{get;} = "saves/manual/";
	private static readonly string _userPath = ProjectSettings.GlobalizePath("user://");


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
			var fullPath = System.IO.Path.Join(_userPath, path, fileName);
			var json = JsonSerializer.Serialize(data);
			await System.IO.File.WriteAllTextAsync(fullPath, json);			
		}else{
			throw new ArgumentException("The file name must have the .json extension");
		}
	}

	public static async Task SaveJsonIfNoneExists(object data, string path, string fileName){
		var fullPath = System.IO.Path.Join(_userPath, path, fileName);
		var json = JsonSerializer.Serialize(data);
		if (System.IO.File.Exists(fullPath))
			return; // already present — leave it alone

		try
		{
			await System.IO.File.WriteAllTextAsync(fullPath, json, Encoding.UTF8).ConfigureAwait(false);
		}
		catch (UnauthorizedAccessException exception)
		{
			// no permissions to write the file
			Console.Error.WriteLine($"Permission error writing {fullPath}: {exception.Message}");
		}
		catch (System.IO.DirectoryNotFoundException exception)
		{
			// path invalid
			Console.Error.WriteLine($"Directory not found for {fullPath}: {exception.Message}");
		}
		catch (System.IO.PathTooLongException exception)
		{
			Console.Error.WriteLine($"Path too long for {fullPath}: {exception.Message}");
		}
		catch (System.IO.IOException exception)
		{
			// I/O error (disk full, file locked by another process, etc.)
			Console.Error.WriteLine($"I/O error writing {fullPath}: {exception.Message}");
		}
		catch (Exception exception)
		{
			// fallback for unexpected exceptions
			Console.Error.WriteLine($"Unexpected error writing {fullPath}: {exception.Message}");
			throw;
		}
	}

	public static void CreateFolder(string fullRelativePath){
		var dir = System.IO.Path.Combine(_userPath, fullRelativePath);
		System.IO.Directory.CreateDirectory(dir);
	}


    public static async Task<object?> LoadJson<T>(string path, string fileName){
		var fullPath = System.IO.Path.Join(_userPath, path, fileName);
        try{
            string json = await System.IO.File.ReadAllTextAsync(fullPath, Encoding.UTF8).ConfigureAwait(false);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			var deserialized = JsonSerializer.Deserialize<object>(json, options);
            return deserialized;
        }
        catch (System.IO.FileNotFoundException){
            return default;
        }
        catch (UnauthorizedAccessException exception){
            Console.Error.WriteLine($"Access denied reading {fullPath}: {exception.Message}");
            throw;
        }
        catch (System.IO.DirectoryNotFoundException){
            return default;
        }
        catch (JsonException exception){
            Console.Error.WriteLine($"Invalid JSON in {fullPath}: {exception.Message}");
            throw;
        }
        catch (System.IO.IOException exception){ //disk issues, locked file, etc.
            Console.Error.WriteLine($"I/O error reading {fullPath}: {exception.Message}");
            throw;
        }
    }	
}