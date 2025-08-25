using Godot;
using System;

public partial class LevelSchema 
{
	public int Index{get;set;}
	public string Name{get;set;}
	public string Objective{get;set;}
	private string _environment;
	public string Environment{
		get => _environment;
		set{
			if(value.Contains(".csv")){
				_environment = value;
			}else{
				throw new ArgumentException("Must be *.csv file");
				value = ""; 
			}
		}
	}
	public string _pieces;
	public string Pieces{
		get => _pieces;
		set{
			if(value.Contains(".csv")){
				_pieces = value;
			}else{
				throw new ArgumentException("Must be *.csv file");
				value = ""; 
			}
		}
	}
}
