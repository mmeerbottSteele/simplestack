using backend.DbContexts;

namespace backend.Services;
public class SimpleService
{
    private readonly SimpleDbContext _context;
    public SimpleService(SimpleDbContext context)
    {
        _context = context;
    }

    public List<Message> GetMessages()
    {
        var list = _context.Messages.ToList();
        return list;
    }
}