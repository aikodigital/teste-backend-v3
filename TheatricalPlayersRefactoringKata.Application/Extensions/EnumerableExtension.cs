namespace cliqx.pernambucanas.selecaorh.extensions
{
    public static class EnumerableExtension
    {
        public static List<T> Pop<T>(this List<T> values, int quantity)
        {
            var valuesTake = values.Take(quantity).ToList();

            values.RemoveAll(x => valuesTake.Contains(x));

            return valuesTake;
        }
    }
}
