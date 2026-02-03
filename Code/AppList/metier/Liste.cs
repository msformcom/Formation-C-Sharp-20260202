public class Liste
{
    public Liste(string libele)
    {
        this.Libele=libele;
    }
    #region Libele
    private string _Libele;
    public string Libele
    {
        get { return _Libele; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("Libélé est obligatoire");
            }
            _Libele = value;
        }
    }
    #endregion

}