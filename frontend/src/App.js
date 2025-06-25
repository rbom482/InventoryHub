import React, { useState, useEffect } from 'react';
import axios from 'axios';
import TaskList from './components/TaskList';
import TaskForm from './components/TaskForm';

const API_BASE_URL = 'https://localhost:7000/api';

function App() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchTasks();
  }, []);

  const fetchTasks = async () => {
    try {
      setLoading(true);
      const response = await axios.get(`${API_BASE_URL}/tasks`);
      setTasks(response.data);
      setError(null);
    } catch (err) {
      setError('Failed to fetch tasks. Make sure the backend server is running.');
      console.error('Error fetching tasks:', err);
    } finally {
      setLoading(false);
    }
  };

  const addTask = async (taskData) => {
    try {
      const response = await axios.post(`${API_BASE_URL}/tasks`, taskData);
      setTasks([...tasks, response.data]);
      return response.data;
    } catch (err) {
      setError('Failed to add task');
      console.error('Error adding task:', err);
      throw err;
    }
  };

  const updateTask = async (id, taskData) => {
    try {
      await axios.put(`${API_BASE_URL}/tasks/${id}`, taskData);
      setTasks(tasks.map(task => task.id === id ? taskData : task));
    } catch (err) {
      setError('Failed to update task');
      console.error('Error updating task:', err);
    }
  };

  const deleteTask = async (id) => {
    try {
      await axios.delete(`${API_BASE_URL}/tasks/${id}`);
      setTasks(tasks.filter(task => task.id !== id));
    } catch (err) {
      setError('Failed to delete task');
      console.error('Error deleting task:', err);
    }
  };

  const toggleTaskCompletion = async (task) => {
    const updatedTask = {
      ...task,
      isCompleted: !task.isCompleted,
      completedAt: !task.isCompleted ? new Date().toISOString() : null
    };
    await updateTask(task.id, updatedTask);
  };

  return (
    <div className="App">
      <div className="container mt-4">
        <div className="row">
          <div className="col-12">
            <h1 className="text-center mb-4 text-primary">
              📋 Full Stack Task Manager
            </h1>
          </div>
        </div>
        
        {error && (
          <div className="row">
            <div className="col-12">
              <div className="alert alert-danger" role="alert">
                {error}
                <button 
                  type="button" 
                  className="btn-close float-end" 
                  onClick={() => setError(null)}
                ></button>
              </div>
            </div>
          </div>
        )}

        <div className="row">
          <div className="col-md-4">
            <div className="card">
              <div className="card-header bg-primary text-white">
                <h5 className="mb-0">Add New Task</h5>
              </div>
              <div className="card-body">
                <TaskForm onSubmit={addTask} />
              </div>
            </div>
          </div>
          
          <div className="col-md-8">
            <div className="card">
              <div className="card-header bg-secondary text-white">
                <h5 className="mb-0">Tasks ({tasks.length})</h5>
              </div>
              <div className="card-body">
                {loading ? (
                  <div className="text-center">
                    <div className="spinner-border" role="status">
                      <span className="visually-hidden">Loading...</span>
                    </div>
                    <p className="mt-2">Loading tasks...</p>
                  </div>
                ) : (
                  <TaskList 
                    tasks={tasks}
                    onToggleCompletion={toggleTaskCompletion}
                    onDelete={deleteTask}
                  />
                )}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
