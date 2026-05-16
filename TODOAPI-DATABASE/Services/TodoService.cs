using Microsoft.EntityFrameworkCore;
using System;
using TODOAPI_DATABASE.DBContext;
using TODOAPI_DATABASE.DTOs;
using TODOAPI_DATABASE.Models;

namespace TODOAPI_DATABASE.Services
{
    public class TodoService : ITodoService
    {

        private readonly ApplicationDBContext _appDBContext;

        public TodoService(ApplicationDBContext appDBcontext)
        {
            _appDBContext = appDBcontext;
        }

        private static TodoResponseDTO MapToResponse(TodoItem todo)
        {
            return new TodoResponseDTO
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
                CreatedAt = todo.CreatedAt,
                DueDate = todo.DueDate,
                Priority = todo.Priority
            };
        }

        // GET all — ToListAsync()
        public async Task<List<TodoResponseDTO>> GetAll()
        {
            var todos = await _appDBContext.TodoItems
                .ToListAsync();             

            return todos
                .Select(t => MapToResponse(t))
                .ToList();
        }

        // GET by id — FindAsync()
        public async Task<TodoResponseDTO?> GetById(int id)
        {
            var todo = await _appDBContext.TodoItems
                .FindAsync(id);           

            if (todo == null) return null;
            return MapToResponse(todo);
        }
        // POST 
        public async Task<TodoResponseDTO> Create(CreateTodoDTO dto)
        {
            var todo = new TodoItem
            {
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = false,
                CreatedAt = DateTime.Now,
                DueDate = dto.DueDate,
                Priority = dto.Priority
            };

            _appDBContext.TodoItems.Add(todo);
            await _appDBContext.SaveChangesAsync();  

            return MapToResponse(todo);
        }

        // PUT 
        public async Task<TodoResponseDTO?> Update(int id, UpdateTodoDTO dto)
        {
            var todo = await _appDBContext.TodoItems
                .FindAsync(id);             

            if (todo == null) return null;

            todo.Title = dto.Title;
            todo.Description = dto.Description;
            todo.IsCompleted = dto.IsCompleted;
            todo.DueDate = dto.DueDate;
            todo.Priority = dto.Priority;

            await _appDBContext.SaveChangesAsync(); 

            return MapToResponse(todo);
        }

        // DELETE 
        public async Task<bool> Delete(int id)
        {
            var todo = await _appDBContext.TodoItems
                .FindAsync(id);          

            if (todo == null) return false;

            _appDBContext.TodoItems.Remove(todo);
            await _appDBContext.SaveChangesAsync(); 

            return true;
        }


    }
}
