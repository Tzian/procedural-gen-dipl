﻿using UnityEngine;

public class TerrainGenerationDebugger : MonoBehaviour {
    public enum DebugPlaneContent {
        kHeightMap,
        kMoistureMap,
        kTemperatureMap
    }

    public TerrainGeneration tg;
    [SerializeField]
    public DebugPlaneContent planeContent;

    private static Texture2D texture;
    private void Start() {
        planeContent = DebugPlaneContent.kHeightMap;
    }

    private void Update() {
        // Fun fact, Unity does not GC new Terrains, which results in a memory leak, to prevent this we call destroy and have the texture as static
        Destroy(texture);
        // The debug terrain is driven by the TerrainGeneration (the main terrain that we are working on)
        texture = new Texture2D(tg.TerrainWidth, tg.TerrainHeight);
        GetComponent<Renderer>().material.mainTexture = texture;
        if (tg.TerrainHeightMap == null) {
            return;
        }
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        switch (planeContent) {
            case (DebugPlaneContent.kHeightMap):
                texture.SetPixels(TextureGeneration.GenerateHeightmapTexture(tg.TerrainWidth, tg.TerrainHeight, tg.TerrainHeightMap, tg.TerrainParameterList, tg.TerrainTextureType));
                break;
            case (DebugPlaneContent.kMoistureMap):
                texture.SetPixels(TextureGeneration.GenerateHeightmapTexture(tg.TerrainWidth, tg.TerrainHeight, tg.TerrainMoistureMap, tg.TerrainParameterList, tg.TerrainTextureType));
                break;
            case (DebugPlaneContent.kTemperatureMap):
                texture.SetPixels(TextureGeneration.GenerateHeightmapTexture(tg.TerrainWidth, tg.TerrainHeight, tg.TerrainTemperatureMap, tg.TerrainParameterList, tg.TerrainTextureType));
                break;
        }
        texture.Apply();
    }
}
