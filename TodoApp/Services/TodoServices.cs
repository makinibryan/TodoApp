﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoApp.AppDataContext;
using TodoApp.Contracts;
using TodoApp.Interfaces;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TodoServices : ITodoServices
    {
        private readonly TodoDbContext _context;
        private readonly ILogger<TodoServices> _logger;
        private readonly IMapper _mapper;

        public TodoServices(TodoDbContext context, ILogger<TodoServices> logger, IMapper mapper )
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task CreateTodoAsync(CreateTodoRequest request)
        {
            try
            {
                var todo = _mapper.Map<Todo>(request);
                todo.CreatedAt = DateTime.Now;
                _context.Todos.Add(todo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while creating the Todo item.");
                throw new Exception("An error occurred while creating the Todo item.");
            }
        }

      

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            var todo = await _context.Todos.ToListAsync();
            if (todo == null) 
            { 
                throw new Exception("No Todo items found");
            }
            return todo;
        }

        public async Task<Todo> GetByIdAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                throw new KeyNotFoundException($"No Todo item with Id {id} found");
            }
            return todo;
        }

        public async Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);
                if (todo == null)
                {
                    throw new Exception($"Todo item with Id: {id} was not found");
                }
                if (request.Title != null)
                {
                    todo.Title = request.Title;
                }
                if (request.Description != null)
                {
                    todo.Description = request.Description;
                }
                if (request.IsComplete != null)
                {
                    todo.IsComplete = request.IsComplete.Value;
                }

                if (request.DueDate != null)
                {
                    todo.DueDate = request.DueDate.Value;
                }

                if (request.Priorty != null)
                {
                    todo.Priority = request.Priorty.Value;
                }

                todo.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"An error occurred while updating the todo item with id {id}.");
            }
        }
        public async Task DeleteTodoAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"No  item found with the id {id}");
            }
        }
    }
}
