namespace Coronavirus.Database.Repository
{
    public class CubeRepository
    {
        private readonly CoronaContext _coronaContext;

        public CubeRepository(CoronaContext coronaContext)
        {
            _coronaContext = coronaContext;
        }

        public void Clear()
        {
            Context.Cubes.Clear();
        }
    }
}
