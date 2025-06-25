import React from 'react';

const TaskItem = ({ task, onToggleCompletion, onDelete }) => {
  const handleToggle = () => {
    onToggleCompletion(task);
  };

  const handleDelete = () => {
    if (window.confirm('Are you sure you want to delete this task?')) {
      onDelete(task.id);
    }
  };

  const formatDate = (dateString) => {
    if (!dateString) return '';
    const date = new Date(dateString);
    return date.toLocaleDateString() + ' ' + date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
  };

  return (
    <div className={`card task-card mb-3 ${task.isCompleted ? 'completed-task' : ''}`}>
      <div className="card-body">
        <div className="d-flex justify-content-between align-items-start">
          <div className="flex-grow-1">
            <div className="d-flex align-items-center mb-2">
              <button
                className={`btn btn-sm me-3 ${task.isCompleted ? 'btn-success' : 'btn-outline-success'}`}
                onClick={handleToggle}
                title={task.isCompleted ? 'Mark as pending' : 'Mark as completed'}
              >
                {task.isCompleted ? '✅' : '⭕'}
              </button>
              <h6 className={`mb-0 ${task.isCompleted ? 'text-muted' : ''}`}>
                {task.title}
              </h6>
            </div>
            
            {task.description && (
              <p className={`card-text small ${task.isCompleted ? 'text-muted' : ''}`}>
                {task.description}
              </p>
            )}
            
            <div className="small text-muted">
              <div>Created: {formatDate(task.createdAt)}</div>
              {task.completedAt && (
                <div>Completed: {formatDate(task.completedAt)}</div>
              )}
            </div>
          </div>
          
          <button
            className="btn btn-outline-danger btn-sm"
            onClick={handleDelete}
            title="Delete task"
          >
            🗑️
          </button>
        </div>
      </div>
    </div>
  );
};

export default TaskItem;
