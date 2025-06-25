import React from 'react';
import TaskItem from './TaskItem';

const TaskList = ({ tasks, onToggleCompletion, onDelete }) => {
  if (tasks.length === 0) {
    return (
      <div className="text-center py-5 text-muted">
        <h4>📝 No tasks yet!</h4>
        <p>Add your first task to get started.</p>
      </div>
    );
  }

  const completedTasks = tasks.filter(task => task.isCompleted);
  const pendingTasks = tasks.filter(task => !task.isCompleted);

  return (
    <div>
      {pendingTasks.length > 0 && (
        <div className="mb-4">
          <h6 className="text-warning mb-3">
            ⏳ Pending Tasks ({pendingTasks.length})
          </h6>
          {pendingTasks.map(task => (
            <TaskItem
              key={task.id}
              task={task}
              onToggleCompletion={onToggleCompletion}
              onDelete={onDelete}
            />
          ))}
        </div>
      )}

      {completedTasks.length > 0 && (
        <div>
          <h6 className="text-success mb-3">
            ✅ Completed Tasks ({completedTasks.length})
          </h6>
          {completedTasks.map(task => (
            <TaskItem
              key={task.id}
              task={task}
              onToggleCompletion={onToggleCompletion}
              onDelete={onDelete}
            />
          ))}
        </div>
      )}
    </div>
  );
};

export default TaskList;
