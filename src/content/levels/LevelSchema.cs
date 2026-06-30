using Godot;
using Levels;
using System;
using System.Collections.Generic;

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
	public string _upcoming;
	public string Upcoming{
		get => _upcoming;
		set{
			if(value.Contains(".csv")){
				_upcoming = value;
			}else{
				throw new ArgumentException("Must be *.csv file");
				value = ""; 
			}
		}
	}	

	public string _upcomingBg;
	public string UpcomingBg{
		get => _upcomingBg;
		set{
			if(value.Contains(".csv")){
				_upcomingBg = value;
			}else{
				throw new ArgumentException("Must be *.csv file");
				value = ""; 
			}
		}
	}	

	public List<string> Modifiers{get;set;}
	public List<PieceOdds> RandomPieceDistribution{get;set;}
}
