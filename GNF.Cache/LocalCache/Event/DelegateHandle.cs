namespace GNF.Cache.LocalCache.Event
{
    /// <summary>
    /// 添加修改事件委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void CacheEventHandle(object sender, ICacheEventArgs e);
}
