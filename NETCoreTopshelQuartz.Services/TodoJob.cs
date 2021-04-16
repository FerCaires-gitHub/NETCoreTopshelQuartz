using Microsoft.Extensions.Logging;
using NETCoreTopshelQuartz.Domain.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NETCoreTopshelQuartz.Services
{
    [DisallowConcurrentExecution]
    public class TodoJob : IJob
    {
        private readonly ILogger<TodoJob> _logger;
        private readonly ITodoRepository _repository;

        public TodoJob(ILogger<TodoJob> logger, ITodoRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"JobExecution:{DateTime.Now.ToString()}");
            _repository.ToDo();
            return Task.CompletedTask;
        }
    }
}
