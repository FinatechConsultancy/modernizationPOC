namespace Etx.Infrastructure.Cache;

public class CacheSettings
{
    public string CacheType { get; set; }
}

public class NCacheSettings
{
    public string CacheName { get; set; }
    public bool EnableLocalCache { get; set; }
    public int LocalCacheSize { get; set; }
    public string ConfigPath { get; set; }
}
