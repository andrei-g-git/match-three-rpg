shader_type canvas_item;

uniform float energy: hint_range(0.0, 1.0, 0.05);
uniform float depleted_r: hint_range(0.0, 1.0, 0.05);
uniform float depleted_g: hint_range(0.0, 1.0, 0.05);
uniform float depleted_b: hint_range(0.0, 1.0, 0.05);
uniform float filled_r: hint_range(0.0, 1.0, 0.05);
uniform float filled_g: hint_range(0.0, 1.0, 0.05);
uniform float filled_b: hint_range(0.0, 1.0, 0.05);

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec4 tex = texture(TEXTURE, UV);
	if(tex.a < 0.01){
		COLOR.a = 0.0;
		//return;
	}
	vec4 filled = vec4(filled_r, filled_g, filled_b, 1.0);
	vec4 depleted = vec4(depleted_r, depleted_g, depleted_b, 1.0);

	//top-left 0,0
	vec4 col = depleted;
	if(UV.y >= 1.0 - energy){
		col= filled;
	}

	COLOR = col * tex;
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
