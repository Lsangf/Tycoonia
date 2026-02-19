using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application
{
    public class ResourcesBufferBool
    {
        public static bool CheckResourcesBuffer(Dictionary<string, StorageResourcesBase> resorcesBuffer, Dictionary<string, byte> recipeListNeeded)
        {
            bool check = false;
            foreach (var item in recipeListNeeded)
            {
                if (resorcesBuffer == null || resorcesBuffer[item.Key].CurrentQuantity < item.Value)
                {
                    return check = false;
                }
                else
                {
                    check = true;
                }
            }
            return check;
        }
    }
}
