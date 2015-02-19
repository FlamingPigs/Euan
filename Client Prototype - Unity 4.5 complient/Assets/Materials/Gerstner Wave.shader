Shader "Custom/Gerstner Wave" 
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,0.5)
	}
	
	SubShader 
	{ 
		//Pass
		//{
			CGPROGRAM
			//#pragma surface surf BlinnPhong alpha vertex:vert
			#pragma surface surf BlinnPhong alpha vertex:vert
	        //#pragma target 2.0 this sets the rendering target to Shader model 2.0 in DirectX 9 (this is the default hence commented out)

			//float4 values below now allow for 4 waves to be passed through mesh, .x .y .z .w all represent a wave individually
			sampler2D _MainTex;
			float4 steepness;
			float4 amplitude;
			float4 direction1;
			float4 direction2;
			float4 centre1;
			float4 centre2;
			float4 circular;
			float4 wavelength;
			float4 speed;
			float deltaTime;
			float PI;
			float4 frequency = 0;
			float4 phaseConstant = 0;
			fixed4 _Color;
			float3 northVertex, southVertex, eastVertex, westVertex;
			
			struct Input 
			{
				float2 uv_MainTex;
			};
			
			float3 calculateNormals(inout appdata_full v)
			{
				//Recalculate normals -- THIS CURRENTLY DOESN'T DO VERY MUCH.. BUT IT ATLEAST ALLOWS THE WATER TO BE LIT
			    //Get the height of the newly sampled vertices
				//float3 northPixelHeightPos = float3(v.vertex.x, v.vertex.y, northVertex.z);
				//float3 southPixelHeightPos = float3(v.vertex.x, v.vertex.y, southVertex.z);
				//float3 eastPixelHeightPos = float3(eastVertex.x, v.vertex.y, v.vertex.z);
				//float3 westPixelHeightPos = float3(westVertex.x, v.vertex.y, v.vertex.z);
	
			    //Get vectors from the points above - this will give a base to get the normal from by giving us the gradient of our current vertex
				//float3 northSouth = northPixelHeightPos - southPixelHeightPos;
				//float3 eastWest = eastPixelHeightPos - westPixelHeightPos;
				
				
				//v.normal = normalize(normals);
				
				return 0;
			}

		    void vert(inout appdata_full v) 
		    {
		    	//wave 1
		    	frequency.x = (2 * PI) / wavelength.x;
		   		phaseConstant.x = speed.x * (2 * PI) / wavelength.x;
		   		northVertex = v.vertex + float3(0, 0, 1);
		   		southVertex = v.vertex + float3(0, 0, -1);
		   		eastVertex = v.vertex + float3(1, 0, 0);
		   		westVertex = v.vertex + float3(-1, 0, 0);
		   		
		   		//DO ALL THIS AGAIN FOR ALL 'GHOST NEIGHBOURS' AND USE THESE VALUES TO GET NORMALS PER CALCULATION BELOW
		   		if(circular.x == 1)
		   		{		   			
		   			direction1.x = (v.vertex.x - centre1.x)/sqrt((v.vertex.x * v.vertex.x) + (centre1.x * centre1.x));
		   			direction1.y = (v.vertex.z - centre1.y)/sqrt((v.vertex.z * v.vertex.z) + (centre1.y * centre1.y));
		   		}
		    	
		    	v.vertex.x += (steepness.x * amplitude.x) * direction1.x * cos((frequency.x * dot(float2(direction1.x, direction1.y), v.vertex.xz)) + (phaseConstant.x * deltaTime));
		    	v.vertex.y += amplitude.x * sin((frequency.x * dot(float2(direction1.x, direction1.y), v.vertex.xz)) + (phaseConstant.x * deltaTime));
		    	v.vertex.z += (steepness.x * amplitude.x) * direction1.y * cos((frequency.x * dot(float2(direction1.x, direction1.y), v.vertex.xz)) + (phaseConstant.x * deltaTime));
		    	
		    	northVertex.x += (steepness.x * amplitude.x) * direction1.x * cos((frequency.x * dot(float2(direction1.x, direction1.y), northVertex.xz)) + (phaseConstant.x * deltaTime));
		    	northVertex.y += amplitude.x * sin((frequency.x * dot(float2(direction1.x, direction1.y), northVertex.xz)) + (phaseConstant.x * deltaTime));
		    	northVertex.z += (steepness.x * amplitude.x) * direction1.y * cos((frequency.x * dot(float2(direction1.x, direction1.y), northVertex.xz)) + (phaseConstant.x * deltaTime));
		    	
		    	southVertex.x += (steepness.x * amplitude.x) * direction1.x * cos((frequency.x * dot(float2(direction1.x, direction1.y), southVertex.xz)) + (phaseConstant.x * deltaTime));
		    	southVertex.y += amplitude.x * sin((frequency.x * dot(float2(direction1.x, direction1.y), southVertex.xz)) + (phaseConstant.x * deltaTime));
		    	southVertex.z += (steepness.x * amplitude.x) * direction1.y * cos((frequency.x * dot(float2(direction1.x, direction1.y), southVertex.xz)) + (phaseConstant.x * deltaTime));
		    	
		    	eastVertex.x += (steepness.x * amplitude.x) * direction1.x * cos((frequency.x * dot(float2(direction1.x, direction1.y), eastVertex.xz)) + (phaseConstant.x * deltaTime));
		    	eastVertex.y += amplitude.x * sin((frequency.x * dot(float2(direction1.x, direction1.y), eastVertex.xz)) + (phaseConstant.x * deltaTime));
		    	eastVertex.z += (steepness.x * amplitude.x) * direction1.y * cos((frequency.x * dot(float2(direction1.x, direction1.y), eastVertex.xz)) + (phaseConstant.x * deltaTime));
		    	
		    	westVertex.x += (steepness.x * amplitude.x) * direction1.x * cos((frequency.x * dot(float2(direction1.x, direction1.y), westVertex.xz)) + (phaseConstant.x * deltaTime));
		    	westVertex.y += amplitude.x * sin((frequency.x * dot(float2(direction1.x, direction1.y), westVertex.xz)) + (phaseConstant.x * deltaTime));
		    	westVertex.z += (steepness.x * amplitude.x) * direction1.y * cos((frequency.x * dot(float2(direction1.x, direction1.y), westVertex.xz)) + (phaseConstant.x * deltaTime));
		    	
		    	if(amplitude.y != 0)
		    	{
			    	//wave 2
			    	frequency.y = (2 * PI) / wavelength.y;
			   		phaseConstant.y = speed.y * (2 * PI) / wavelength.y;
			   		
			   		if(circular.y == 1)
			   		{		   			
			   			direction1.z = (v.vertex.x - centre1.z)/sqrt((v.vertex.x * v.vertex.x) + (centre1.z * centre1.z));
			   			direction1.w = (v.vertex.z - centre1.w)/sqrt((v.vertex.z * v.vertex.z) + (centre1.w * centre1.w));
			   		}
			    	
			    	v.vertex.x += (steepness.y * amplitude.y) * direction1.z * cos((frequency.y * dot(float2(direction1.z, direction1.w), v.vertex.xz)) + (phaseConstant.y * deltaTime));
			    	v.vertex.y += amplitude.y * sin((frequency.y * dot(float2(direction1.z, direction1.w), v.vertex.xz)) + (phaseConstant.y * deltaTime));
			    	v.vertex.z += (steepness.y * amplitude.y) * direction1.w * cos((frequency.y * dot(float2(direction1.z, direction1.w), v.vertex.xz)) + (phaseConstant.y * deltaTime));
			    	
			    	northVertex.x += (steepness.y * amplitude.y) * direction1.z * cos((frequency.y * dot(float2(direction1.z, direction1.w), northVertex.xz)) + (phaseConstant.y * deltaTime));
			    	northVertex.y += amplitude.y * sin((frequency.y * dot(float2(direction1.z, direction1.w), northVertex.xz)) + (phaseConstant.y * deltaTime));
			    	northVertex.z += (steepness.y * amplitude.y) * direction1.w * cos((frequency.y * dot(float2(direction1.z, direction1.w), northVertex.xz)) + (phaseConstant.y * deltaTime));
			    	
			    	southVertex.x += (steepness.y * amplitude.y) * direction1.z * cos((frequency.y * dot(float2(direction1.z, direction1.w), southVertex.xz)) + (phaseConstant.y * deltaTime));
			    	southVertex.y += amplitude.y * sin((frequency.y * dot(float2(direction1.z, direction1.w), southVertex.xz)) + (phaseConstant.y * deltaTime));
			    	southVertex.z += (steepness.y * amplitude.y) * direction1.w * cos((frequency.y * dot(float2(direction1.z, direction1.w), southVertex.xz)) + (phaseConstant.y * deltaTime));
			    	
			    	eastVertex.x += (steepness.y * amplitude.y) * direction1.z * cos((frequency.y * dot(float2(direction1.z, direction1.w), eastVertex.xz)) + (phaseConstant.y * deltaTime));
			    	eastVertex.y += amplitude.y * sin((frequency.y * dot(float2(direction1.z, direction1.w), eastVertex.xz)) + (phaseConstant.y * deltaTime));
			    	eastVertex.z += (steepness.y * amplitude.y) * direction1.w * cos((frequency.y * dot(float2(direction1.z, direction1.w), eastVertex.xz)) + (phaseConstant.y * deltaTime));
			    	
			    	westVertex.x += (steepness.y * amplitude.y) * direction1.z * cos((frequency.y * dot(float2(direction1.z, direction1.w), westVertex.xz)) + (phaseConstant.y * deltaTime));
			    	westVertex.y += amplitude.y * sin((frequency.y * dot(float2(direction1.z, direction1.w), westVertex.xz)) + (phaseConstant.y * deltaTime));
			    	westVertex.z += (steepness.y * amplitude.y) * direction1.w * cos((frequency.y * dot(float2(direction1.z, direction1.w), westVertex.xz)) + (phaseConstant.y * deltaTime));
			    }
		    	
		    	if(amplitude.z != 0)
		    	{
			    	//wave 3
			    	frequency.z = (2 * PI) / wavelength.z;
			   		phaseConstant.z = speed.z * (2 * PI) / wavelength.z;
			   		
			   		if(circular.z == 1)
			   		{		   			
			   			direction2.x = (v.vertex.x - centre2.x)/sqrt((v.vertex.x * v.vertex.x) + (centre2.x * centre2.x));
			   			direction2.y = (v.vertex.z - centre2.y)/sqrt((v.vertex.z * v.vertex.z) + (centre2.y * centre2.y));
			   		}
			    	
			    	v.vertex.x += (steepness.z * amplitude.z) * direction2.x * cos((frequency.z * dot(float2(direction2.x, direction2.y), v.vertex.xz)) + (phaseConstant.z * deltaTime));
			    	v.vertex.y += amplitude.z * sin((frequency.z * dot(float2(direction2.x, direction2.y), v.vertex.xz)) + (phaseConstant.z * deltaTime));
			    	v.vertex.z += (steepness.z * amplitude.z) * direction2.y * cos((frequency.z * dot(float2(direction2.x, direction2.y), v.vertex.xz)) + (phaseConstant.z * deltaTime));
			    	
			    	northVertex.x += (steepness.z * amplitude.z) * direction2.x * cos((frequency.z * dot(float2(direction2.x, direction2.y), northVertex.xz)) + (phaseConstant.z * deltaTime));
			    	northVertex.y += amplitude.z * sin((frequency.z * dot(float2(direction2.x, direction2.y), northVertex.xz)) + (phaseConstant.z * deltaTime));
			    	northVertex.z += (steepness.z * amplitude.z) * direction2.y * cos((frequency.z * dot(float2(direction2.x, direction2.y), northVertex.xz)) + (phaseConstant.z * deltaTime));
			    	
			    	southVertex.x += (steepness.z * amplitude.z) * direction2.x * cos((frequency.z * dot(float2(direction2.x, direction2.y), southVertex.xz)) + (phaseConstant.z * deltaTime));
			    	southVertex.y += amplitude.z * sin((frequency.z * dot(float2(direction2.x, direction2.y), southVertex.xz)) + (phaseConstant.z * deltaTime));
			    	southVertex.z += (steepness.z * amplitude.z) * direction2.y * cos((frequency.z * dot(float2(direction2.x, direction2.y), southVertex.xz)) + (phaseConstant.z * deltaTime));
			    	
			    	eastVertex.x += (steepness.z * amplitude.z) * direction2.x * cos((frequency.z * dot(float2(direction2.x, direction2.y), eastVertex.xz)) + (phaseConstant.z * deltaTime));
			    	eastVertex.y += amplitude.z * sin((frequency.z * dot(float2(direction2.x, direction2.y), eastVertex.xz)) + (phaseConstant.z * deltaTime));
			    	eastVertex.z += (steepness.z * amplitude.z) * direction2.y * cos((frequency.z * dot(float2(direction2.x, direction2.y), eastVertex.xz)) + (phaseConstant.z * deltaTime));
			    	
			    	westVertex.x += (steepness.z * amplitude.z) * direction2.x * cos((frequency.z * dot(float2(direction2.x, direction2.y), westVertex.xz)) + (phaseConstant.z * deltaTime));
			    	westVertex.y += amplitude.z * sin((frequency.z * dot(float2(direction2.x, direction2.y), westVertex.xz)) + (phaseConstant.z * deltaTime));
			    	westVertex.z += (steepness.z * amplitude.z) * direction2.y * cos((frequency.z * dot(float2(direction2.x, direction2.y), westVertex.xz)) + (phaseConstant.z * deltaTime));
			    }
		    	
		    	if(amplitude.w != 0)
		    	{
			    	//wave 4
			    	frequency.w = (2 * PI) / wavelength.w;
			   		phaseConstant.w = speed.w * (2 * PI) / wavelength.w;
			   		
			   		if(circular.w == 1)
			   		{		   			
			   			direction2.z = (v.vertex.x - centre2.z)/sqrt((v.vertex.x * v.vertex.x) + (centre2.z * centre2.z));
			   			direction2.w = (v.vertex.z - centre2.w)/sqrt((v.vertex.z * v.vertex.z) + (centre2.w * centre2.w));
			   		}
			   		
			   		v.vertex.x += (steepness.w * amplitude.w) * direction2.z * cos((frequency.w * dot(float2(direction2.z, direction2.w), v.vertex.xz)) + (phaseConstant.w * deltaTime));
			    	v.vertex.y += amplitude.w * sin((frequency.w * dot(float2(direction2.z, direction2.w), v.vertex.xz)) + (phaseConstant.w * deltaTime));
			    	v.vertex.z += (steepness.w * amplitude.w) * direction2.w * cos((frequency.w * dot(float2(direction2.z, direction2.w), v.vertex.xz)) + (phaseConstant.w * deltaTime));
			    	
			    	northVertex.x += (steepness.w * amplitude.w) * direction2.z * cos((frequency.w * dot(float2(direction2.z, direction2.w), northVertex.xz)) + (phaseConstant.w * deltaTime));
			    	northVertex.y += amplitude.w * sin((frequency.w * dot(float2(direction2.z, direction2.w), northVertex.xz)) + (phaseConstant.w * deltaTime));
			    	northVertex.z += (steepness.w * amplitude.w) * direction2.w * cos((frequency.w * dot(float2(direction2.z, direction2.w), northVertex.xz)) + (phaseConstant.w * deltaTime));
			    	
			    	southVertex.x += (steepness.w * amplitude.w) * direction2.z * cos((frequency.w * dot(float2(direction2.z, direction2.w), southVertex.xz)) + (phaseConstant.w * deltaTime));
			    	southVertex.y += amplitude.w * sin((frequency.w * dot(float2(direction2.z, direction2.w), southVertex.xz)) + (phaseConstant.w * deltaTime));
			    	southVertex.z += (steepness.w * amplitude.w) * direction2.w * cos((frequency.w * dot(float2(direction2.z, direction2.w), southVertex.xz)) + (phaseConstant.w * deltaTime));
			    	
			    	eastVertex.x += (steepness.w * amplitude.w) * direction2.z * cos((frequency.w * dot(float2(direction2.z, direction2.w), eastVertex.xz)) + (phaseConstant.w * deltaTime));
			    	eastVertex.y += amplitude.w * sin((frequency.w * dot(float2(direction2.z, direction2.w), eastVertex.xz)) + (phaseConstant.w * deltaTime));
			    	eastVertex.z += (steepness.w * amplitude.w) * direction2.w * cos((frequency.w * dot(float2(direction2.z, direction2.w), eastVertex.xz)) + (phaseConstant.w * deltaTime));
			    	
			    	westVertex.x += (steepness.w * amplitude.w) * direction2.z * cos((frequency.w * dot(float2(direction2.z, direction2.w), westVertex.xz)) + (phaseConstant.w * deltaTime));
			    	westVertex.y += amplitude.w * sin((frequency.w * dot(float2(direction2.z, direction2.w), westVertex.xz)) + (phaseConstant.w * deltaTime));
			    	westVertex.z += (steepness.w * amplitude.w) * direction2.w * cos((frequency.w * dot(float2(direction2.z, direction2.w), westVertex.xz)) + (phaseConstant.w * deltaTime));
			    }
				
				float3 northSouth = northVertex - southVertex;
				float3 eastWest = eastVertex - westVertex;

				//Lets get the cross product of these to get the perpindicular normal
				float3 normals = cross(northSouth, eastWest);
				v.normal = normalize(normals);	
		    }

		    void surf (Input IN, inout SurfaceOutput o)
		    {
			    //this code takes the position of the texture and moves it based on the direction vector
			    float2 uvs = IN.uv_MainTex;
			    float disp = deltaTime*speed/250;
			    uvs += direction1.xy*disp;
				fixed4 tex = tex2D(_MainTex, uvs);
				//multiply the colour value by the texture so you can change its appearance
				o.Albedo = tex.rgb;
				//set the gloss to the alpha value (not entirely sure why)
				o.Gloss = tex.a;
				//get the alpha value from the colour and times it by the texture
				o.Alpha = tex.a * _Color.a;
				//the specularity is equal to the variable defined above
				o.Specular = 0.07;
			}
		    
			ENDCG
		//}
	} 
}
