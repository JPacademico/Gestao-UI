import { useState } from 'react'
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'
import Gestao from './pages/Gestao/Gestao'
import Login from './pages/Login/Login'
import './App.css'
import Logs from './pages/Log/Log'
import BenchMarkingItems from './pages/Benchmark/Benchmark'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <div className='main'>
        <Routes>
          <Route path={"/gestao"} element={<Gestao />} />
          <Route path={"/"} element={<Login/>}></Route>
          <Route path={"/log"} element={<Logs/>}></Route>
          <Route path={"/itens"} element={<BenchMarkingItems/>}></Route>
        </Routes>
      </div>
    </>
  )
}

export default App
