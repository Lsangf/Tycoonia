using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application
{
    public class ResourcesBufferBool
    {
        public static bool CheckResourcesBuffer(Dictionary<string, StorageResourcesBase> resorcesBuffer, Dictionary<string, long> recipeListNeeded, decimal produced)
        {
            foreach (var item in recipeListNeeded)
            {
                decimal needed = item.Value * produced;

                if (resorcesBuffer[item.Key].CurrentQuantity < needed)
                    return false;
            }

            return true;
        }
    }
}
