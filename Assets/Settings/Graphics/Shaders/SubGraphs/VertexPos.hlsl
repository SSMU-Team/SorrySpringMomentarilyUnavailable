//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSHINCLUDE_INCLUDED
#define MYHLSHINCLUDE_INCLUDED

void GetWorldVertexPos_float( float3 Position,out float3 Out )
{
	Out = mul( UNITY_MATRIX_M,Position );
}

#endif