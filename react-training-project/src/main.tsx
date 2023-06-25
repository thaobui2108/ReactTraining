import React from 'react'
import ReactDOM from 'react-dom/client'
import Todo from './Todo.tsx'
import './index.css'

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>
    <Todo />
  </React.StrictMode>,
)
