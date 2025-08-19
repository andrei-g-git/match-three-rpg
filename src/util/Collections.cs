using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

namespace Util;
public static class Collections{

	public static List<T> RemoveDuplicates<[MustBeVariant]T>(List<T> array_){
		return [..array_.Distinct()];
	}

	public static Array<T> RemoveDuplicates<[MustBeVariant]T>(Array<T> array_){
		return [..array_.Distinct()];
	}
}