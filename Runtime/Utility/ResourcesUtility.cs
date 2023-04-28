namespace Pangoo
{
    public static class ResourcesUtility
    {

        public static string GetGameInfoBasePath(bool fullpath = false)
        {
            string basePath = "";
            if (fullpath)
            {
                basePath = $"{AssetUtility.GetResources()}/";
            }
            return $"{basePath}GameInfo";
        }

        public static string GetGameInfoOverviewPath(bool fullpath = false)
        {
            return $"{GetGameInfoBasePath(fullpath)}/Overview";
        }

        public static string GetGameInfoOverview(string overview_name, bool fullpath = false)
        {
            return $"{GetGameInfoOverviewPath(fullpath)}/{overview_name}";
        }


    }
}