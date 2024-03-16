import { useState } from 'react'
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'
import Gestao from './pages/Gestao/Gestao'
import Login from './pages/Login/Login'
import './App.css'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <div className='main'>
        <Routes>
          <Route path={"/gestao"} element={<Gestao />} />
          <Route path={"/"} element={<Login/>}></Route>
        </Routes>
      </div>
    </>
  )
}

export default App
