Shader "VertexColorUnlit"
{
	Properties
	{
		_Color ("Line Color", Color) = (1,1,1,1)
	}

	Category
	{
		Lighting Off
		BindChannels
		{
			Bind "Color", color
			Bind "Vertex", vertex
		}

		SubShader 
		{
			Pass
			{
			}
		}
	}
}