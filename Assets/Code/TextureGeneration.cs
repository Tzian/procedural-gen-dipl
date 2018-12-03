﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGeneration {
    public static Color[] GenerateHeightmapTexture(int terrainWidth, int terrainHeight,
        float[,] currentTerrain, List<TerrainParameters> terrainParameterList, TerrainGeneration.TextureType textureType) {
        Color[] terrainTexture = new Color[terrainWidth * terrainHeight];
        for (int y = 0; y < terrainHeight; y++) {
            for (int x = 0; x < terrainWidth; x++) {
                float currentHeight = currentTerrain[x, y];
                switch (textureType) {
                    case TerrainGeneration.TextureType.kColored:
                        for (int i = 0; i < terrainParameterList.Count; i++) {
                            var parameter = terrainParameterList[i];
                            if (currentHeight <= parameter.ParameterBoundry) {
                                terrainTexture[y * terrainWidth + x] = parameter.TerrainColor;
                                break;
                            }
                        }
                        break;
                    case TerrainGeneration.TextureType.kGrayscale:
                        terrainTexture[y * terrainWidth + x] = Color.Lerp(Color.white, Color.black, currentHeight);
                        break;
                    default:
                        Debug.LogError("Unknown texture type.");
                        break;
                }
            }
        }
        return terrainTexture;
    }
}
