// Compiled shader for PC, Mac & Linux Standalone, uncompressed size: 1.6KB

// Skipping shader variants that would not be included into build of current scene.

Shader "Hidden/EdgeDetect" {
Properties {
 _Color ("Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGB)", 2D) = "" { }
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 13283
Program "vp" {
// Platform d3d9 had shader errors
//   <no keywords>
}
Program "fp" {
// Platform d3d9 skipped due to earlier errors
// Platform d3d9 had shader errors
//   <no keywords>
}
 }
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 86140
Program "vp" {
// Platform d3d9 skipped due to earlier errors
// Platform d3d9 had shader errors
//   <no keywords>
}
Program "fp" {
// Platform d3d9 skipped due to earlier errors
// Platform d3d9 had shader errors
//   <no keywords>
}
 }
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 158998
Program "vp" {
// Platform d3d9 skipped due to earlier errors
// Platform d3d9 had shader errors
//   <no keywords>
}
Program "fp" {
// Platform d3d9 skipped due to earlier errors
// Platform d3d9 had shader errors
//   <no keywords>
}
 }
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 241142
Program "vp" {
// Platform d3d9 skipped due to earlier errors
// Platform d3d9 had shader errors
//   <no keywords>
}
Program "fp" {
// Platform d3d9 skipped due to earlier errors
// Platform d3d9 had shader errors
//   <no keywords>
}
 }
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 275762
Program "vp" {
// Platform d3d9 skipped due to earlier errors
// Platform d3d9 had shader errors
//   <no keywords>
}
Program "fp" {
// Platform d3d9 skipped due to earlier errors
// Platform d3d9 had shader errors
//   <no keywords>
}
 }
}
Fallback Off
}