import { useState } from 'react'
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'
import Gestao from './pages/Gestao'
import './App.css'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <div className='main'>
        <Routes>
          <Route path={"/"} element={<Gestao />} />
          
        </Routes>
      </div>
    </>
  )
}

export default App
