using STRINGS;
using TUNING;
using UnityEngine;

public class PollutedWaterSublimatorConfig : IBuildingConfig
{
    public const string ID = "PollutedWaterSublimator";
    public static string Effect = string.Format("Converts {0} to {1} using some power in the process.", (object)ELEMENTS.DIRTYWATER.NAME, (object)ELEMENTS.CONTAMINATEDOXYGEN.NAME);
    private const float PWATER_CONSUME_RATE = 1f;
    private const float PWATER_STORAGE = 600f;
    private const float OXYGEN_GENERATION_RATE = 0.66f;
    private const float OXYGEN_TEMPERATURE = 303.15f;
    
    public override BuildingDef CreateBuildingDef()
    {
        float[] tieR3_1 = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
        string[] allMetals = MATERIALS.ALL_METALS;
        EffectorValues tieR3_2 = NOISE_POLLUTION.NOISY.TIER3;
        EffectorValues tieR1 = TUNING.BUILDINGS.DECOR.PENALTY.TIER1;
        EffectorValues noise = tieR3_2;
        BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("PollutedWaterSublimator", 2, 2, "pwater_sublimator_kanim", 30, 30f, tieR3_1, allMetals, 800f, BuildLocationRule.Anywhere, tieR1, noise);
        buildingDef.RequiresPowerInput = true;
        buildingDef.EnergyConsumptionWhenActive = 60f;
        buildingDef.ExhaustKilowattsWhenActive = 0.5f;
        buildingDef.SelfHeatKilowattsWhenActive = 1f;
        buildingDef.InputConduitType = ConduitType.Liquid;
        buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
        buildingDef.UtilityInputOffset = new CellOffset(0, 1);
        buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
        buildingDef.AudioCategory = "HollowMetal";
        buildingDef.Breakable = true;
        return buildingDef;
    }

    public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
    {
        Prioritizable.AddRef(go);
        CellOffset cellOffset = new CellOffset(0, 0);
        Electrolyzer electrolyzer = go.AddOrGet<Electrolyzer>();
        electrolyzer.maxMass = 1.8f;
        electrolyzer.hasMeter = false;
        electrolyzer.emissionOffset = cellOffset;
        Storage storage = go.AddOrGet<Storage>();
        storage.capacityKg = 600f;
        storage.showInUI = true;
        ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
        elementConverter.consumedElements = new ElementConverter.ConsumedElement[1]
        {
      new ElementConverter.ConsumedElement(SimHashes.DirtyWater.CreateTag(), 1f)
        };
        elementConverter.outputElements = new ElementConverter.OutputElement[1]
        {
      new ElementConverter.OutputElement(0.66f, SimHashes.ContaminatedOxygen, 303.15f, outputElementOffsetx: ((float) cellOffset.x), outputElementOffsety: ((float) cellOffset.y))
        };
    }

    public override void DoPostConfigureComplete(GameObject go)
    {
        go.AddOrGet<LogicOperationalController>();
        go.AddOrGetDef<PoweredActiveController.Def>();
    }
}
