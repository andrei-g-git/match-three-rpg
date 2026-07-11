using Godot.Collections;
using Godot;
using System.Collections.Generic;
//using System.IO;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
//using System.IO;

public static class Files
{
	public static string LevelEnvironmentsPath{get;} = "content/levels/environment/";
	public static string LevelPiecesPath{get;} = "content/levels/pieces/";
	public static string LevelUpcomingPath{get;} = "content/levels/upcoming/";
	public static string LevelUpcomingBgPath{get;} = "content/levels/upcomingBg/";
	public static string LevelsPath{get;} = "content/levels/";	
	public static string SavesPath{get;} = "saves/";
	public static string ManualSavesPath{get;} = "saves/manual/";
	private static readonly string _userPath = ProjectSettings.GlobalizePath("user://");


	public static Grid<string>LoadCsv(string path){
		var fullPath = System.IO.Path.Join(_userPath, path);
		Grid<string> grid = new Grid<string>();

		if (!FileAccess.FileExists(fullPath)){    
			GD.PrintErr($"Missing file: {fullPath}");    
			return grid;
		}

		using var file = FileAccess.Open(fullPath, FileAccess.ModeFlags.Read); // In C#, using guarantees Dispose() is called at the end of the block (even if an exception occurs), which closes/releases the underlying file handle.
		while(!file.EofReached()){
			var row = new List<string>(file.GetCsvLine());
			if(row.Count > 1){
				//grid.Append(row);	// looks like csvs have a 'secret' row with crap content like ['0'] or something
				grid.AddRow(row);
			} 
							
		}
		return grid;	
	}

	public static async Task SaveCsvFromGrid(Grid<Control> grid, string path){
		var fullPath = System.IO.Path.Join(_userPath, path);
		//check if already exists first?
		var stringGrid = Hex.EnumGridToStrings(grid);

		using(var writer = new System.IO.StreamWriter(fullPath)){
			foreach(var row in stringGrid.GetGridAs2DList()){
				var csvLine = string.Join(",", row);
				writer.WriteLine(csvLine);
			}
		}
	}

	// public static async Task CopyFileAsync(string sourcePath, string destinationPath){
	// 	var fullSourcePath = System.IO.Path.Join(_userPath, sourcePath);
	// 	var fullDestinationPath = System.IO.Path.Join(_userPath, destinationPath);
	// 	using (var source = System.IO.File.Open(fullSourcePath, System.IO.FileMode.Open)){
	// 		using(var destination = System.IO.File.Create(fullDestinationPath)){
	// 			await source.CopyToAsync(destination);
	// 		}
	// 	}
	// }


	public static /* async Task */ void CopyFileAsync(string sourcePath, string /* destinationPath */destinationUserPath)
	{
		//var fullDestinationPath = System.IO.Path.Join(_userPath, /* destinationPath */destinationUserPath);        
		
		var sourceFile = FileAccess.Open(sourcePath, FileAccess.ModeFlags.Read);    
		if (sourceFile == null)        
			throw new System.IO.FileNotFoundException($"Source file not found: {sourcePath}");        
		// string content = sourceFile.GetAsText();          
		// System.IO.File.WriteAllText(fullDestinationPath, content);        
		// await System.IO.File.WriteAllTextAsync(fullDestinationPath, content);

        var dir = destinationUserPath.GetBaseDir(); // e.g. "user://content/levels/environment"        
		if (!string.IsNullOrEmpty(dir) && !DirAccess.DirExistsAbsolute(dir))            
			DirAccess.MakeDirRecursiveAbsolute(dir);
        using var dst = FileAccess.Open(destinationUserPath, FileAccess.ModeFlags.Write);        
		if (dst == null)            
			throw new System.IO.IOException($"Could not open destination for write: {destinationUserPath}");
        // If it's text:        
		var text = sourceFile.GetAsText();        
		dst.StoreString(text);
		GD.Print("in copyFile method");
        //await Task.CompletedTask; // FileAccess is synchronous; keep API shape if you need it.		
	}


	public static void CopyFile(string sourcePath, string destinationUserPath){

GD.Print($"Exists? {FileAccess.FileExists(sourcePath)} : {sourcePath}");
GD.Print(ProjectSettings.GlobalizePath("res://assets/content/levels/level_2_environment.csv")); 
// GD.Print(FileAccess.FileExists("res://icon.svg"));
//var p = "res://assets/content/levels/level_2_environment.csv";GD.Print("exists: ", FileAccess.FileExists(p));GD.Print("real: ", ProjectSettings.GlobalizePath(p));using var f = FileAccess.Open(p, FileAccess.ModeFlags.Read);GD.Print("open ok: ", f != null);if (f != null)    GD.Print(f.GetAsText());
		try{
			var sourceFile = FileAccess.Open(sourcePath, FileAccess.ModeFlags.Read); 
			if (sourceFile == null) 
				throw new System.IO.FileNotFoundException($"Source file not found: {sourcePath}");
			var dir = DirAccess.Open(destinationUserPath.GetBaseDir()); 
			if (dir == null) 
				DirAccess.MakeDirRecursiveAbsolute(destinationUserPath.GetBaseDir());
			using var dst = FileAccess.Open(destinationUserPath, FileAccess.ModeFlags.Write); 
			if (dst == null) 
				throw new System.IO.IOException($"Could not open destination for write: {destinationUserPath}");
			var text = sourceFile.GetAsText(); dst.StoreString(text); GD.Print($"Successfully copied to {destinationUserPath}");
		}
		catch (Exception ex) { 
			GD.PrintErr($"Error in CopyFile: {ex.Message}\n{ex.StackTrace}"); 
		}
	}


	public static Grid<string>LoadLocalCsv(string path){
		var fullPath = System.IO.Path.Join("res://", path);
		Grid<string> grid = new Grid<string>();

		if (!FileAccess.FileExists(fullPath)){    
			GD.PrintErr($"Missing file: {fullPath}");    
			return grid;
		}

		using var file = FileAccess.Open(fullPath, FileAccess.ModeFlags.Read); // In C#, using guarantees Dispose() is called at the end of the block (even if an exception occurs), which closes/releases the underlying file handle.
		while(!file.EofReached()){
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


    public static async Task<T> LoadJson<T>(string path, string fileName){
		var fullPath = System.IO.Path.Join(_userPath, path, fileName);
        try{
            string json = await System.IO.File.ReadAllTextAsync(fullPath, Encoding.UTF8).ConfigureAwait(false);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			var deserialized = JsonSerializer.Deserialize<T>(json, options);
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

	public static T LoadTres<T>(string path) where T: Resource{
		var res = ResourceLoader.Load(path);
		if(res is T  tTyped){
			return tTyped;
		}else{
			GD.PushError($"Failed to load {path} as {typeof(T).Name} (got {res?.GetType().Name ?? "null"})");
		}
		return default;
	}	
}