DDS Texture Converter - by 510deshawn
================================================

Description:
------------
This tool converts game-ready texture files into .DDS format using BC compression, ideal for game engines or modding.

(Files Supported) Suffix-based logic:
- *_color   → BC7_UNORM_SRGB (color, with sRGB)
- *_metal   → BC4_UNORM (grayscale, metalness)
- *_rough   → BC4_UNORM (grayscale, roughness)
- *_ao      → BC4_UNORM (ambient occlusion)
- *_opacity → BC4_UNORM (opacity masks)
- *_normal  → BC5_SNORM (signed normals)

Requirements:
-------------
- Windows 10 or 11
- [.NET 6 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) if not using the self-contained EXE
- texconv.exe — required! (Download from Microsoft: https://github.com/microsoft/DirectXTex)

How to Use:
-----------
1. Double-click the .exe file
2. Enter the full path to `texconv.exe` when prompted
3. Enter the input folder that contains your texture files
4. Enter the output folder where converted .dds files should go
5. Only files ending in *_color, *_normal, *_metal, etc., will be processed

Example Paths:
--------------
C:\Users\YourName\Documents\texconv.exe  
C:\Projects\MyTextures\Input  
C:\Projects\MyTextures\Converted


Contact:
--------
https://next.nexusmods.com/profile/510deshawn
https://www.youtube.com/@510DeshawnPlays
