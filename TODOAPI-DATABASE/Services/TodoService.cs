using Microsoft.EntityFrameworkCore;
using System;
using TODOAPI_DATABASE.DBContext;
using TODOAPI_DATABASE.DTOs;
using TODOAPI_DATABASE.Models;

namespace TODOAPI_DATABASE.Services
{
    public class TodoService : ITodoService
    {

        private readonly List<TodoItem> _todos = new();
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

        // GET all
        public List<TodoResponseDTO> GetAll()
        {
            return _appDBContext.TodoItems
                 .AsEnumerable()
                .Select(t => MapToResponse(t))
                .ToList();
        }

        // GET by id
        public TodoResponseDTO? GetById(int id)
        {
            var todo = _appDBContext.TodoItems
                .FirstOrDefault(t => t.Id == id);
            if (todo == null) return null;
            return MapToResponse(todo);
        }

        // POST - create
        public TodoResponseDTO Create(CreateTodoDTO dto)
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
            _appDBContext.SaveChanges();         
            return MapToResponse(todo);
        }

        // PUT - update
        public TodoResponseDTO? Update(int id, UpdateTodoDTO dto)
        {
            var todo = _appDBContext.TodoItems
                .FirstOrDefault(t => t.Id == id);
            if (todo == null) return null;

            todo.Title = dto.Title;
            todo.Description = dto.Description;
            todo.IsCompleted = dto.IsCompleted;
            todo.DueDate = dto.DueDate;
            todo.Priority = dto.Priority;

            _appDBContext.SaveChanges();     
            return MapToResponse(todo);
        }

        // DELETE
        public bool Delete(int id)
        {
            var todo = _appDBContext.TodoItems
                .FirstOrDefault(t => t.Id == id);
            if (todo == null) return false;

            _appDBContext.TodoItems.Remove(todo);
            _appDBContext.SaveChanges();         
            return true;
        }


    }
}
