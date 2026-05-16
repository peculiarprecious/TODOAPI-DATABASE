using TODOAPI_DATABASE.DTOs;

namespace TODOAPI_DATABASE.Services
{
    public interface ITodoService
    {
        Task<List<TodoResponseDTO>> GetAll();
        Task<TodoResponseDTO?> GetById(int id);
        Task<TodoResponseDTO> Create(CreateTodoDTO dto);
        Task<TodoResponseDTO?> Update(int id, UpdateTodoDTO dto);
        Task<bool> Delete(int id);
    }
}
