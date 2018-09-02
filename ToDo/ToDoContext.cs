using System.Data.Entity;


namespace ToDo
{
    class ToDoContext:DbContext
    {
        public ToDoContext()
            :base ("DefaultConnection")
            {}
        public DbSet<Goal> Goals { get; set; }
    }
}
