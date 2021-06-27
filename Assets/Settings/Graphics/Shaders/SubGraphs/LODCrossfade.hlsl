//UNITY_SHADER_NO_UPGRADE
#ifndef CROSSFADE_INCLUDED
#define CROSSFADE_INCLUDED

void LODValue_float( out float Out )
{
	if(unity_LODFade.x > 0)
	{
		Out = unity_LODFade.x;
	}
	else
	{
		Out = 1;
	}
}

#endif