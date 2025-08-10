using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Util;
public static class Collections{

	public static List<T> RemoveDuplicates<[MustBeVariant]T>(List<T> array_){
		return [..array_.Distinct()];
	}
}