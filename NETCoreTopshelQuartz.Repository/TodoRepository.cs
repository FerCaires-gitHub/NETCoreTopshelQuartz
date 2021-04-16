using Microsoft.Extensions.Logging;
using NETCoreTopshelQuartz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCoreTopshelQuartz.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ILogger<TodoRepository> _logger;

        public TodoRepository(ILogger<TodoRepository> logger)
        {
            _logger = logger;
        }
        public void ToDo()
        {
            _logger.LogInformation($"TodoRepository:{DateTime.Now.ToString()}");
        }
    }
}
