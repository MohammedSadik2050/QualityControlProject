namespace eConLab.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly eConLabDbContext _context;

        public InitialHostDbBuilder(eConLabDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            new InitialLookupTypeCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
