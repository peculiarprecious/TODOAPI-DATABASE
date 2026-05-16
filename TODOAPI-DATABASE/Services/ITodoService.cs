using TODOAPI_DATABASE.DTOs;

namespace TODOAPI_DATABASE.Services
{
    public interface ITodoService
    {
        List<TodoResponseDTO> GetAll();
        TodoResponseDTO? GetById(int id);
        TodoResponseDTO Create(CreateTodoDTO dto);
        TodoResponseDTO? Update(int id, UpdateTodoDTO dto);
        bool Delete(int id);
    }
}
