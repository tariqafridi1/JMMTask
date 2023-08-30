using JMMtask.Models;

namespace JMMtask.IServices
{
    public interface ITodo
    {
        List<Todo> GetTodos();
        void AddTodo(Todo todo);

        Todo GetTodoById(int id);
        void Update(Todo todo);
        string Delete(int id);
    }
}
