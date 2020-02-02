using static ModLoader;
using TerrainGeneration;

namespace grasmanek94.AllOresInAllBiomes
{
    [ModManager]
    public static class AllOresInAllBiomes
    {
        [ModCallback(EModCallbackType.OnAssemblyLoaded, "OnAssemblyLoaded")]
        static void OnLoad(string assemblyPath)
        {
        
        }

        [ModCallback(EModCallbackType.OnLoadingTerrainGenerator, "OnLoadingTerrainGenerator")]
        static void OnLoadingTerrainGenerator(TerrainGeneratorBase generatorBase)
        {
			if(TerrainModManager.QueuedOreLayers == null)
			{
				return;
			}

            for (int i = 0; i < TerrainModManager.QueuedOreLayers.Length; i++)
            {
                TerrainModManager.QueuedOreLayers[i].ScienceBiome = null;
            }


			TerrainGenerator terrainGenerator = generatorBase as TerrainGenerator;
			TerrainGenerator.IFinalChunkModifier finalChunkModifier = (terrainGenerator != null) ? terrainGenerator.FinalChunkModifier : null;
			TerrainGenerator.OreLayersGenerator oreLayersGenerator = null;
			while (finalChunkModifier != null)
			{
				oreLayersGenerator = (finalChunkModifier as TerrainGenerator.OreLayersGenerator);
				if (oreLayersGenerator != null)
				{
					break;
				}
				finalChunkModifier = finalChunkModifier.InnerGenerator;
			}

			if (oreLayersGenerator == null)
			{
				return;
			}

			oreLayersGenerator.Layers.Clear();

			for (int i = 0; i < TerrainModManager.QueuedOreLayers.Length; i++)
			{
				oreLayersGenerator.AddLayer(TerrainModManager.QueuedOreLayers[i], i == TerrainModManager.QueuedOreLayers.Length - 1);
			}
		}
    }
}
