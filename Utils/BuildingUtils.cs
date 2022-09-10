using TUNING;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace UTILS
{

    public static class BuildingUtils
    {
        public static void AddBuildingToPlanScreen(
          HashedString category,
          string buildingId,
          string addAfterBuildingId = null)
        {
            int index = BUILDINGS.PLANORDER.FindIndex((Predicate<PlanScreen.PlanInfo>)(x => x.category == category));
            if (index == -1)
                return;
            IList<string> data = (IList<string>) BUILDINGS.PLANORDER[index].data;
            if (data == null)
            {
                Console.WriteLine("<-----Could not load" + buildingId + "to the building menu.----->");
            }
            else
            {
                int num = data.IndexOf(addAfterBuildingId);
                if (num != -1)
                    data.Insert(num + 1, buildingId);
                else
                    data.Add(buildingId);
            }
        }

        public static void AddBuildingToTechnology(string techId, string buildingId) => Db.Get().Techs.Get(techId).unlockedItemIDs.Add(buildingId);
    }
}