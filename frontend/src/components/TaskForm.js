import React, { useState } from 'react';

const TaskForm = ({ onSubmit }) => {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!title.trim()) return;

    setLoading(true);
    try {
      await onSubmit({
        title: title.trim(),
        description: description.trim(),
        isCompleted: false
      });
      setTitle('');
      setDescription('');
    } catch (error) {
      console.error('Error submitting task:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <div className="mb-3">
        <label htmlFor="taskTitle" className="form-label">Task Title *</label>
        <input
          type="text"
          className="form-control"
          id="taskTitle"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="Enter task title..."
          required
          disabled={loading}
        />
      </div>
      
      <div className="mb-3">
        <label htmlFor="taskDescription" className="form-label">Description</label>
        <textarea
          className="form-control"
          id="taskDescription"
          rows="3"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          placeholder="Enter task description (optional)..."
          disabled={loading}
        />
      </div>
      
      <button 
        type="submit" 
        className="btn btn-primary w-100"
        disabled={loading || !title.trim()}
      >
        {loading ? (
          <>
            <span className="spinner-border spinner-border-sm me-2" role="status"></span>
            Adding...
          </>
        ) : (
          '✅ Add Task'
        )}
      </button>
    </form>
  );
};

export default TaskForm;
