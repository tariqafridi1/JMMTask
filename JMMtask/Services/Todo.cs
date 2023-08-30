using JMMtask.DatabaseContext;
using JMMtask.IServices;

namespace JMMtask.Services
{
    public class Todo : ITodo
    {
        private readonly TodoDBContext dbContext;

        public Todo(TodoDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddTodo(Models.Todo todo)
        {
            dbContext.Todos.Add(todo);
            //dbContext.SaveChanges();
        }

        public string Delete(int ID)
        {
            var todo = dbContext.Todos.FirstOrDefault(s => s.ID == ID);
            if (todo != null)
            {
                dbContext.Todos.Remove(todo);
               // dbContext.SaveChanges();
            }
            return "Deleted";
        }

        public Models.Todo GetTodoById(int ID)
        {
            return dbContext.Todos.SingleOrDefault(c => c.ID == ID);
        }

        public List<Models.Todo> GetTodos()
        {
            return dbContext.Todos.ToList();
        }

        public void Update(Models.Todo todo)
        {
            dbContext.Todos.Update(todo);
            dbContext.SaveChanges();      
        }
    }
}
